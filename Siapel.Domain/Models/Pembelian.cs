using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siptanos.Domain.Models
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
