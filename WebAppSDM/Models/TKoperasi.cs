using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class TKoperasi
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public int Periode_year { get; set; }
        public string Periode_month { get; set; }
        public Decimal Cicilan { get; set; }
        public Decimal Tabungan { get; set; }
        public Decimal Iuran { get; set; }
        public DateTime update_date { get; set; }
        public int isdelete { get; set; }
    }
}
