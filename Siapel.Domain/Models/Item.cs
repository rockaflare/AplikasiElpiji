using System.Collections.Generic;

namespace Siapel.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public int Harga { get; set; }

        public ICollection<Pembelian> Pembelian { get; set; }
        public ICollection<Penjualan> Penjualan { get; set; }
        public ICollection<TitipTabung> TitipTabung { get; set; }

    }
}
