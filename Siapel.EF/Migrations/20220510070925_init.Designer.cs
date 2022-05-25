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
    [Migration("20220510070925_init")]
    partial class init
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
                            Nama = "UMUM",
                            Perma = true,
                            Status = "Aktif"
                        },
                        new
                        {
                            Id = 2,
                            Nama = "WIJAYA",
                            Perma = false,
                            Status = "Aktif"
                        },
                        new
                        {
                            Id = 3,
                            Nama = "NIHAYAH",
                            Perma = false,
                            Status = "Aktif"
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

                    b.Property<int>("PangkalanId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("Tanggal")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("TanggalLunas")
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