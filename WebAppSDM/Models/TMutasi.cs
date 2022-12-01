using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class TMutasi
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public int grade_old { get; set; }
        public int grade_new { get; set; }
        public int jabatan_old { get; set; }
        public int jabatan_new { get; set; }
        public DateTime date_mutasi { get; set; }
        public string no_sk { get; set; }
        public string keterangan { get; set; }
        public int isdelete { get; set; }
        public DateTime createdate { get; set; }
    }
}
