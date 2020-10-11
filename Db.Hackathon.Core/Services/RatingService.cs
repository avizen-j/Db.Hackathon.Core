using Db.Hackathon.Core.API;
using Db.Hackathon.Core.Entities;
using Db.Hackathon.Core.Models;
using Db.Hackathon.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<List<UserRating>> GetKeywordUsersAsync(string keyword)
        {
            var mappedUsers = new List<UserRating>();
            var users = await _ratingRepository.GetKeywordUsersAsync(keyword);

            if (users.Count != 0)
            {
                foreach (var user in users)
                {
                    mappedUsers.Add(new UserRating
                    {
                        Username = user.Username,
                        ContributionRating = user.ContributionRating,
                    });
                }

                return mappedUsers;
            }
            else
            {
                throw new InvalidOperationException($"Users for {keyword} not found.");
            }
        }

        public async Task AddUserKeywordRatingAsync(UserRating userRating)
        {
            var contributionRating = userRating.ReviewScore * 10;
            var ratingEntity = new RatingEntity
            {
                Username = userRating.Username,
                ContributionRating = contributionRating,
                Keyword = userRating.Keyword,
            };

            await SetExpertSolvedCasesAsync(userRating.Username);
            await _ratingRepository.AddOrUpdateRatingAsync(ratingEntity);
        }

        public async Task<List<TrendingKeyword>> GetTrendingKeywordsAsync()
        {
            var mappedTrendingKeywords = new List<TrendingKeyword>();
            List<TrendingKeyword> trendingKeywords = await _ratingRepository.GetTrendingKeywordsAsync();

            if (trendingKeywords.Count != 0)
            {
                foreach (var trendingKeyword in trendingKeywords)
                {
                    mappedTrendingKeywords.Add(new TrendingKeyword
                    {
                        Keyword = trendingKeyword.Keyword,
                        KeywordAppearanceCount = trendingKeyword.KeywordAppearanceCount,
                    });
                }

                return mappedTrendingKeywords;
            }
            else
            {
                throw new InvalidOperationException($"No trending keywords found.");
            }
        }

        public async Task<List<TrendingExpert>> GetTrendingExpertsAsync()
        {
            List<TrendingExpert> trendingExperts = await _ratingRepository.GetTrendingExpertsAsync();

            if (trendingExperts.Count != 0)
            {
                return trendingExperts;
            }
            else
            {
                throw new InvalidOperationException($"No trending experts found.");
            }
        }

        public async Task SetExpertReceivedCasesAsync(string username)
        {
            //await _ratingRepository.AddOrUpdateUserAnalytics(username);
            var receivedDate = DateTime.Now;
            var startOfPeriod = new DateTime(receivedDate.Year, receivedDate.Month, 1);
            var lastUserAnalyticsRecord = await _ratingRepository.GetLastUserAnalyticsRecordAsync(username);

            if (lastUserAnalyticsRecord != default)
            {
                lastUserAnalyticsRecord.ReceivedCases++;
                await _ratingRepository.UpdateUserAnalyticsRecord(lastUserAnalyticsRecord);
            }
            else
            {
                var newUserAnalyticsRecord = new AnalyticsEntity
                {
                    Username = username,
                    ReceivedCases = 1,
                    SolvedCases = 0,
                    Period = startOfPeriod,
                };
                await _ratingRepository.AddUserAnalyticsRecord(newUserAnalyticsRecord);
            }
        }

        public async Task SetExpertSolvedCasesAsync(string username)
        {
            //await _ratingRepository.AddOrUpdateUserAnalytics(username);
            var receivedDate = DateTime.Now;
            var startOfPeriod = new DateTime(receivedDate.Year, receivedDate.Month, 1);
            var lastUserAnalyticsRecord = await _ratingRepository.GetLastUserAnalyticsRecordAsync(username);

            if (lastUserAnalyticsRecord != default)
            {
                lastUserAnalyticsRecord.SolvedCases++;
                await _ratingRepository.UpdateUserAnalyticsRecord(lastUserAnalyticsRecord);
            }
            else
            {
                throw new InvalidOperationException($"Cannot increase '{username}' solved cases count because there is no received cases.");
            }
        }

        public async Task<UserCasesResponse> GetUserCasesAsync(string username)
        {
            var userAnalyticsRecord = await _ratingRepository.GetUserCasesAsync(username);

            if (userAnalyticsRecord != default)
            {
                var userCases = new UserCasesResponse
                {
                    Username = username,
                    ReceivedCases = userAnalyticsRecord.ReceivedCases,
                    SolvedCases = userAnalyticsRecord.SolvedCases,
                };

                return userCases;
            }
            else
            {
                throw new InvalidOperationException($"No cases for user found.");
            }
        }

        public async Task<UserCasesTotalResponse> GetUserCasesTotalAsync()
        {
            var userCasesTotalResponse = await _ratingRepository.GetUserCasesTotalAsync();

            return userCasesTotalResponse ?? throw new InvalidOperationException($"No total received/solved cases found.");
        }

        public async Task<List<UserCasesTotalResponse>> GetUserCasesTotalByMonthAsync()
        {
            var userCasesTotalResponseByMonth = await _ratingRepository.GetUserCasesTotalByMonthAsync();

            if (userCasesTotalResponseByMonth.Count != 0)
            {
                return userCasesTotalResponseByMonth;
            }
            else
            {
                throw new InvalidOperationException($"No total received/solved cases found by month.");
            }
        }
    }
}
