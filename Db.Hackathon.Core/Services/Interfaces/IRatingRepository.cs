using Db.Hackathon.Core.API;
using Db.Hackathon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Services.Interfaces
{
    public interface IRatingRepository
    {
        // Rating table.
        public Task AddOrUpdateRatingAsync(RatingEntity rating);

        public Task<List<RatingEntity>> GetKeywordUsersAsync(string keyword);

        public Task<List<TrendingKeyword>> GetTrendingKeywordsAsync();

        public Task<List<TrendingExpert>> GetTrendingExpertsAsync();

        public Task<AnalyticsEntity> GetLastUserAnalyticsRecordAsync(string username);

        // Analytics table.
        public Task AddUserAnalyticsRecord(AnalyticsEntity newUserAnalyticsRecord);

        public Task UpdateUserAnalyticsRecord(AnalyticsEntity lastUserAnalyticsRecord);

        public Task<AnalyticsEntity> GetUserCasesAsync(string username);

        public Task<UserCasesTotalResponse> GetUserCasesTotalAsync();

        public Task<List<UserCasesTotalResponse>> GetUserCasesTotalByMonthAsync();
    }
}
