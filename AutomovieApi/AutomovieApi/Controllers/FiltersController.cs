using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutomovieApi.Entities;
using AutomovieApi.Models;
using AutomovieApi.Models.Filters;
using AutomovieApi.Models.Post;
using AutomovieApi.Models.SuggestionsDto;
using AutomovieApi.Services;
using Azure.Storage.Blobs;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using RestSharp;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using static AutomovieApi.Controllers.PlatformController;

namespace AutomovieApi.Controllers
{
    [Route("api/filters")]
    public class FiltersController : ControllerBase
    {
        private readonly PlatformDbContext _dbContext;
        private readonly IAutomovieServices _automovieServices;
        private readonly IMapper _mapper;

        public FiltersController(PlatformDbContext dbContext, IAutomovieServices automovieServices, IMapper mapper)
        {
            _dbContext = dbContext;
            _automovieServices = automovieServices;
            _mapper = mapper;
        }

        private string ToKebabCase(string input)
        {
            return string.IsNullOrEmpty(input) ? input : string.Join("-", input.Split(' ').Select(s => s.ToLower()));
        }

        [HttpPost("filterAnn")]
        public async Task<IActionResult> FilterAnnouncements([FromBody] FilterRequest filter)
        {
            var query = _dbContext.Announcements
                .AsNoTracking()
                .Include(a => a.User)
                .Include(a => a.Images)
                .AsQueryable();

            if (filter.Brands != null && filter.Brands.Any())
            {
                var kebabCaseBrands = filter.Brands.Select(b => ToKebabCase(b)).ToList();
                query = query.Where(a => filter.Brands.Contains(a.Brand) || filter.Brands.Contains(a.Brand.ToLower().Replace(" ", "-")));
            }

            if (filter.Models != null && filter.Models.Any())
            {
                var kebabCaseModels = filter.Models.Select(m => ToKebabCase(m)).ToList();
                query = query.Where(a => filter.Models.Contains(a.Model) || filter.Models.Contains(a.Model.ToLower().Replace(" ", "-")));
            }

            if (filter.MinYear.HasValue)
            {
                query = query.Where(a => a.ProductionYear >= filter.MinYear.Value);
            }

            if (filter.MaxYear.HasValue)
            {
                query = query.Where(a => a.ProductionYear <= filter.MaxYear.Value);
            }

            if (filter.MinMileage.HasValue)
            {
                query = query.Where(a => a.Mileage >= filter.MinMileage.Value);
            }

            if (filter.MaxMileage.HasValue)
            {
                query = query.Where(a => a.Mileage <= filter.MaxMileage.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(a => a.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(a => a.Price <= filter.MaxPrice.Value);
            }


            if (filter.BodyTypes != null && filter.BodyTypes.Any())
            {
                query = query.Where(a => filter.BodyTypes.Contains(a.BodyType));
            }


            if (filter.MinPower.HasValue)
            {
                query = query.Where(a => a.Power >= filter.MinPower.Value);
            }

            if (filter.MaxPower.HasValue)
            {
                query = query.Where(a => a.Power <= filter.MaxPower.Value);
            }
            if (filter.MultimediaFeatures != null && filter.MultimediaFeatures.Any())
            {
                foreach (var featureId in filter.MultimediaFeatures)
                {
                    query = query.Where(a => a.Multimedia.Any(m => m.featureId == featureId));
                }
            }

            if (filter.SafetyFeatures != null && filter.SafetyFeatures.Any())
            {
                foreach (var featureId in filter.SafetyFeatures)
                {
                    query = query.Where(a => a.Safety.Any(s => s.featureId == featureId));
                }
            }

            if (filter.DriverAssistanceSystemsFeatures != null && filter.DriverAssistanceSystemsFeatures.Any())
            {
                foreach (var featureId in filter.DriverAssistanceSystemsFeatures)
                {
                    query = query.Where(a => a.DriverAssistanceSystems.Any(d => d.featureId == featureId));
                }
            }

            if (filter.PerformanceFeatures != null && filter.PerformanceFeatures.Any())
            {
                foreach (var featureId in filter.PerformanceFeatures)
                {
                    query = query.Where(a => a.Performance.Any(p => p.featureId == featureId));
                }
            }

            if (filter.OtherFeatures != null && filter.OtherFeatures.Any())
            {
                foreach (var featureId in filter.OtherFeatures)
                {
                    query = query.Where(a => a.Other.Any(o => o.featureId == featureId));
                }
            }

            var announcements = await query.ToListAsync();

            var projectedResults = announcements.Select(a => new AnnPreview
            {
                Id = a.AnId,
                Slug = a.Slug,
                Brand = a.Brand,
                Model = a.Model,
                Description = a.Description,
                summary = a.Summary,
                User = new UserDto
                {
                    UserId = a.User.UserId,
                    Name = a.User.Name,
                    Surname = a.User.Surname,
                    Phone = a.User.Phone,
                    Email = a.User.Email,
                    lan = a.User.lat,
                    lng = a.User.lng,
                    Voivodeship = a.User.Voivodeship,
                    City = a.User.City,
                },
                Price = a.Price,
                Power = a.Power,
                Engine = a.Engine ?? string.Empty,
                FuelType = a.FuelType,
                Mileage = a.Mileage,
                ProductionYear = a.ProductionYear,
                LikedBy = _dbContext.FavoriteAnnouncements
                            .Where(fa => fa.AnnouncementAnId == a.AnId)
                            .Select(fa => fa.UserId)
                            .ToList(), 
                Images = a.Images.Select(i => new AnnouncementImagesDto
                {
                    AnId = i.AnId,
                    ImageUrl = i.ImageUrl
                }).ToList()
            }).ToList();

            return Ok(projectedResults);
        }



        [HttpPost("getFilteredAnnCount")]
        public async Task<IActionResult> FilterAnnouncementsCount([FromBody] FilterRequest filter)
        {
            var query = _dbContext.Announcements
                .AsNoTracking()
                .Include(a => a.User)
                .Include(a => a.Images)
                .AsQueryable();

            if (filter.Brands != null && filter.Brands.Any())
            {
                var kebabCaseBrands = filter.Brands.Select(b => ToKebabCase(b)).ToList();
                query = query.Where(a => filter.Brands.Contains(a.Brand) || filter.Brands.Contains(a.Brand.ToLower().Replace(" ", "-")));
            }

            if (filter.Models != null && filter.Models.Any())
            {
                var kebabCaseModels = filter.Models.Select(m => ToKebabCase(m)).ToList();
                query = query.Where(a => filter.Models.Contains(a.Model) || filter.Models.Contains(a.Model.ToLower().Replace(" ", "-")));
            }

            if (filter.MinYear.HasValue)
            {
                query = query.Where(a => a.ProductionYear >= filter.MinYear.Value);
            }

            if (filter.MaxYear.HasValue)
            {
                query = query.Where(a => a.ProductionYear <= filter.MaxYear.Value);
            }

            if (filter.MinMileage.HasValue)
            {
                query = query.Where(a => a.Mileage >= filter.MinMileage.Value);
            }

            if (filter.MaxMileage.HasValue)
            {
                query = query.Where(a => a.Mileage <= filter.MaxMileage.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(a => a.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(a => a.Price <= filter.MaxPrice.Value);
            }

            if (filter.BodyTypes != null && filter.BodyTypes.Any())
            {
                query = query.Where(a => filter.BodyTypes.Contains(a.BodyType));
            }

            if (filter.MinPower.HasValue)
            {
                query = query.Where(a => a.Power >= filter.MinPower.Value);
            }

            if (filter.MaxPower.HasValue)
            {
                query = query.Where(a => a.Power <= filter.MaxPower.Value);
            }

            if (filter.MultimediaFeatures != null && filter.MultimediaFeatures.Any())
            {
                foreach (var featureId in filter.MultimediaFeatures)
                {
                    query = query.Where(a => a.Multimedia.Any(m => m.featureId == featureId));
                }
            }

            if (filter.SafetyFeatures != null && filter.SafetyFeatures.Any())
            {
                foreach (var featureId in filter.SafetyFeatures)
                {
                    query = query.Where(a => a.Safety.Any(s => s.featureId == featureId));
                }
            }

            if (filter.DriverAssistanceSystemsFeatures != null && filter.DriverAssistanceSystemsFeatures.Any())
            {
                foreach (var featureId in filter.DriverAssistanceSystemsFeatures)
                {
                    query = query.Where(a => a.DriverAssistanceSystems.Any(d => d.featureId == featureId));
                }
            }

            if (filter.PerformanceFeatures != null && filter.PerformanceFeatures.Any())
            {
                foreach (var featureId in filter.PerformanceFeatures)
                {
                    query = query.Where(a => a.Performance.Any(p => p.featureId == featureId));
                }
            }

            if (filter.OtherFeatures != null && filter.OtherFeatures.Any())
            {
                foreach (var featureId in filter.OtherFeatures)
                {
                    query = query.Where(a => a.Other.Any(o => o.featureId == featureId));
                }
            }

            var results = await query
                .Select(a => new AnnPreview
                {
                    Id = a.AnId,
                    Slug = a.Slug,
                    Brand = a.Brand,
                    Model = a.Model,
                    Description = a.Description,
                    summary = a.Summary,
                    User = new UserDto
                    {
                        UserId = a.User.UserId,
                        Name = a.User.Name,
                        Surname = a.User.Surname,
                        Phone = a.User.Phone,
                        Email = a.User.Email,
                        lan = a.User.lat,
                        lng = a.User.lng,
                        Voivodeship = a.User.Voivodeship,
                        City = a.User.City,
                    },
                    Price = a.Price,
                    Power = a.Power,
                    Engine = a.Engine ?? string.Empty,
                    FuelType = a.FuelType,
                    Mileage = a.Mileage,
                    ProductionYear = a.ProductionYear,
                    
                    Images = a.Images.Select(i => new AnnouncementImagesDto
                    {
                        AnId = i.AnId,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                })
                .ToListAsync();

            return Ok(results.Count);
        }




            [HttpGet("suggest-brands")]
        public async Task<IActionResult> SuggestBrands([FromQuery] string term)
        {

            if (string.IsNullOrEmpty(term))
            {
                var brands = await _dbContext.Brands.ToListAsync();
                var brandDtos = _mapper.Map<List<BrandOrModelOrTypeSuggestionsDto>>(brands);
                return Ok(brandDtos);
            }
            else
            {
                var brands = await _dbContext.Brands
                    .Where(a => a.Name.Contains(term))
                    .ToListAsync();

                var brandDtos = _mapper.Map<List<BrandOrModelOrTypeSuggestionsDto>>(brands);
                return Ok(brandDtos);
            }

        }


        [HttpGet("suggest-models")]
        public async Task<IActionResult> SuggestModels([FromQuery] string brand, [FromHeader] string term)
        {
            if (string.IsNullOrEmpty(brand))
            {
                return BadRequest("Brand is required");
            }

            string value = brand.ToLower().Replace(" ", "-");

            var brandId = await _dbContext.Brands
                .Where(f => f.Name == brand || f.Name.ToLower().Replace(" ", "-") == value)
                .Select(m => m.BrandId)
                .FirstOrDefaultAsync();

            if (brandId == 0) 
            {
                return NotFound("Brand not found.");
            }

            if (string.IsNullOrEmpty(term))
            {
                var models = await _dbContext.Models
                    .Where(m => m.BrandId == brandId)
                    .ToListAsync();

                var modelDtos = _mapper.Map<List<BrandOrModelOrTypeSuggestionsDto>>(models);
                return Ok(modelDtos);
            }
            else
            {
                var models = await _dbContext.Models
                    .Where(m => m.Name.Contains(term) && m.BrandId == brandId)
                    .ToListAsync();

                var modelDtos = _mapper.Map<List<BrandOrModelOrTypeSuggestionsDto>>(models);
                return Ok(modelDtos);
            }

        }

        [HttpGet("suggest-bodytype")]
        public async Task<IActionResult> SuggestBodyType()
        {
            var bodytypes = await _dbContext.BodyTypes
                .ToListAsync();

            var results = _mapper.Map<List<BrandOrModelOrTypeSuggestionsDto>> (bodytypes);

            return Ok(results);
        }

        [HttpGet("suggest-fueltype")]
        public async Task<IActionResult> SuggestFuelType()
        {
            var fueltypes = await _dbContext.FuelTypes
                .ToListAsync();

            var results = _mapper.Map<List<BrandOrModelOrTypeSuggestionsDto>>(fueltypes);

            return Ok(results);
        }


        [HttpGet("suggest-multimedia-features")]
        public async Task<IActionResult> SuggestMultimediaFeatures()
        {


            var suggestions = await _dbContext.MultimediaDataset
            .Select(a => new FeatureSuggestionsDto
            {
                id = a.Id,
                label = a.feature
            })
            .Distinct()
            .ToListAsync();

            return Ok(suggestions);
        }
        [HttpGet("suggest-safety-features")]
        public async Task<IActionResult> SuggestSafetyFeatures()
        {


            var suggestions = await _dbContext.SafetyDataset
            .Select(a => new FeatureSuggestionsDto
            {
                id = a.Id,
                label = a.feature
            })
            .Distinct()
            .ToListAsync();

            return Ok(suggestions);
        }

        [HttpGet("suggest-driver-assistance-features")]
        public async Task<IActionResult> SuggestDriverAssistanceFeatures()
        {

            var suggestions = await _dbContext.DriverAssistanceSystemsDataset
            .Select(a => new FeatureSuggestionsDto
            {
                id = a.Id,
                label = a.feature
            })
            .Distinct()
            .ToListAsync();

            return Ok(suggestions);
        }


        [HttpGet("suggest-performance-features")]
        public async Task<IActionResult> SuggestPerformanceFeatures()
        {


            var suggestions = await _dbContext.PerformanceDataset
            .Select(a => new FeatureSuggestionsDto
            {
                id = a.Id,
                label = a.feature
            })
            .Distinct()
            .ToListAsync();

            return Ok(suggestions);
        }


        [HttpGet("suggest-other-features")]
        public async Task<IActionResult> SuggestOtherFeatures()
        {


            var suggestions = await _dbContext.OtherDataset
            .Select(a => new FeatureSuggestionsDto
            {
                id = a.Id,
                label = a.feature
            })
            .Distinct()
            .ToListAsync();

            return Ok(suggestions);
        }


    }
}
