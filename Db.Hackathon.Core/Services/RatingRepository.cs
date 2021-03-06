﻿using Db.Hackathon.Core.API;
using Db.Hackathon.Core.Entities;
using Db.Hackathon.Core.Models;
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

        // ------------------------ RATINGS ------------------------

        // Fetching existing data.
        public async Task<List<TrendingKeyword>> GetTrendingKeywordsAsync()
        {
            return await _context.Ratings.GroupBy(t => t.Keyword)
                                         .Select(t => new TrendingKeyword
                                         {
                                             Keyword = t.Key,
                                             KeywordAppearanceCount = t.Count()
                                         })
                                         .ToListAsync();
        }

        public async Task<List<TrendingExpert>> GetTrendingExpertsAsync()
        {
            return await _context.Ratings.GroupBy(x => x.Username)
                                         .Select(t => new TrendingExpert
                                         {
                                             Username = t.Key,
                                             ContributionRating = t.Sum(x => x.ContributionRating)
                                         })
                                         .OrderByDescending(t => t.ContributionRating)
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


        // ------------------------ ANALYTICS ------------------------

        public async Task<AnalyticsEntity> GetLastUserAnalyticsRecordAsync(string username)
        {
            return await _context.Analytics.Where(t => t.Username == username)
                                           .OrderByDescending(t => t.Period)
                                           .FirstOrDefaultAsync();
        }

        public async Task AddUserAnalyticsRecord(AnalyticsEntity newUserAnalyticsRecord)
        {
            _context.Add(newUserAnalyticsRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAnalyticsRecord(AnalyticsEntity lastUserAnalyticsRecord)
        {
            _context.Update(lastUserAnalyticsRecord);
            await _context.SaveChangesAsync();
        }

        public Task<AnalyticsEntity> GetUserCasesAsync(string username)
        {
            return _context.Analytics.Where(t => t.Username == username)
                                     .FirstOrDefaultAsync();
        }

        public async Task<UserCasesTotalResponse> GetUserCasesTotalAsync()
        {
            return await _context.Analytics.GroupBy(x => true)
                                           .Select(x => new UserCasesTotalResponse
                                           {
                                               ReceivedCases = x.Sum(t => t.ReceivedCases),
                                               SolvedCases = x.Sum(t => t.SolvedCases),
                                           })
                                           .FirstOrDefaultAsync();
        }

        public async Task<List<UserCasesTotalResponse>> GetUserCasesTotalByMonthAsync()
        {
            return await _context.Analytics.GroupBy(x => x.Period)
                                           .Select(x => new UserCasesTotalResponse
                                           {
                                               Period = x.Key,
                                               ReceivedCases = x.Sum(t => t.ReceivedCases),
                                               SolvedCases = x.Sum(t => t.SolvedCases),
                                           })
                                           .ToListAsync();
        }
    }
}
