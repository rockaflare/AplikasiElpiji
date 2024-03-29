﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Siapel.EF;

#nullable disable

namespace Siapel.EF.Migrations
{
    [DbContext(typeof(SiapelDbContext))]
    [Migration("20220614070334_remove-transaksilog-only")]
    partial class removetransaksilogonly
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Siapel.Domain.Models.Harga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PangkalanId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TanggalUbah")
                        .HasColumnType("TEXT");

                    b.Property<int>("TbDuaBelas")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TbLimaPuluh")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TbLimaSetengah")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PangkalanId");

                    b.ToTable("Harga");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PangkalanId = 1,
                            TanggalUbah = new DateTime(2022, 6, 14, 15, 3, 34, 583, DateTimeKind.Local).AddTicks(2773),
                            TbDuaBelas = 195000,
                            TbLimaPuluh = 1012000,
                            TbLimaSetengah = 93000
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

                    b.Property<bool>("Perma")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pangkalan");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nama = "DEFAULT",
                            Perma = false,
                            Status = "Aktif"
                        },
                        new
                        {
                            Id = 2,
                            Nama = "WIJAYA",
                            Perma = true,
                            Status = "Aktif"
                        },
                        new
                        {
                            Id = 3,
                            Nama = "NIHAYAH",
                            Perma = true,
                            Status = "Aktif"
                        });
                });

            modelBuilder.Entity("Siapel.Domain.Models.Pemasukan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Jumlah")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("Tanggal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pemasukan");
                });

            modelBuilder.Entity("Siapel.Domain.Models.StokAwal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanEdit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Jumlah")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Tanggal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StokAwal");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanEdit = false,
                            Item = "50 KG",
                            Jumlah = 20,
                            Tanggal = new DateTimeOffset(new DateTime(2022, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            Id = 2,
                            CanEdit = false,
                            Item = "12 KG",
                            Jumlah = 220,
                            Tanggal = new DateTimeOffset(new DateTime(2022, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            Id = 3,
                            CanEdit = false,
                            Item = "5,5 KG",
                            Jumlah = 355,
                            Tanggal = new DateTimeOffset(new DateTime(2022, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("Siapel.Domain.Models.TabungBocor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ambil")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Keterangan")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("Tanggal")
                        .HasColumnType("TEXT");

                    b.Property<int>("Titipan")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("TabungBocor");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Transaksi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Harga")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Item")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("JenisBayar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Jumlah")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PangkalanId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Tanggal")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("TanggalLunas")
                        .HasColumnType("TEXT");

                    b.Property<int>("Total")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PangkalanId");

                    b.ToTable("Transaksi");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Harga", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Pangkalan", "Pangkalan")
                        .WithMany()
                        .HasForeignKey("PangkalanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pangkalan");
                });

            modelBuilder.Entity("Siapel.Domain.Models.Transaksi", b =>
                {
                    b.HasOne("Siapel.Domain.Models.Pangkalan", "Pangkalan")
                        .WithMany()
                        .HasForeignKey("PangkalanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pangkalan");
                });
#pragma warning restore 612, 618
        }
    }
}
