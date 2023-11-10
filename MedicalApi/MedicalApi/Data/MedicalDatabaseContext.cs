using System;
using System.Collections.Generic;
using MedicalDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalDatabase.Data;

public partial class MedicalDatabaseContext : DbContext
{
    public MedicalDatabaseContext(DbContextOptions<MedicalDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<LabResult> LabResults { get; set; }

    public virtual DbSet<MedicalVisit> MedicalVisits { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Patientrecord> Patientrecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccId);

            entity.HasIndex(e => e.AccId, "IX_Accounts_Acc_ID").IsUnique();

            entity.Property(e => e.AccId).HasColumnName("Acc_ID");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DId);

            entity.ToTable("Doctor");

            entity.HasIndex(e => e.DId, "IX_Doctor_D_ID").IsUnique();

            entity.Property(e => e.DId).HasColumnName("D_ID");
            entity.Property(e => e.AccId).HasColumnName("Acc_ID");
            entity.Property(e => e.HId).HasColumnName("H_ID");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HId);

            entity.ToTable("Hospital");

            entity.HasIndex(e => e.HId, "IX_Hospital_H_ID").IsUnique();

            entity.Property(e => e.HId).HasColumnName("H_ID");
        });

        modelBuilder.Entity<LabResult>(entity =>
        {
            entity.HasKey(e => e.LbId);

            entity.HasIndex(e => e.LbId, "IX_LabResults_LB_ID").IsUnique();

            entity.Property(e => e.LbId).HasColumnName("LB_ID");
            entity.Property(e => e.MId).HasColumnName("M_ID");
        });

        modelBuilder.Entity<MedicalVisit>(entity =>
        {
            entity.HasKey(e => e.MId);

            entity.ToTable("MedicalVisit");

            entity.HasIndex(e => e.MId, "IX_MedicalVisit_M_ID").IsUnique();

            entity.Property(e => e.MId).HasColumnName("M_ID");
            entity.Property(e => e.DId).HasColumnName("D_ID");
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.HId).HasColumnName("H_ID");
            entity.Property(e => e.PId).HasColumnName("P_ID");
            entity.Property(e => e.PatientIdnumber).HasColumnName("PatientIDNUMBER");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PId);

            entity.ToTable("Patient");

            entity.HasIndex(e => e.Id, "IX_Patient_ID").IsUnique();

            entity.HasIndex(e => e.PId, "IX_Patient_P_ID").IsUnique();

            entity.Property(e => e.PId).HasColumnName("P_ID");
            entity.Property(e => e.AccId).HasColumnName("Acc_ID");
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Patientrecord>(entity =>
        {
            entity.HasKey(e => e.Prid);

            entity.Property(e => e.Prid).HasColumnName("PRID");
            entity.Property(e => e.Age).HasColumnName("AGE");
            entity.Property(e => e.Idnumber).HasColumnName("IDNUMBER");
            entity.Property(e => e.Name).HasColumnName("NAME");
            entity.Property(e => e.Surname).HasColumnName("SURNAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
