﻿// <auto-generated />
using System;
using Epizon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Epizon.Migrations
{
    [DbContext(typeof(EpizonContext))]
    partial class EpizonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Epizon.Models.Articolo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Categoria")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Immagine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Immagine3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImmagineCopertina")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrdineId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Prezzo")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("RivenditoreId")
                        .HasColumnType("int");

                    b.Property<int?>("TempiDiSpedizione")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("OrdineId");

                    b.HasIndex("RivenditoreId");

                    b.ToTable("Articoli");
                });

            modelBuilder.Entity("Epizon.Models.Ordine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompratoreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataOrdine")
                        .HasColumnType("datetime2");

                    b.Property<int>("RivenditoreId")
                        .HasColumnType("int");

                    b.Property<decimal>("Totale")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CompratoreId");

                    b.HasIndex("RivenditoreId");

                    b.ToTable("Ordini");
                });

            modelBuilder.Entity("OrdineArticolo", b =>
                {
                    b.Property<int>("OrdineId")
                        .HasColumnType("int");

                    b.Property<int>("ArticoloId")
                        .HasColumnType("int");

                    b.Property<int>("Quantità")
                        .HasColumnType("int");

                    b.HasKey("OrdineId", "ArticoloId");

                    b.HasIndex("ArticoloId");

                    b.ToTable("OrdineArticoli");
                });

            modelBuilder.Entity("Utente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Ruolo")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Utente");

                    b.HasDiscriminator<string>("Ruolo").HasValue("Utente");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Epizon.Models.Compratore", b =>
                {
                    b.HasBaseType("Utente");

                    b.Property<string>("CAP")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Citta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Indirizzo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.ToTable("Utente", t =>
                        {
                            t.Property("CAP")
                                .HasColumnName("Compratore_CAP");

                            t.Property("Citta")
                                .HasColumnName("Compratore_Citta");

                            t.Property("Cognome")
                                .HasColumnName("Compratore_Cognome");

                            t.Property("Indirizzo")
                                .HasColumnName("Compratore_Indirizzo");

                            t.Property("Nome")
                                .HasColumnName("Compratore_Nome");

                            t.Property("Provincia")
                                .HasColumnName("Compratore_Provincia");

                            t.Property("Telefono")
                                .HasColumnName("Compratore_Telefono");
                        });

                    b.HasDiscriminator().HasValue("Compratore");
                });

            modelBuilder.Entity("Epizon.Models.Rivenditore", b =>
                {
                    b.HasBaseType("Utente");

                    b.Property<string>("CAP")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Citta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CodiceDestinatario")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Indirizzo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PartitaIva")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Pec")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RagioneSociale")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasDiscriminator().HasValue("Rivenditore");
                });

            modelBuilder.Entity("Epizon.Models.Articolo", b =>
                {
                    b.HasOne("Epizon.Models.Ordine", "Ordine")
                        .WithMany("Articoli")
                        .HasForeignKey("OrdineId");

                    b.HasOne("Epizon.Models.Rivenditore", "Rivenditore")
                        .WithMany("Articoli")
                        .HasForeignKey("RivenditoreId");

                    b.Navigation("Ordine");

                    b.Navigation("Rivenditore");
                });

            modelBuilder.Entity("Epizon.Models.Ordine", b =>
                {
                    b.HasOne("Epizon.Models.Compratore", "Compratore")
                        .WithMany("OrdiniEffettuati")
                        .HasForeignKey("CompratoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Epizon.Models.Rivenditore", "Rivenditore")
                        .WithMany("OrdiniRicevuti")
                        .HasForeignKey("RivenditoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compratore");

                    b.Navigation("Rivenditore");
                });

            modelBuilder.Entity("OrdineArticolo", b =>
                {
                    b.HasOne("Epizon.Models.Articolo", "Articolo")
                        .WithMany()
                        .HasForeignKey("ArticoloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Epizon.Models.Ordine", "Ordine")
                        .WithMany()
                        .HasForeignKey("OrdineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articolo");

                    b.Navigation("Ordine");
                });

            modelBuilder.Entity("Epizon.Models.Ordine", b =>
                {
                    b.Navigation("Articoli");
                });

            modelBuilder.Entity("Epizon.Models.Compratore", b =>
                {
                    b.Navigation("OrdiniEffettuati");
                });

            modelBuilder.Entity("Epizon.Models.Rivenditore", b =>
                {
                    b.Navigation("Articoli");

                    b.Navigation("OrdiniRicevuti");
                });
#pragma warning restore 612, 618
        }
    }
}
