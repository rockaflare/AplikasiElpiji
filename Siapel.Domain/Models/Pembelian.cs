using System;

namespace Siapel.Domain.Models
{
    public class Pembelian
    {
        public int Id { get; set; }
        public DateTime TanggalBeli { get; set; }
        public Item Item { get; set; }
        public int Jumlah { get; set; }
        public int Total { get; set; }
    }
}
