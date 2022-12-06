using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MListTunjangan
    {
        [Key]
        public int id { get; set; }
        public string nama { get; set; }
        public string keterangan { get; set; }
        public int isdelete { get; set; }
    }
}
