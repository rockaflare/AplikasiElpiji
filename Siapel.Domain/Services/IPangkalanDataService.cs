using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Services
{
    public interface IPangkalanDataService : IDataService<Pangkalan>
    {
        Task<IEnumerable<Pangkalan>> GetByName(string name);
    }
}
