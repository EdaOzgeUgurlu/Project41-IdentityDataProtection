using Microsoft.EntityFrameworkCore;

namespace Project41_IdentityDataProtection.Models
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options : base,(options))
        {
            this.options = options;
        }
    }
}
