using CoreMasterDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMasterDetails.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Experience> Experiences { get; set; }
    }
}
