using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class TDivisi
    {
        [Key]
        public int id { get; set; }
        public int id_divisi { get; set; }
        public int nip { get; set; }
        public DateTime createdate { get; set; }
        public int isdelete { get; set; }
    }
    public class ViewDivisi
    {
        [Key]
        public int id { get; set; }
        public string? divisi_name { get; set; }
        public int totalkaryawan { get; set; }
    }
    public class TDivisiDetail
    {
        [Key]
        public int id { get; set; }
        public int id_divisi { get; set; }
        public string? divisi_name { get; set; }
        public string? nama { get; set; }
        public int nip { get; set; }
    }
}
