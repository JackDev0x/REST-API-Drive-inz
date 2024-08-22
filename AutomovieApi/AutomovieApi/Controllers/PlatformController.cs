using AutoMapper;
using AutomovieApi.Entities;
using AutomovieApi.Models;
using AutomovieApi.Models.Post;
using AutomovieApi.Services;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using RestSharp;
using System.Formats.Asn1;
using System.Globalization;
using System.Security.Claims;

namespace AutomovieApi.Controllers
{
    [Route("api/platform")]
    public class PlatformController : ControllerBase
    {

        private readonly IAutomovieServices _automovieServices;
        public PlatformController(IAutomovieServices automovieServices)
        {
            _automovieServices = automovieServices;
        }

        [HttpGet("getAnnById/{id}")]
        public async Task<IActionResult> GetAnnouncement([FromRoute] int id)
        {
            var announcementDto = await _automovieServices.GetById(id);


            return Ok(announcementDto);
        }



        [HttpGet("getAnnBySlug/{slug}")]
        public async Task<IActionResult> GetAnnouncementBySlug([FromRoute] string slug)
        {
            var announcementDto = await _automovieServices.GetBySlug(slug);


            return Ok(announcementDto);
        }

        [HttpGet("getUsrById/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var announcementDto = await _automovieServices.GetUsrById(id);


            return Ok(announcementDto);
        }

        [HttpGet("getCommentsByAnnId/{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comments = await _automovieServices.GetCommentsByAnnId(id);


            return Ok(comments);
        }

        [HttpGet("getAnnByUsrId/{id}")]
        public async Task<IActionResult> GetAnnByUsrId([FromRoute] int id)
        {
            var anss = await _automovieServices.GetAnnByUsrId(id);


            return Ok(anss);
        }

        [HttpGet("GetFvAnnsByUsrId/{id}")]
        public async Task<IActionResult> GetFvAnnsByUsrId([FromRoute] int id)
        {
            var comments = await _automovieServices.GetFvAnnsByUsrId(id);


            return Ok(comments);
        }

        [Authorize]
        [HttpPost("CreateAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement([FromForm] AnnouncementCreateRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            

                var announcement = await _automovieServices.CreateAnn(request, userId);

                return Ok(announcement);

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

        [Authorize]
        [HttpDelete("DeleteAnnouncement/{AnnId}")]
        public async Task<IActionResult> DeleteAnnouncement(int AnnId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _automovieServices.DeleteAnnouncementAsync(AnnId, userId);

            if (!result)
            {
                return NotFound(new { Message = "Announcement not found or user not authorized." });
            }

            return Ok(new { Message = "Announcement deleted successfully." });
        }


        [Authorize]
        [HttpPost("AddComment")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddComment([FromBody] CommentCreateRequest commentCreateRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var comment = await _automovieServices.CreateCom(commentCreateRequest, userId);

            return Ok(comment);
        }


        [Authorize]
        [HttpPost("AddAnnToFavorites")]
        public async Task<IActionResult> AddAnnToFavorites([FromQuery] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var (isSuccess, message) = await _automovieServices.AddToFavAnn(userId, id);

            if (isSuccess)
            {
                return Ok(message);
            }

            return BadRequest(message);
        }

        [Authorize]
        [HttpDelete("DeleteAnnFromFavorites/{AnnId}")]
        public async Task<IActionResult> DeleteAnnFromFavorites(int AnnId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            
                var result = await _automovieServices.DeleteAnnFromFavorites(AnnId, userId);

                if (!result)
                {
                    return NotFound(new { Message = "Announcement not found or user not authorized." });
                }

                return Ok(new { Message = "Announcement deleted successfully from your favorites." }); 
        }


        [Authorize]
        [HttpDelete("DeleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId) {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var isDeleted = await _automovieServices.DeleteCom(commentId, int.Parse(userId));

            if (!isDeleted)
            {
                return Forbid();
            }

            return Ok();
        }



        public class CarData
        {
            public string Automaker { get; set; }
            public int Automaker_ID { get; set; }
            public string Genmodel { get; set; }
            public string Genmodel_ID { get; set; }
        }

    }
}