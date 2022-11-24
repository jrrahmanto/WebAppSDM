using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class ARole
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int isdelete { get; set; }
    }
}
