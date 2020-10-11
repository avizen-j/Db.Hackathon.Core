using Db.Hackathon.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Services.Interfaces
{
    public interface IRatingService
    {
        public Task AddUserKeywordRatingAsync(UserRating userRating);

        public Task<List<UserRating>> GetKeywordUsersAsync(string keyword);

        public Task<List<TrendingKeyword>> GetTrendingKeywordsAsync();

        public Task<List<TrendingExpert>> GetTrendingExpertsAsync();

        public Task SetExpertReceivedCasesAsync(string username);

        public Task<UserCasesResponse> GetUserCasesAsync(string username);

        public Task<UserCasesTotalResponse> GetUserCasesTotalAsync();

        public Task<List<UserCasesTotalResponse>> GetUserCasesTotalByMonthAsync();
    }
}
