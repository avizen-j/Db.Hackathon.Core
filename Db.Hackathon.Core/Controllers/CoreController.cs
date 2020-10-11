using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Db.Hackathon.Core.API;
using Db.Hackathon.Core.Entities;
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
                await _ratingService.AddUserKeywordRatingAsync(userRating);
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

        // Set/Update analytics for the user in order to track monthly activity of Received/Solved cases.
        [HttpPost("requestExpertHelp")]
        public async Task<IActionResult> RequestExpertHelp([FromBody]UserRating userRating)
        {
            try
            {
                if (userRating == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                    {
                        Message = "Provided user rating is null",
                    });
                }
                await _ratingService.SetExpertReceivedCasesAsync(userRating.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                {
                    Message = "Error occured while setting user received cases.",
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
                return Ok(await _ratingService.GetTrendingKeywordsAsync());
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

        // Get all experts(users) and rating based on the keyword
        [HttpGet("users")]
        public async Task<IActionResult> GetKeywordUsers([FromQuery]string keyword)
        {
            try
            {
                var keywordUsers = await _ratingService.GetKeywordUsersAsync(keyword);
                if (keywordUsers.Count != 0)
                {
                    var serialisedKeywordUsers = new UserRatingResponse
                    {
                        UserRatings = keywordUsers,
                    };
                    return Ok(serialisedKeywordUsers);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                    {
                        Message = "No users found for provided keyword",
                    });
                }
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                {
                    Message = "No users found for provided keyword",
                });
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

        [HttpGet("getTrendingExperts")]
        public async Task<IActionResult> GetTrendingExperts()
        {
            try
            {
                var trendingExperts = await _ratingService.GetTrendingExpertsAsync();
                var serialisedKeywordUsers = new TrendingExpertsResponse
                {
                    TrendingExperts = trendingExperts,
                };

                return Ok(serialisedKeywordUsers);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                {
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured while getting trending experts",
                    Exception = ex.Message,
                });
            }
        }

        // Get user cases for provided username.
        [HttpGet("getUserCases")]
        public async Task<IActionResult> GetUserCasesAsync([FromQuery] string username)
        {
            try
            {
                var userCases = await _ratingService.GetUserCasesAsync(username);
                return Ok(userCases);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                {
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured while getting user keywords",
                    Exception = ex.Message,
                });
            }
        }

        // Get total user cases.
        [HttpGet("getTotalUserCases")]
        public async Task<IActionResult> GetUserCasesTotalAsync()
        {
            try
            {
                var userTotalCases = await _ratingService.GetUserCasesTotalAsync();
                return Ok(userTotalCases);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                {
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured while getting user keywords",
                    Exception = ex.Message,
                });
            }
        }

        // Get total user cases by month.
        [HttpGet("getTotalUserCasesByMonth")]
        public async Task<IActionResult> GetUserCasesTotalByMonthAsync()
        {
            try
            {
                var userTotalCases = await _ratingService.GetUserCasesTotalByMonthAsync();
                return Ok(userTotalCases);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse
                {
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Error occured while getting user keywords",
                    Exception = ex.Message,
                });
            }
        }
    }
}
