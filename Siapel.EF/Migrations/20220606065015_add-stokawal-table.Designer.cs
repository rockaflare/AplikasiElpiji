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
    [Migration("20220606065015_add-stokawal-table")]
    partial class addstokawaltable
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
                            TanggalUbah = new DateTime(2022, 6, 6, 14, 50, 15, 587, DateTimeKind.Local).AddTicks(2151),
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

                    b.Property<int?>("InDuaBelas")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InLimaPuluh")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InLimaSetengah")
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

                    b.Property<int?>("InitDuaBelas")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InitLimaPuluh")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InitLimaSetengah")
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
                            InitDuaBelas = 50,
                            InitLimaPuluh = 20,
                            InitLimaSetengah = 100,
                            Tanggal = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0))
                        });
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
