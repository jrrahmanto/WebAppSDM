using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MPotongan
    {
        [Key]
        public int id { get; set; }
        public string kode_potongan { get; set; }
        public string nama_potongan { get; set; }
        public Decimal nominal { get; set; }
        public Decimal rate_percent { get; set; }
        public int komponen { get; set; }
        public int isdelete { get; set; }
        public DateTime create_date { get; set; }
    }
}
