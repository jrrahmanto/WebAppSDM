using Microsoft.EntityFrameworkCore;
using WebAppSDM.Models;

namespace WebAppSDM.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<MGrade> MGrade { get; set; }
        public DbSet<MJabatan> MJabatan { get; set; }
    }
}
