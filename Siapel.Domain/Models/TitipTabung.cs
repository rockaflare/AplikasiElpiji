using System;

namespace Siapel.Domain.Models
{
    public class TitipTabung
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public Pangkalan Pangkalan { get; set; }
        public Item Item { get; set; }
        public int Jumlah { get; set; }
    }
}
