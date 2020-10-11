using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.API
{
    public class UserRatingResponse
    {
        [JsonPropertyName("userRatings")]
        public List<UserRating> UserRatings { get; set; }
    }
}
