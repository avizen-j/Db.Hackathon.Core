using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.API
{
    public class UserRating
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("reviewScore")]
        public int ReviewScore { get; set; }

        [JsonPropertyName("contributionRating")]
        public int ContributionRating { get; set; }
    }
}
