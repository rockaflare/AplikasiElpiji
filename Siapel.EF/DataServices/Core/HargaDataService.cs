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
    public class HargaDataService : IDataService<Harga>
    {
        private readonly SiapelDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Harga> _nonQueryDataService;

        public HargaDataService(SiapelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Harga>(contextFactory);
        }

        public async Task<Harga> Create(Harga entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(Harga entity)
        {
            return await _nonQueryDataService.Delete(entity);
        }

        public Task<Harga> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Harga>> GetAll()
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Harga> entities = await context.Harga.Include(h => h.Pangkalan).ToListAsync();
                return entities;
            }
        }

        public Task<Harga> Update(Harga entity)
        {
            return _nonQueryDataService.Update(entity);
        }
    }
}
