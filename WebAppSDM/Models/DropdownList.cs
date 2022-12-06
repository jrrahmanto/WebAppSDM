namespace WebAppSDM.Models
{
    public class DropdownList
    {
        public class GradeList
        {
            public int id { get; set; }
            public string nama { get; set; }
        }
        public class JabatanList
        {
            public int id { get; set; }
            public string nama { get; set; }
        }
        public class KaryawanList
        {
            public long id { get; set; }
            public string nama { get; set; }
            public int NIP { get; set; }
        }
    }
}
