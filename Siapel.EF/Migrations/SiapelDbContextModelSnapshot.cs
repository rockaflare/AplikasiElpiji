﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Siapel.EF;

#nullable disable

namespace Siapel.EF.Migrations
{
    [DbContext(typeof(SiapelDbContext))]
    partial class SiapelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Siapel.Domain.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Harga")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Item");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Harga = 195000,
                            Nama = "12 KG"
                        },
                        new
                        {
                            Id = 2,
                            Harga = 92000,
                            Nama = "5.5 KG"
                        },
                        new
                        {
                            Id = 3,
                            Harga = 250000,
                            Nama = "50 KG"
                        });
                });

            modelBuilder.Entity("Siapel.Domain.Models.Pangkalan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pangkalan");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nama = "Pangkalpaha",
                            Status = "Aktif"
                        },
                        new
                        {
                            Id = 2,
                            Nama = "Rajinpangkalpandai",
                            Status = "Aktif"
                        },
                        new
                        {
                            Id = 3,
                            Nama = "APMS",
                            Status = "Aktif"
                        });
                });

            modelBuilder.Entity("Siapel.Domain.Models.Pembelian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Jumlah")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TanggalBeli")
                        .HasColumnType("TEXT");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Pembelian");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Pengeluaran", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Jenis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Jumlah")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Keterangan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pengeluaran");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Penjualan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Jumlah")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Keterangan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PangkalanId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PembayaranId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TanggalJual")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TanggalLunas")
                        .HasColumnType("TEXT");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PangkalanId");

                    b.HasIndex("PembayaranId");

                    b.ToTable("Penjualan");
                });

            modelBuilder.Entity("Siapel.Domain.Models.StokGudang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DalamTruk")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TabungIsi")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TabungKosong")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("StokGudang");
                });

            modelBuilder.Entity("Siapel.Domain.Models.StokReal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Jumlah")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("StokReal");
                });

            modelBuilder.Entity("Siapel.Domain.Models.TabungBocor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ambil")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Keterangan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("TEXT");

                    b.Property<int>("Titipan")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("TabungBocor");
                });

            modelBuilder.Entity("Siapel.Domain.Models.TipePembayaran", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pembayaran");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nama = "Tunai"
                        },
                        new
                        {
                            Id = 2,
                            Nama = "Transfer"
                        },
                        new
                        {
                            Id = 3,
                            Nama = "Invoice"
                        });
                });

            modelBuilder.Entity("Siapel.Domain.Models.TitipTabung", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Jumlah")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PangkalanId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Tanggal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PangkalanId");

                    b.ToTable("TitipTabung");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Pembelian", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Item", "Item")
                        .WithMany("Pembelian")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Penjualan", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Item", "Item")
                        .WithMany("Penjualan")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Siapel.Domain.Models.Pangkalan", "Pangkalan")
                        .WithMany()
                        .HasForeignKey("PangkalanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Siapel.Domain.Models.TipePembayaran", "Pembayaran")
                        .WithMany()
                        .HasForeignKey("PembayaranId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Pangkalan");

                    b.Navigation("Pembayaran");
                });

            modelBuilder.Entity("Siapel.Domain.Models.StokGudang", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Siapel.Domain.Models.StokReal", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Siapel.Domain.Models.TabungBocor", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Siapel.Domain.Models.TitipTabung", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Item", "Item")
                        .WithMany("TitipTabung")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Siapel.Domain.Models.Pangkalan", "Pangkalan")
                        .WithMany()
                        .HasForeignKey("PangkalanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Pangkalan");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Item", b =>
                {
                    b.Navigation("Pembelian");

                    b.Navigation("Penjualan");

                    b.Navigation("TitipTabung");
                });
#pragma warning restore 612, 618
        }
    }
}