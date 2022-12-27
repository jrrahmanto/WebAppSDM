using System.ComponentModel.DataAnnotations;

namespace WebAppSDM.Models
{
    public class TMutasi
    {
        [Key]
        public int id { get; set; }
        public int NIP { get; set; }
        public int grade_old { get; set; }
        public int grade_new { get; set; }
        public int jabatan_old { get; set; }
        public int jabatan_new { get; set; }
        public DateTime date_mutasi { get; set; }
        public string? no_sk { get; set; }
        public string? keterangan { get; set; }
        public int isdelete { get; set; }
        public DateTime createdate { get; set; }
        public string jenis_mutasi { get; set; }
        public Decimal gapok_old { get; set; }
        public Decimal gapok_new { get; set; }
    }
    public class ViewTMutasi
    {
        [Key]
        public int id { get; set; }
        public int NIP { get; set; }
        public int grade_old { get; set; }
        public int grade_new { get; set; }
        public int jabatan_old { get; set; }
        public int jabatan_new { get; set; }
        public DateTime date_mutasi { get; set; }
        public string? no_sk { get; set; }
        public string? keterangan { get; set; }
        public int isdelete { get; set; }
        public DateTime createdate { get; set; }
        public string jenis_mutasi { get; set; }
        public Decimal gapok_old { get; set; }
        public Decimal gapok_new { get; set; }
        public string nama { get; set; }
        public string grade_new_name { get; set; }
        public string grade_old_name { get; set; }
        public string jabatan_new_name { get; set; }
        public string jabatan_old_name { get; set; }
    }

}
