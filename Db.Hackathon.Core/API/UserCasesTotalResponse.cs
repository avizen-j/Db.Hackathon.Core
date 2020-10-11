using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.API
{
    public class UserCasesTotalResponse
    {
        [JsonPropertyName("totalReceivedCases")]
        public int ReceivedCases { get; set; }

        [JsonPropertyName("totalSolvedCases")]
        public int SolvedCases { get; set; }

        [JsonPropertyName("startPeriod")]
        public DateTime Period { get; set; }
    }
}
