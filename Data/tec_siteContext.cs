using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace tec_site.Data
{
    public class tec_siteContext : IdentityDbContext
    {
        public tec_siteContext(DbContextOptions<tec_siteContext> options)
            : base(options)
        {
        }

        public DbSet<Models.ApplicationUser> users => Set<Models.ApplicationUser>();
    }
}
