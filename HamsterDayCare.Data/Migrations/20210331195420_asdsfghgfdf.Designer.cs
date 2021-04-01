﻿// <auto-generated />
using System;
using HamsterDayCare.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HamsterDayCare.Data.Migrations
{
    [DbContext(typeof(HDCDbContext))]
    [Migration("20210331195420_asdsfghgfdf")]
    partial class asdsfghgfdf
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HamsterDayCare.Domain.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AccuredAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DayCareStayId")
                        .HasColumnType("int");

                    b.Property<int>("HamsterId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOfActivity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayCareStayId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.Cage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cages");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.DayCareLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("DayCareLog");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.DayCareStay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("datetime2");

                    b.Property<int>("CageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DayCareLogId")
                        .HasColumnType("int");

                    b.Property<int>("HamasterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayCareLogId");

                    b.ToTable("DayCareStays");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.ExerciseArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExerciseArea");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.Hamster", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("CageId")
                        .HasColumnType("int");

                    b.Property<int?>("ExerciseAreaId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("CageId");

                    b.HasIndex("ExerciseAreaId");

                    b.ToTable("Hamsters");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.Activity", b =>
                {
                    b.HasOne("HamsterDayCare.Domain.DayCareStay", null)
                        .WithMany("Activities")
                        .HasForeignKey("DayCareStayId");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.DayCareStay", b =>
                {
                    b.HasOne("HamsterDayCare.Domain.DayCareLog", null)
                        .WithMany("DayCareStays")
                        .HasForeignKey("DayCareLogId");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.Hamster", b =>
                {
                    b.HasOne("HamsterDayCare.Domain.Cage", "Cage")
                        .WithMany("Hamsters")
                        .HasForeignKey("CageId");

                    b.HasOne("HamsterDayCare.Domain.ExerciseArea", null)
                        .WithMany("Hamsters")
                        .HasForeignKey("ExerciseAreaId");

                    b.Navigation("Cage");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.Cage", b =>
                {
                    b.Navigation("Hamsters");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.DayCareLog", b =>
                {
                    b.Navigation("DayCareStays");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.DayCareStay", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("HamsterDayCare.Domain.ExerciseArea", b =>
                {
                    b.Navigation("Hamsters");
                });
#pragma warning restore 612, 618
        }
    }
}
