﻿// <auto-generated />
using System;
using EmeraldChameleonChat.Services.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmeraldChameleonChat.Services.Migrations
{
    [DbContext(typeof(EmeraldChameleonChatContext))]
    partial class EmeraldChameleonChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EmeraldChameleonChat.Services.Model.Entity.ChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastConnection")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("isActive")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Chatroom");
                });

            modelBuilder.Entity("EmeraldChameleonChat.Services.Model.Entity.ChatRoomMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ChatRoomId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ChatRoomMessage");
                });

            modelBuilder.Entity("EmeraldChameleonChat.Services.Model.Entity.WeatherForecast", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<double>("TemperatureC")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("WeatherForecast");
                });
#pragma warning restore 612, 618
        }
    }
}
