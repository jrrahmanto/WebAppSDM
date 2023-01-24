using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MEmpEducations
    {
        [Key]
        public long edu_id { get; set; }
        public long nip { get; set; }
        public string? edu_year { get; set; }
        public string? edu_place { get; set; }
        public string? edu_strata { get; set; }
        public string? edu_faculty { get; set; }
        public string? edu_major { get; set; }
        public decimal? emp_gpa { get; set; }
        public string? emp_scholarship { get; set; }
        public string? edu_final { get; set; }
    }
    public class ViewEdu
    {
        [Key]
        public long edu_id { get; set; }
        public long nip { get; set; }
        public string? edu_year { get; set; }
        public string? edu_place { get; set; }
        public string? edu_strata { get; set; }
        public string? edu_faculty { get; set; }
        public string? edu_major { get; set; }
        public decimal? emp_gpa { get; set; }
        public string? emp_scholarship { get; set; }
        public string? edu_final { get; set; }
        public string nama { get; set; }
    }
}
