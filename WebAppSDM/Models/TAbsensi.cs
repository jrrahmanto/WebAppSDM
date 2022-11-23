using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSDM.Models
{
    public class TAbsensi
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public DateTime? Jam_Masuk { get; set; }
        public DateTime? Jam_Keluar { get; set; }
        public string? Status { get; set; }
        public TimeSpan? Lembur { get; set; }
        public Decimal Nominal_Lembur { get; set; }
        public bool Hitung_Lembur { get; set; }
        public DateTime update_date { get; set; }
        public string? keterangan { get; set; }
    }
    [Table("dbo.ViewTAbsensi")]
    public class ViewTAbsensi
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public string Jam_Masuk { get; set; }
        public string Jam_Keluar { get; set; }
        public string Status { get; set; }
        public string Lembur { get; set; }
        public Decimal Nominal_Lembur { get; set; }
        public bool Hitung_Lembur { get; set; }
        public DateTime update_date { get; set; }
        public string nama { get; set; }
        public string keterangan { get; set; }
    }
}
