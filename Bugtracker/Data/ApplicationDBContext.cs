using Bugtracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bugtracker.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<TicketModel> Tickets { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Severity> Severities { get; set; }
    }
}
