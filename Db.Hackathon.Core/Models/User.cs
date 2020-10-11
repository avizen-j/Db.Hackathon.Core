using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Models
{
    public class User
    {
        public Guid IdentificationNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int ConfluencePages { get; set; }

        public string Rating { get; set; }
    }
}
