﻿using AutoMapper;
using AutomovieApi.Entities;
using AutomovieApi.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AutomovieApi.Models.Auth;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Security.Cryptography;
using AutomovieApi.Services;
using AutomovieApi.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using static System.Net.WebRequestMethods;
using Braintree;
using Braintree.Exceptions;
using Microsoft.Extensions.Logging;

namespace AutomovieApi.Controllers
{
    
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly PlatformDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IBraintreeGateway _braintreeGateway;
        private readonly ILogger<AuthController> _logger;
        private readonly decimal _paymentAmount = 60.00m;



        public AuthController(PlatformDbContext dbContext, IConfiguration configuration, IPasswordHasher<User> passwordHasher, IBraintreeGateway braintreeGateway, ILogger<AuthController> logger)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _braintreeGateway = braintreeGateway;
            _logger = logger;
        }
        public class TransferRequest
        {
            public string PaymentMethodNonce { get; set; }
        }


        [HttpGet("request-payment")]
        public IActionResult GetClientToken()
        {
            var clientToken = _braintreeGateway.ClientToken.Generate();
            decimal paymentAmount = _paymentAmount;
            var response = new
            {
                token = clientToken,
                amount = paymentAmount,
                currency = "PLN"
            };

            return Ok(response);
        }
        [HttpPost("MakePayment")]
        public async Task<IActionResult> MakePayment([FromBody] TransferRequest model)
        {
            var transactionRequest = new TransactionRequest
            {
                Amount = _paymentAmount,
                PaymentMethodNonce = model.PaymentMethodNonce,
                MerchantAccountId = "DriveAPP",
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            var result = await _braintreeGateway.Transaction.SaleAsync(transactionRequest);

            if (result.IsSuccess())
            {
                _logger.LogInformation($"Payment successful. Transaction ID: {result.Target.Id}");
                return Ok(result.Target);
            }
            else
            {
                _logger.LogError($"Payment failed. Errors: {string.Join(", ", result.Errors.DeepAll().Select(e => e.Message))}");
                return BadRequest(result.Errors.DeepAll());
            }
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest RegisterRequest)
        {
            var fieldState = ModelState.GetFieldValidationState("Email");
            if (fieldState == ModelValidationState.Invalid)
            {
                var errors = ModelState["Email"].Errors;
                return BadRequest(errors);
            }

            try
            {
                if (await _dbContext.Users.AnyAsync(u => u.Email == RegisterRequest.Email))
                {
                    return BadRequest("Email already in use.");
                }

                double _lan = 0;
                double _lng = 0;



                    if (!string.IsNullOrEmpty(RegisterRequest.City))
                    {
                        string adresUrl = "https://nominatim.openstreetmap.org/";

                        var client = new RestClient(adresUrl);

                        var req = new RestRequest("search?city=" + RegisterRequest.City + "&country=Poland&format=geojson");

                        req.RequestFormat = DataFormat.Json;
                        Task<RestResponse> t = client.ExecuteGetAsync(req);
                        t.Wait();
                        var response = t.Result as RestResponse;

                        if (response.IsSuccessful)
                        {
                            Root dane = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(response.Content);

                            if (dane.features != null && dane.features.Any())
                            {
                                List<double> _coordinates = dane.features.FirstOrDefault().geometry.coordinates.ToList();
                                _lng = _coordinates.First();
                                _lan = _coordinates.Last();
                            }
                            else
                            {
                                // Brak danych geograficznych dla podanego miasta
                                return BadRequest("Brak danych geograficznych dla podanego miasta.");
                            }
                        }
                        else
                        {
                            // Obsługa błędów
                            return BadRequest($"Zapytanie o lokalizacje miasta nie powiodło się. Status: {response.StatusCode}, Błąd: {response.ErrorMessage}");
                        }
                    }

              


                var user = new User
                {
                    Name = RegisterRequest.Name,
                    Surname = RegisterRequest.Surname,
                    Phone = RegisterRequest.Phone,
                    Email = RegisterRequest.Email,
                    City = RegisterRequest.City,
                    Voivodeship = RegisterRequest.Voivodeship,
                    lat = _lan,
                    lng = _lng,
                    PasswordHash = _passwordHasher.HashPassword(null, RegisterRequest.Password)
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();



                return Ok("User registered successfully.");
            }
            catch (HttpRequestException httpEx)
            {
                return StatusCode(500, httpEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = GenerateToken(user);

            return Ok(new AuthResponse
            {
                Token = token,
                Email = user.Email,
                UserId = user.UserId.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                ExpiresIn = _configuration.GetValue<int>("Jwt:TokenLifetimeMinutes")
            });
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenLifetimeMinutes = _configuration.GetValue<int>("Jwt:TokenLifetimeMinutes");
            var expiration = DateTime.Now.AddMinutes(tokenLifetimeMinutes);


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class PaymentRequest
        {
            public decimal Amount { get; set; }
            public string Nonce { get; set; }
        }

    }
}