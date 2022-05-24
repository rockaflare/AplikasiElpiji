using System;
using Siapel.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siapel.Domain.Models;
using Siapel.EF.Services;
using Microsoft.EntityFrameworkCore;

namespace Siapel.EF.DataServices.Core
{
    public class TransaksiDataService : ITransaksiDataService
    {
        private readonly SiapelDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Transaksi> _nonQueryDataService;

        public TransaksiDataService(SiapelDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Transaksi>(contextFactory);
        }

        public async Task<Transaksi> Create(Transaksi entity)
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                var pangkalan = context.Pangkalan.Single(x => x.Id == entity.Pangkalan.Id);
                var createdResult = await context.Transaksi.AddAsync(
                    new Transaksi
                    {
                        Tanggal = entity.Tanggal,
                        Pangkalan = pangkalan,
                        Item = entity.Item,
                        Harga = entity.Harga,
                        Jumlah = entity.Jumlah,
                        JenisBayar = entity.JenisBayar,
                        Total = entity.Total,
                        Status = entity.Status,
                        TanggalLunas = entity.TanggalLunas
                    }
                    );

                await context.SaveChangesAsync();
                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(Transaksi entity)
        {
            return await _nonQueryDataService.Delete(entity);
        }

        public Task<Transaksi> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Transaksi>> GetAll()
        {
            using (SiapelDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Transaksi> entities = await context.Transaksi.Include(t => t.Pangkalan).ToListAsync();
                return entities;
            }
        }

        public async Task<Transaksi> Update(Transaksi entity)
        {
            return await _nonQueryDataService.Update(entity);
        }
    }
}
