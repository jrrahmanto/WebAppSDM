using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MGrade
    {
        [Key]
        public int Id { get; set; }
        public string kode_grade { get; set; }
        public int grade { get; set; }
        public string nama_grade { get; set; }
        public int isdelete { get; set; }
    }
}
