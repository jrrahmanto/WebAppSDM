using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MDivisi
    {
        [Key]
        public int id { get; set; }
        public string divisi_name { get; set; }
        public DateTime created_date { get; set; }
        public int is_delete { get; set; }
    }
}
