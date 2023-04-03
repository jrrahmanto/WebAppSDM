using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class TAbsenKhusus
    {
        [Key]
        public int id { get; set; }
        public int nip { get; set; }
        public DateTime periode_start { get; set; }
        public DateTime periode_end { get; set; }
        public string keterangan { get; set; }
        public int isdelete { get; set; }
        public int? status { get; set; }
    }
    public class ViewAbsenKhusus
    {
        [Key]
        public int id { get; set; }
        public int nip { get; set; }
        public DateTime periode_start { get; set; }
        public DateTime periode_end { get; set; }
        public string keterangan { get; set; }
        public int isdelete { get; set; }
        public string nama { get; set; }
        public int status { get; set; }
    }
}