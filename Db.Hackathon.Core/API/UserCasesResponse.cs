using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.API
{
    public class UserCasesResponse
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("receivedCases")]
        public int ReceivedCases { get; set; }

        [JsonPropertyName("solvedCases")]
        public int SolvedCases { get; set; }

    }
}
