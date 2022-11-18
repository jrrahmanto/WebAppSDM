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
            public int id { get; set; }
            public string nama { get; set; }
            public string NIP { get; set; }
        }
    }
}
