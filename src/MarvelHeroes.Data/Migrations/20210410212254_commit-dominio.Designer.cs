﻿// <auto-generated />
using System;
using MarvelHeroes.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarvelHeroes.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20210410212254_commit-dominio")]
    partial class commitdominio
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarvelHeroes.Business.Models.Personagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasMaxLength(2147483647)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id_marvel")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Pic_url")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Wiki_url")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.ToTable("personagem");
                });

            modelBuilder.Entity("MarvelHeroes.Business.Models.Quadrinho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasMaxLength(2147483647)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id_marvel")
                        .HasColumnType("int");

                    b.Property<string>("Pic_url")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<decimal?>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Wiki_url")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.ToTable("quadrinhos");
                });
#pragma warning restore 612, 618
        }
    }
}
