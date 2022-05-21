using System.Collections.Generic;

namespace Siapel.Domain.Models
{
    public class Pangkalan
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Status { get; set; }
        public bool Perma { get; set; }

        ICollection<Harga> Harga { get; set; }
    }
}
