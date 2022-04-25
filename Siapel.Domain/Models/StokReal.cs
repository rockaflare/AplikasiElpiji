using Siptanos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class StokReal
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public string Jumlah { get; set; }
    }
}
