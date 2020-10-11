using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.API
{
    public class TrendingKeyword
    {
        [JsonPropertyName("keyword")]
        public string Keyword { get; set; }

        [JsonPropertyName("keywordAppearanceCount")]
        public int KeywordAppearanceCount { get; set; }
    }
}
