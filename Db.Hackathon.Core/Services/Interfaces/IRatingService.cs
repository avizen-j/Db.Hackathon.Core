using Db.Hackathon.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Services.Interfaces
{
    public interface IRatingService
    {
        public Task AddUserKeywordRating(UserRating userRating);

        public Task<List<UserRating>> GetKeywordUsers(string keyword);
        public Task<List<TrendingKeyword>> GetTrendingKeywords();
    }
}
