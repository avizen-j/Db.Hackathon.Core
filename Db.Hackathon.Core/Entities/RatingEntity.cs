using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Entities
{
    public class RatingEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; }

        public string Keyword { get; set; }

        public int ContributionRating { get; set; }
    }
}
