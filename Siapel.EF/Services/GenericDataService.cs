using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siapel.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Siapel.EF.Services
{
    public class GenericDataService<T> : IDataService<T> where T : class
    {
        private readonly SiapelDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(SiapelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(T entity)
        {
            return await (_nonQueryDataService.Delete(entity));
        }

        public async Task<T> Get(int id)
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FindAsync(id);

                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();

                return entities;
            }
        }

        public async Task<T> Update(T entity)
        {
            return await _nonQueryDataService.Update(entity);
        }
    }
}
