﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class TransaksiLog
    {
        public int Id { get; set; }
        public DateTimeOffset Tanggal { get; set; }
        public string Item { get; set; }
        public string Tipe { get; set; }
        public int Jumlah { get; set; }
        public int SisaStok { get; set; }
    }
}