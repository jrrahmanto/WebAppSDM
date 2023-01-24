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
        public DbSet<TAbsensi> TAbsensi { get; set; }
        public DbSet<TKoperasi> TKoperasi { get; set; }
        public DbSet<MMesinAbsen> MMesinAbsen { get; set; }
        public DbSet<MParameter> MParameter { get; set; }
        public DbSet<ARole> ARole { get; set; }
        public DbSet<MTunjanganPerson> MTunjanganPerson { get; set; }
        public DbSet<AUser> AUser { get; set; }
        public DbSet<MEmployee> MEmployee { get; set; }
        public DbSet<TMutasi> TMutasi { get; set; }
        public DbSet<ViewKaryawan> ViewKaryawan { get; set; }
        public DbSet<ViewTunjangan> ViewTunjangan { get; set; }
        public DbSet<ViewTAbsensi> ViewTAbsensi { get; set; }
        public DbSet<ViewGaji> ViewGaji { get; set; }
        public DbSet<DropdownList.GradeList> GradeList { get; set; }
        public DbSet<DropdownList.JabatanList> JabatanList { get; set; }
        public DbSet<DropdownList.KaryawanList> KaryawanLists { get; set; }
        public DbSet<MListTunjangan> MListTunjangan { get; set; }
        public DbSet<ViewMTunjanganPerson> ViewMTunjanganPerson { get; set; }
        public DbSet<MHariLibur> MHariLibur { get; set; }
        public DbSet<TAbsenKhusus> TAbsenKhusus { get; set; }
        public DbSet<MPotonganPerson> MPotonganPerson { get; set; }
        public DbSet<TGajis> TGajis { get; set; }
        public DbSet<ViewPotonganPerson> ViewPotonganPerson { get; set; }
        public DbSet<ViewTMutasi> ViewTMutasi { get; set; }
        public DbSet<ViewMasterAbsen> ViewMasterAbsen { get; set; }
        public DbSet<ViewAbsenKhusus> ViewAbsenKhusus { get; set; }
        public DbSet<MDivisi> MDivisi { get; set; }
        public DbSet<TDivisi> TDivisi { get; set; }
        public DbSet<ViewDivisi> ViewDivisi { get; set; }
        public DbSet<TDivisiDetail> TDivisiDetail { get; set; }
        public DbSet<MEmpFamily> MEmpFamily { get; set; }
        public DbSet<ViewEmpFamily> ViewEmpFamily { get; set; }
        public DbSet<MEmpEducations> MEmpEducations { get; set; }
        public DbSet<ViewEdu> ViewEdu { get; set; }
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
            modelBuilder.Entity<ViewTAbsensi>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewTAbsensi");
                entity.Property(e => e.nama).HasMaxLength(50);
            });
            modelBuilder.Entity<DropdownList.KaryawanList>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("KaryawanList");
                entity.Property(e => e.nama).HasMaxLength(50);
            });
            modelBuilder.Entity<ViewGaji>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewGaji");
                entity.Property(e => e.Nama).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewMTunjanganPerson>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewMTunjanganPerson");
                entity.Property(e => e.nama).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewPotonganPerson>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewPotonganPerson");
                entity.Property(e => e.nama).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewTMutasi>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewTMutasi");
                entity.Property(e => e.nama).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewMasterAbsen>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewMasterAbsen");
                entity.Property(e => e.nama).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewAbsenKhusus>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewAbsenKhusus");
                entity.Property(e => e.nama).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewDivisi>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("ViewDivisi");
                entity.Property(e => e.divisi_name).HasMaxLength(100);
            });
            modelBuilder.Entity<TDivisiDetail>(entity => {
                entity.HasKey(e => e.id);
                entity.ToTable("TDivisiDetail");
                entity.Property(e => e.divisi_name).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewEmpFamily>(entity => {
                entity.HasKey(e => e.fam_id);
                entity.ToTable("ViewEmpFamily");
                entity.Property(e => e.nama).HasMaxLength(100);
            });
            modelBuilder.Entity<ViewEdu>(entity => {
                entity.HasKey(e => e.edu_id);
                entity.ToTable("ViewEdu");
                entity.Property(e => e.nama).HasMaxLength(100);
            });
        }
    }
}
