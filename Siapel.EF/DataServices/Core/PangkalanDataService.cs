using Microsoft.EntityFrameworkCore;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.EF.DataServices.Core
{
    public class PangkalanDataService : IDataService<Pangkalan>
    {
        private readonly SiapelDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Pangkalan> _nonQueryDataService;
        public PangkalanDataService(SiapelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Pangkalan>(_contextFactory);
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

        public async Task<IEnumerable<Pangkalan>> GetAll()
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Pangkalan> entities = await context.Pangkalan.ToListAsync();
                return entities;
            }
        }

        public Task<Pangkalan> Update(Pangkalan entity)
        {
            throw new NotImplementedException();
        }
    }
}
