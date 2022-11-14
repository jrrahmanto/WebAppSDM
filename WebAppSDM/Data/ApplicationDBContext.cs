using Microsoft.EntityFrameworkCore;
using WebAppSDM.Models;
using static WebAppSDM.Models.DropdownList;

namespace WebAppSDM.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<MGrade> MGrade { get; set; }
        public DbSet<MJabatan> MJabatan { get; set; }
        public DbSet<MTunjangan> MTunjangan { get; set; }
        public DbSet<MPotongan> MPotongan { get; set; }
        public DbSet<MKaryawan> MKaryawan { get; set; }
        public DbSet<ViewKaryawan> ViewKaryawan { get; set; }
        public DbSet<ViewTunjangan> ViewTunjangan { get; set; }
        public DbSet<DropdownList.GradeList> GradeList { get; set; }
        public DbSet<DropdownList.JabatanList> JabatanList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewTunjangan>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewTunjangan");
                entity.Property(e => e.nama_tunjangan).HasMaxLength(50);
            });

            modelBuilder.Entity<DropdownList.GradeList>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("GradeList");
                entity.Property(e => e.nama).HasMaxLength(50);
            });
            modelBuilder.Entity<DropdownList.JabatanList>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("JabatanList");
                entity.Property(e => e.nama).HasMaxLength(50);
            });
            modelBuilder.Entity<ViewKaryawan>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewKaryawan");
                entity.Property(e => e.Nama).HasMaxLength(50);
            });
        }
    }
}
