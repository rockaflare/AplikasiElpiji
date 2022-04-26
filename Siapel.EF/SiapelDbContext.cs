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

        public DbSet<Item> Item { get; set; }
        public DbSet<Pangkalan> Pangkalan { get; set; }
        public DbSet<TipePembayaran> Pembayaran { get; set; }
        public DbSet<Pembelian> Pembelian { get; set; }
        public DbSet<Penjualan> Penjualan { get; set; }
        public DbSet<Pengeluaran> Pengeluaran { get; set; }
        public DbSet<StokGudang> StokGudang { get; set; }
        public DbSet<StokReal> StokReal { get; set; }
        public DbSet<TabungBocor> TabungBocor { get; set; }
        public DbSet<TitipTabung> TitipTabung { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Nama = "12 KG", Harga = 195000 },
                new Item { Id = 2, Nama = "5.5 KG", Harga = 92000 },
                new Item { Id = 3, Nama = "50 KG", Harga = 250000 });
            modelBuilder.Entity<Pangkalan>().HasData(
                new Pangkalan { Id = 1, Nama = "Pangkalpaha", Status = "Aktif" },
                new Pangkalan { Id = 2, Nama = "Rajinpangkalpandai", Status = "Aktif" },
                new Pangkalan { Id = 3, Nama = "APMS", Status = "Aktif" });
            modelBuilder.Entity<TipePembayaran>().HasData(
                new TipePembayaran { Id = 1, Nama = "Tunai" },
                new TipePembayaran { Id = 2, Nama = "Transfer" },
                new TipePembayaran { Id = 3, Nama = "Invoice" });
        }
    }
}
