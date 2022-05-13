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
        public async Task<Pangkalan> Create(Pangkalan entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(Pangkalan entity)
        {
            return await _nonQueryDataService.Delete(entity);
        }

        public async Task<Pangkalan> Get(int id)
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                Pangkalan entity = await context.Pangkalan.FindAsync(id);
                return entity;
            }
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
