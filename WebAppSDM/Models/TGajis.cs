using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSDM.Models
{
    public class TGajis
    {
        [Key]
        public int id { get; set; }
        public DateTime periode_start { get; set; }
        public DateTime periode_end { get; set; }
        public Decimal gapok { get; set; }
        public Decimal tunj_tetap { get; set; }
        public Decimal tunj_komunikasi { get; set; }
        public Decimal tunj_konjungtur { get; set; }
        public Decimal tunj_harian { get; set; }
        public Decimal tunj_structural { get; set; }
        public Decimal tunj_perumahan { get; set; }
        public Decimal tunj_penyesuaian { get; set; }
        public Decimal tunj_kinerja { get; set; }
        public Decimal tunj_sc { get; set; }
        public Decimal total { get; set; }
        public DateTime createddate { get; set; }
        public int nip { get; set; }
        public Decimal Tunj_lain { get; set; }
        public Decimal pot_koperasi { get; set; }
        public Decimal pot_parkiran { get; set; }
        public Decimal pot_dim { get; set; }
        public Decimal pot_lain { get; set; }
        public Decimal tunj_pph { get; set; }
        public Decimal tunj_jamsostek { get; set; }
        public Decimal tunj_pensiun { get; set; }
        public Decimal tunj_bpjs { get; set; }
        public Decimal pot_jamsostek { get; set; }
        public Decimal pot_bpjs { get; set; }
        public Decimal pot_dplk { get; set; }
        public int jumlah_masuk { get; set; }
        public int jumlah_terlambat { get; set; }
        public Decimal pot_tvs { get; set; }
    }
    [Table("dbo.ViewGaji")]
    public class ViewGaji
    {
        [Key]
        public int id { get; set; }
        public string Nama { get; set; }
        public int nip { get; set; }
        public DateTime periode_start { get; set; }
        public DateTime periode_end { get; set; }
        public Decimal total { get; set; }
    }
}
