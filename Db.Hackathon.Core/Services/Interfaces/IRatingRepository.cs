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
        public Task AddOrUpdateRatingAsync(RatingEntity rating);

        public Task<List<RatingEntity>> GetKeywordUsersAsync(string keyword);

        public Task<List<TrendingKeyword>> GetTrendingKeywords();
    }
}
