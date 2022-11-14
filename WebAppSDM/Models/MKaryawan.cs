using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSDM.Models
{
    public class MKaryawan
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public string Nama { get; set; }
        public DateTime Tgl_Masuk { get; set; }
        public DateTime Tgl_Permanen { get; set; }
        public string NIK { get; set; }
        public string NPWP { get; set; }
        public string Alamat { get; set; }
        public int Status_Pajak { get; set; }
        public int Grade_id { get; set; }
        public int Jabatan_id { get; set; }
        public Decimal Gapok { get; set; }
        public int isdelete { get; set; }
        public DateTime create_date { get; set; }
    }
    [Table("dbo.ViewTunjangan")]
    public class ViewKaryawan
    {
        [Key]
        public int id { get; set; }
        public string NIP { get; set; }
        public string Nama { get; set; }
        public DateTime Tgl_Masuk { get; set; }
        public DateTime Tgl_Permanen { get; set; }
        public string NIK { get; set; }
        public string NPWP { get; set; }
        public string Alamat { get; set; }
        public int Status_Pajak { get; set; }
        public string Grade_id { get; set; }
        public string Jabatan_id { get; set; }
        public Decimal Gapok { get; set; }
        public int isdelete { get; set; }
    }
}
