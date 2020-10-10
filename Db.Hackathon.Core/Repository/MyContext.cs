using Db.Hackathon.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Db.Hackathon.Core.Repository
{
    public class MyContext : DbContext
    {
        public DbSet<RatingEntity> Ratings { get; set; }

        public MyContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
