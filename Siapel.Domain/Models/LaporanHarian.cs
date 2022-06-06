using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class LaporanHarian
    {
        public string Item { get; set; }
        public int Jumlah { get; set; }
        public string Harga { get; set; }
        public string Tunai { get; set; }
        public string Transfer { get; set; }
        public string Invoice { get; set; }
        public string Total { get; set; }
        public int TunaiInt { get; set; }
        public int TransferInt { get; set; }
        public int InvoiceInt { get; set; }
        public int TotalInt { get; set; }
    }
}
