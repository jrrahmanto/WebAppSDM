using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MPotonganPerson
    {
        [Key]
        public int id { get; set; }
        public int nip { get; set; }
        public Decimal? tvs { get; set; }
        public Decimal? potongan_parkiran { get; set; }
        public Decimal? potongan_dim { get; set; }
        public Decimal? potongan_lain { get; set; }
        public string periode { get; set; }
        public bool dplk { get; set; }
        public int isdelete { get; set; }
        public int flag_execute { get; set; }
        public Decimal? potongan_koperasi { get; set; }
    }
    public class ViewPotonganPerson
    {
        [Key]
        public int id { get; set; }
        public int nip { get; set; }
        public Decimal? tvs { get; set; }
        public Decimal? potongan_parkiran { get; set; }
        public Decimal? potongan_dim { get; set; }
        public Decimal? potongan_lain { get; set; }
        public string periode { get; set; }
        public bool dplk { get; set; }
        public int isdelete { get; set; }
        public int flag_execute { get; set; }
        public Decimal? potongan_koperasi { get; set; }
        public string nama { get; set; }
    }
}
