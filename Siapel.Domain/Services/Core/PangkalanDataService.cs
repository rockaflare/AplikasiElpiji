using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.Domain.Services.Core
{
    public class PangkalanDataService : IDataService<Pangkalan>
    {
        private readonly IDataService<Pangkalan> _dataService;
        public PangkalanDataService()
        {
            
        }
        public Task<Pangkalan> Create(Pangkalan entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Pangkalan entity)
        {
            throw new NotImplementedException();
        }

        public Task<Pangkalan> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pangkalan>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Pangkalan> Update(Pangkalan entity)
        {
            throw new NotImplementedException();
        }
    }
}
