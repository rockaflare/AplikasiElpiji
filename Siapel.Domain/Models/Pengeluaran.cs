using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class Pengeluaran
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public string Jenis { get; set; }
        public string Keterangan { get; set; }
        public int Jumlah { get; set; }
    }
}
