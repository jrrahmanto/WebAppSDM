using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MEmpFamily
    {
        [Key]
        public long fam_id { get; set; }
        public long nip { get; set; }
        public string? fam_relationship { get; set; }
        public string? fam_name { get; set; }
        public string? fam_sex { get; set; }
        public string? fam_pob { get; set; }
        public DateTime? fam_dob { get; set; }
        public string? fam_KTP { get; set; }
        public string? fam_contact { get; set; }
        public int? isdelete { get; set; }
        public DateTime? createddate { get; set; }
    }
    public class ViewEmpFamily
    {
        [Key]
        public long fam_id { get; set; }
        public long nip { get; set; }
        public string? fam_relationship { get; set; }
        public string? fam_name { get; set; }
        public string? fam_sex { get; set; }
        public string? fam_pob { get; set; }
        public DateTime? fam_dob { get; set; }
        public string? fam_KTP { get; set; }
        public string? fam_contact { get; set; }
        public int? isdelete { get; set; }
        public DateTime? createddate { get; set; }
        public string? nama { get; set; }
    }
}
