using Microsoft.EntityFrameworkCore;
using Siapel.Domain.Models;
using Siapel.Domain.Services.MasterServices;
using Siapel.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.EF.DataServices
{
    public class ItemDataService : IItemService
    {
        private readonly SiapelDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Item> _nonQueryDataService;

        public ItemDataService(SiapelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Item>(contextFactory);
        }

        public async Task<Item> Create(Item entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(Item entity)
        {
            return await _nonQueryDataService.Delete(entity);
        }

        public Task<Item> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Item> entities = await context.Item.ToListAsync();
                return entities;
            }
        }

        public Task<Item> Update(Item entity)
        {
            throw new NotImplementedException();
        }
    }
}
