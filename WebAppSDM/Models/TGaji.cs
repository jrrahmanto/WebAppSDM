using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSDM.Models
{
    public class TGaji
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public int periode_year { get; set; }
        public string periode_month { get; set; }
        public Decimal gapok { get; set; }
        public Decimal tunjangan_tetap { get; set; }
        public Decimal tunjangan_harian { get; set; }
        public Decimal Gapok { get; set; }
        public Decimal tunjangan_lain { get; set; }
        public Decimal bpjs_ks { get; set; }
        public Decimal bpjs_tk { get; set; }
        public Decimal dplk { get; set; }
        public Decimal pph21 { get; set; }
        public Decimal potongan_koperasi { get; set; }
        public Decimal potongan_lain { get; set; }
        public Decimal thp1 { get; set; }
        public Decimal thp2 { get; set; }
        public Decimal nominal_upah { get; set; }
        public DateTime update_date { get; set; }
    }

    [Table("dbo.ViewGaji")]
    public class ViewGaji
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public int periode_year { get; set; }
        public string periode_month { get; set; }
        public Decimal gapok { get; set; }
        public Decimal tunjangan_tetap { get; set; }
        public Decimal tunjangan_harian { get; set; }
        public Decimal Gapok { get; set; }
        public Decimal tunjangan_lain { get; set; }
        public Decimal bpjs_ks { get; set; }
        public Decimal bpjs_tk { get; set; }
        public Decimal dplk { get; set; }
        public Decimal pph21 { get; set; }
        public Decimal potongan_koperasi { get; set; }
        public Decimal potongan_lain { get; set; }
        public Decimal thp1 { get; set; }
        public Decimal thp2 { get; set; }
        public Decimal nominal_upah { get; set; }
        public DateTime update_date { get; set; }
        public string Nama { get; set; }
    }
}
