using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSDM.Models
{
    public class MTunjanganPerson
    {
        [Key]
        public int id { get; set; }
        public int nip { get; set; }
        public string nama_tunjangan { get; set; }
        public Decimal nominal { get; set; }
        public int isdelete { get; set; }
        public DateTime createddate { get; set; }

    }
    [Table("dbo.ViewMTunjanganPerson")]
    public class ViewMTunjanganPerson
    {
        [Key]
        public int id { get; set; }
        public string nama { get; set; }
        public string nama_tunjangan { get; set; }
        public Decimal nominal { get; set; }
        public int isdelete { get; set; }
    }
}
