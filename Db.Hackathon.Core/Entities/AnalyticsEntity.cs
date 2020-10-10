using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Entities
{
    public class AnalyticsEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; }

        public DateTime Period { get; set; }

        public int ReceivedCases { get; set; }

        public int SolvedCases { get; set; }
    }
}
