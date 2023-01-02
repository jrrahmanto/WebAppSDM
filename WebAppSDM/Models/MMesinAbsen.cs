using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MMesinAbsen
    {
        [Key]
        public int id { get; set; }
        public int id_mechine { get; set; }
        public string badge_number { get; set; }
        public string NIP { get; set; }
        public int isdelete { get; set; }
    }
    public class ViewMasterAbsen
    {
        [Key]
        public int id { get; set; }
        public int id_mechine { get; set; }
        public string badge_number { get; set; }
        public string NIP { get; set; }
        public int isdelete { get; set; }
        public string nama { get; set; }
    }
}
