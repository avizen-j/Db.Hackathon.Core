using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Db.Hackathon.Core.API;
using Db.Hackathon.Core.Models;
using Db.Hackathon.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Db.Hackathon.Core.Controllers
{
    [Route("api/[controller]")]
    public class CoreController : Controller
    {
        private readonly IRatingService _ratingService;
        private readonly ILogger<CoreController> _logger;
        private readonly static Random Random = new Random();

        public CoreController(
            IRatingService ratingService,
            ILogger<CoreController> logger)
        {
            _ratingService = ratingService;
            _logger = logger;
        }

        // Set rating for user based on keyword. 
        [HttpPost("setUserReview")]
        public async Task<IActionResult> SetUserRating([FromBody] UserRating userRating)
        {
            try
            {
                await _ratingService.AddUserKeywordRating(userRating);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured while setting user keyword rating",
                    Exception = ex.Message,
                });
            }
        }

        // Get searched keywords and their appearance count.
        [HttpGet("getTrendingKeywords")]
        public async Task<IActionResult> GetTrendingKeywords()
        {
            try
            {
                return Ok(await _ratingService.GetTrendingKeywords());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured while setting getting trending keywords",
                    Exception = ex.Message,
                });
            }
        }

        // Get all experts(users) based on the keyword
        [HttpPost("getKeywordUsers")]
        public async Task<IActionResult> GetKeywordUsers([FromBody] string keyword)
        {
            try
            {
                return Ok(await _ratingService.GetKeywordUsers(keyword));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured while getting users by keyword",
                    Exception = ex.Message,
                });
            }
        }

        // Get ratings for provided user list.
        [HttpPost("getUsersRating")]
        public async Task<IActionResult> GetUsersRating([FromBody] string[] userIds)
        {
            var userRatingList = new List<UserRating>();

            foreach (var userId in userIds)
            {
                userRatingList.Add(new UserRating
                {
                    Username = userId,
                    ContributionRating = Random.Next(1, 100),
                });
            }

            return Ok(JsonSerializer.Serialize(userRatingList));
        }
    }
}
