using System;

namespace Siapel.Domain.Models
{
    public class TabungBocor
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }
        public string Item { get; set; }
        public int Titipan { get; set; }
        public int Ambil { get; set; }
        public string Keterangan { get; set; }
    }
}
