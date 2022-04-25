using Siptanos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public  class StokGudang
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public Item Item { get; set; }
        public int TabungIsi { get; set; }
        public int TabungKosong { get; set; }
        public int DalamTruk { get; set; }

    }
}
