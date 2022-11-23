using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MParameter
    {
        [Key]
        public int id { get; set; }
        public string value { get; set; }
        public string keterangan { get; set; }
    }
}
