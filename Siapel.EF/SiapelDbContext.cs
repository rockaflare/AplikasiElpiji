using System;
using Microsoft.EntityFrameworkCore;
using Siapel.Domain.Models;

namespace Siapel.EF
{
    public class SiapelDbContext : DbContext
    {
        public SiapelDbContext()
        {

        }

        public SiapelDbContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Pangkalan> Pangkalan { get; set; }
        public DbSet<Harga> Harga { get; set; }
        public DbSet<Transaksi> Transaksi { get; set; }
        //public DbSet<Pengeluaran> Pengeluaran { get; set; }
        //public DbSet<StokGudang> StokGudang { get; set; }
        //public DbSet<StokReal> StokReal { get; set; }
        //public DbSet<TabungBocor> TabungBocor { get; set; }
        //public DbSet<TitipTabung> TitipTabung { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pangkalan>().HasData(
                new Pangkalan { Id = 1, Nama = "UMUM", Status = "Aktif", Perma = false },
                new Pangkalan { Id = 2, Nama = "WIJAYA", Status = "Aktif", Perma = true },
                new Pangkalan { Id = 3, Nama = "NIHAYAH", Status = "Aktif", Perma = true });
        }
    }
}
