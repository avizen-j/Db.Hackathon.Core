using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.API
{
    public class TrendingExpertsResponse
    {
        [JsonPropertyName("users")]
        public List<TrendingExpert> TrendingExperts { get; set; }
    }
}
