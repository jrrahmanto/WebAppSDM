using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class MEmployee
    {
        [Key]
        public long id { get; set; }
        public int nip { get; set; }
        public string? nama { get; set; }
        public string? emp_pob { get; set; }
        public DateTime? emp_dob { get; set; }
        public string? emp_sex { get; set; }
        public string? emp_address_1 { get; set; }
        public string? emp_address_2 { get; set; }
        public string? emp_phone_1 { get; set; }
        public string? emp_phone_2 { get; set; }
        public string? emp_cellphone_1 { get; set; }
        public string? emp_cellphone_2 { get; set; }
        public string? emp_religion { get; set; }
        public string? emp_NPWP { get; set; }
        public string? emp_KTP { get; set; }
        public string? emp_passport { get; set; }
        public DateTime? emp_passportexp { get; set; }
        public string? emp_licenseA { get; set; }
        public DateTime? emp_licenseAexp { get; set; }
        public string? emp_licenseB { get; set; }
        public DateTime? emp_licenseBexp { get; set; }
        public string? emp_licenseC { get; set; }
        public DateTime? emp_licenseCexp { get; set; }
        public string? emp_marital { get; set; }
        public DateTime? emp_dom { get; set; }
        public string? emp_email { get; set; }
        public string? emp_aktif { get; set; }
        public string? emp_photo { get; set; }
        public string? emp_ptkp { get; set; }
        public string? emp_norek { get; set; }
        public string? emp_bank { get; set; }
        public string? emp_an { get; set; }
        public string? emp_status { get; set; }
        public string? emp_darah { get; set; }
        public int? grade_id { get; set; }
        public int? jabatan_id { get; set; }
        public Decimal? salary { get; set; }
        public DateTime? permanent_date { get; set; }
        public DateTime? contract_date { get; set; }
        public DateTime? date_of_entry { get; set; }
        public int? isdelete { get; set; }
    }
}
