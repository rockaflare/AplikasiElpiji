using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siptanos.Domain.Models
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
