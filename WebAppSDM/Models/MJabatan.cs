using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MJabatan
    {
        [Key]
        public int id { get; set; }
        public string kode_jabatan { get; set; }
        public string nama_jabatan { get; set; }
        public int isdelete { get; set; }
    }
}
