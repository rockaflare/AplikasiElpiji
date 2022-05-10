using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class Harga
    {
        public int Id { get; set; }
        public Pangkalan Pangkalan { get; set; }
        public int TbLimaPuluh { get; set; }
        public int TbDuaBelas { get; set; }
        public int TbLimaSetengah { get; set; }
        public DateTime TanggalUbah { get; set; }
    }
}
