using Db.Hackathon.Core.API;
using Db.Hackathon.Core.Entities;
using Db.Hackathon.Core.Repository;
using Db.Hackathon.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Services
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MyContext _context;
        private readonly ILogger<RatingRepository> _logger;

        public RatingRepository(
            MyContext context,
            ILogger<RatingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Fetching existing data.
        public async Task<List<TrendingKeyword>> GetTrendingKeywords()
        {
            return await _context.Ratings.GroupBy(t => t.Keyword)
                                         .Select(t => new TrendingKeyword
                                         {
                                             Keyword = t.Key,
                                             KeywordAppearanceCount = t.Count()
                                         })
                                         .ToListAsync();
        }

        public async Task<List<RatingEntity>> GetKeywordUsersAsync(string keyword)
        {
            return await _context.Ratings.Where(t => t.Keyword == keyword)
                                         .OrderBy(t => t.ContributionRating)
                                         .ToListAsync();
        }

        // Adding and updating rating info.
        public async Task AddOrUpdateRatingAsync(RatingEntity rating)
        {
            var userRating = _context.Ratings.Where(t => t.Username == rating.Username && t.Keyword == rating.Keyword)
                                             .FirstOrDefault();

            if (userRating == null)
            {
                _logger.LogInformation($"User with keyword was not in the list. Creating new record...");
                _context.Add(rating);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogInformation($"User {userRating.Username} with keyword {userRating.Keyword} was found. Updating existing record...");
                userRating.ContributionRating += rating.ContributionRating;
                await _context.SaveChangesAsync();
            }
        }
    }
}
