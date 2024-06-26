﻿// <auto-generated />
using System;
using Cwiczenia6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cwiczenia6.Migrations
{
    [DbContext(typeof(ClinicContext))]
    [Migration("20240608070907_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cwiczenia6.Models.Doctor", b =>
                {
                    b.Property<int>("IdDoctor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDoctor"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PrescriptionIdPrescription")
                        .HasColumnType("int");

                    b.HasKey("IdDoctor");

                    b.HasIndex("PrescriptionIdPrescription");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Cwiczenia6.Models.Medicament", b =>
                {
                    b.Property<int>("IdMedicament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMedicament"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdMedicament");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("Cwiczenia6.Models.Patient", b =>
                {
                    b.Property<int>("IdPatient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPatient"));

                    b.Property<string>("Birthdate")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PrescriptionIdPrescription")
                        .HasColumnType("int");

                    b.HasKey("IdPatient");

                    b.HasIndex("PrescriptionIdPrescription");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Cwiczenia6.Models.Prescription", b =>
                {
                    b.Property<int>("IdPrescription")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPrescription"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DueDate")
                        .HasColumnType("date");

                    b.Property<int>("IdDoctor")
                        .HasColumnType("int");

                    b.Property<int>("IdPatient")
                        .HasColumnType("int");

                    b.HasKey("IdPrescription");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("Cwiczenia6.Models.PrescriptionMedicament", b =>
                {
                    b.Property<int>("IdMedicament")
                        .HasColumnType("int");

                    b.Property<int>("IdPrescription")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Dose")
                        .HasColumnType("int");

                    b.Property<int>("MedicamentsIdMedicament")
                        .HasColumnType("int");

                    b.Property<int>("PrescriptionsIdPrescription")
                        .HasColumnType("int");

                    b.HasKey("IdMedicament", "IdPrescription");

                    b.HasIndex("MedicamentsIdMedicament");

                    b.HasIndex("PrescriptionsIdPrescription");

                    b.ToTable("PrescriptionMedicaments");
                });

            modelBuilder.Entity("Cwiczenia6.Models.Doctor", b =>
                {
                    b.HasOne("Cwiczenia6.Models.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionIdPrescription")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("Cwiczenia6.Models.Patient", b =>
                {
                    b.HasOne("Cwiczenia6.Models.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionIdPrescription")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("Cwiczenia6.Models.PrescriptionMedicament", b =>
                {
                    b.HasOne("Cwiczenia6.Models.Medicament", "Medicaments")
                        .WithMany("PrescriptionMedicament")
                        .HasForeignKey("MedicamentsIdMedicament")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cwiczenia6.Models.Prescription", "Prescriptions")
                        .WithMany("PrescriptionMedicament")
                        .HasForeignKey("PrescriptionsIdPrescription")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicaments");

                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("Cwiczenia6.Models.Medicament", b =>
                {
                    b.Navigation("PrescriptionMedicament");
                });

            modelBuilder.Entity("Cwiczenia6.Models.Prescription", b =>
                {
                    b.Navigation("PrescriptionMedicament");
                });
#pragma warning restore 612, 618
        }
    }
}
