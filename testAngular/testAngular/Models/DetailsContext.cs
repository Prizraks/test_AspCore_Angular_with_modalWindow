using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testAngular.Models
{
    public class DetailsContext : DbContext
    {
        public DbSet<Detail> Details { get; set; }
        public DbSet<Storekeeper> Storekeepers { get; set; }
        public DetailsContext(DbContextOptions<DetailsContext> options)
            : base(options)
        {
        }
    }
}
