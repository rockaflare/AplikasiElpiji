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
        public DbSet<Pemasukan> Pemasukan { get; set; }
        public DbSet<StokAwal> StokAwal { get; set; }
        public DbSet<TabungBocor> TabungBocor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pangkalan>().HasData(
                new Pangkalan { Id = 1, Nama = "DEFAULT", Status = "Aktif", Perma = false },
                new Pangkalan { Id = 2, Nama = "WIJAYA", Status = "Aktif", Perma = true },
                new Pangkalan { Id = 3, Nama = "NIHAYAH", Status = "Aktif", Perma = true });
            modelBuilder.Entity<Harga>().HasData(
                new { Id = 1, PangkalanId = 1, TbLimaPuluh = 1012000, TbDuaBelas = 195000, TbLimaSetengah = 93000, TanggalUbah = DateTime.Now}
                );
            modelBuilder.Entity<StokAwal>().HasData(
                new StokAwal { Id = 1, Tanggal = DateTimeOffset.Now.Date, Item = "50 KG", Jumlah = 20, CanEdit = false },
                new StokAwal { Id = 2, Tanggal = DateTimeOffset.Now.Date, Item = "12 KG", Jumlah = 220, CanEdit = false },
                new StokAwal { Id = 3, Tanggal = DateTimeOffset.Now.Date, Item = "5,5 KG", Jumlah = 355, CanEdit = false }
                );
        }
    }
}
