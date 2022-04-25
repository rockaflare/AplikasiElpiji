using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siptanos.Domain.Models
{
    public class Penjualan
    {
        public int Id { get; set; }
        public DateTime TanggalJual { get; set; }
        public Item Item { get; set; }
        public Pangkalan Pangkalan { get; set; }
        public int Jumlah { get; set; }
        public TipePembayaran Pembayaran { get; set; }
        public DateTime TanggalLunas { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
        public string Keterangan { get; set; }
    }
}
