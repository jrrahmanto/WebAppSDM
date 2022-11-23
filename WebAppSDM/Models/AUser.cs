using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class AUser
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int role_id { get; set; }
        public int isactive { get; set; }
        public string nip { get; set; }
    }
}
