using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MHariLibur
    {
        [Key]
        public int id { get; set; }
        public DateTime tanggal { get; set; }
        public string keterangan { get; set; }
        public int isdelete { get; set; }
    }
}
