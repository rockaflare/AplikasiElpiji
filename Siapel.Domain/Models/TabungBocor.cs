using Siptanos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class TabungBocor
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public Item Item { get; set; }
        public int Titipan { get; set; }
        public int Ambil { get; set; }
        public string Keterangan { get; set; }
    }
}
