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

        public async Task<List<UserRating>> GetKeywordUsers(string keyword)
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

        public async Task AddUserKeywordRating(UserRating userRating)
        {
            var contributionRating = userRating.ReviewScore * 10;
            var ratingEntity = new RatingEntity
            {
                Username = userRating.Username,
                ContributionRating = contributionRating,
                Keyword = userRating.Keyword,
            };

            await _ratingRepository.AddOrUpdateRatingAsync(ratingEntity);
        }

        public async Task<List<TrendingKeyword>> GetTrendingKeywords()
        {
            var mappedTrendingKeywords = new List<TrendingKeyword>();
            List<TrendingKeyword> trendingKeywords = await _ratingRepository.GetTrendingKeywords();

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
    }
}
