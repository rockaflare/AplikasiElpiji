using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class Transaksi
    {
        public int Id { get; set; }
        public DateOnly Tanggal { get; set; }
        public Pangkalan Pangkalan { get; set; }
        public string Item { get; set; }
        public int Harga { get; set; }
        public string JenisBayar { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
        public DateOnly TanggalLunas { get; set; }
    }
}
