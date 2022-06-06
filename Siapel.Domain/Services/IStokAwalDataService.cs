using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Services
{
    public interface IStokAwalDataService : IDataService<StokAwal>
    {
        Task<StokAwal> GetByTanggal(DateTimeOffset tanggal);
    }
}
