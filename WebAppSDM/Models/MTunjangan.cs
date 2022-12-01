using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSDM.Models
{
    public class MTunjangan
    {
        [Key]
        public int id { get; set; }
        public string kode_tunjangan { get; set; }
        public string nama_tunjangan { get; set; }
        public Decimal nominal { get; set; }
        public int? grade { get; set; }
        public int? jabatan { get; set; }
        public bool daily { get; set; }
        public int isdelete { get; set; }
        public DateTime create_date { get; set; }
    }

    [Table("dbo.ViewTunjangan")]
    public class ViewTunjangan
    {
        [Key]
        public int id { get; set; }
        public string kode_tunjangan { get; set; }
        public string nama_tunjangan { get; set; }
        public Decimal nominal { get; set; }
        public string grade { get; set; }
        public string nama_jabatan { get; set; }
        public bool daily { get; set; }
    }
}
