using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Models
{
    public class Pemasukan
    {
        public int Id { get; set; }
        public DateTimeOffset? Tanggal { get; set; }
        public int InLimaPuluh { get; set; }
        public int InDuaBelas { get; set; }
        public int InLimaSetengah { get; set; }

    }
}
