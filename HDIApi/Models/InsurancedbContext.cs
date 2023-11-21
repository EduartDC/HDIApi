using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HDIApi.Models;

public partial class InsurancedbContext : DbContext
{
    IConfiguration config = null;
    public InsurancedbContext(IConfiguration config)
    {
        this.config = config;
    }

    public InsurancedbContext(DbContextOptions<InsurancedbContext> options, IConfiguration config)
        : base(options)
    {
        this.config = config;
    }

    public virtual DbSet<Accident> Accidents { get; set; }

    public virtual DbSet<Carinvolved> Carinvolveds { get; set; }

    public virtual DbSet<Driverclient> Driverclients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Insurancepolicy> Insurancepolicies { get; set; }

    public virtual DbSet<Involved> Involveds { get; set; }

    public virtual DbSet<Opinionadjuster> Opinionadjusters { get; set; }

    public virtual DbSet<Vehicleclient> Vehicleclients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = config.GetConnectionString("mysql");

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Accident>(entity =>
        {
            entity.HasKey(e => e.IdAccident).HasName("PRIMARY");

            entity.ToTable("accident");

            entity.HasIndex(e => e.EmployeeIdEmployee, "fk_Accident_Employee1_idx");

            entity.HasIndex(e => e.OpinionAdjusterIdOpinionAdjuster, "fk_Accident_OpinionAdjuster1_idx");

            entity.HasIndex(e => e.DriverClientIdDriverClient, "fk_ReportAccident_DriverClient1_idx");

            entity.HasIndex(e => e.VehicleClientIdVehicleClient, "fk_ReportAccident_VehicleClient1_idx");

            entity.Property(e => e.IdAccident)
                .HasMaxLength(100)
                .HasColumnName("idAccident");
            entity.Property(e => e.AccidentDate)
                .HasColumnType("datetime")
                .HasColumnName("accidentDate");
            entity.Property(e => e.DriverClientIdDriverClient)
                .HasMaxLength(20)
                .HasColumnName("DriverClient_idDriverClient");
            entity.Property(e => e.EmployeeIdEmployee)
                .HasMaxLength(100)
                .HasColumnName("Employee_idEmployee");
            entity.Property(e => e.Location)
                .HasMaxLength(45)
                .HasColumnName("location");
            entity.Property(e => e.NameLocation)
                .HasMaxLength(150)
                .HasColumnName("nameLocation");
            entity.Property(e => e.OpinionAdjusterIdOpinionAdjuster)
                .HasMaxLength(100)
                .HasColumnName("OpinionAdjuster_idOpinionAdjuster");
            entity.Property(e => e.ReportStatus)
                .HasMaxLength(45)
                .HasColumnName("reportStatus");
            entity.Property(e => e.VehicleClientIdVehicleClient)
                .HasMaxLength(20)
                .HasColumnName("VehicleClient_idVehicleClient");

            entity.HasOne(d => d.DriverClientIdDriverClientNavigation).WithMany(p => p.Accidents)
                .HasForeignKey(d => d.DriverClientIdDriverClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ReportAccident_DriverClient1");

            entity.HasOne(d => d.EmployeeIdEmployeeNavigation).WithMany(p => p.Accidents)
                .HasForeignKey(d => d.EmployeeIdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Accident_Employee1");

            entity.HasOne(d => d.OpinionAdjusterIdOpinionAdjusterNavigation).WithMany(p => p.Accidents)
                .HasForeignKey(d => d.OpinionAdjusterIdOpinionAdjuster)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Accident_OpinionAdjuster1");

            entity.HasOne(d => d.VehicleClientIdVehicleClientNavigation).WithMany(p => p.Accidents)
                .HasForeignKey(d => d.VehicleClientIdVehicleClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ReportAccident_VehicleClient1");
        });

        modelBuilder.Entity<Carinvolved>(entity =>
        {
            entity.HasKey(e => e.IdCarInvolved).HasName("PRIMARY");

            entity.ToTable("carinvolved");

            entity.Property(e => e.IdCarInvolved)
                .HasMaxLength(100)
                .HasColumnName("idCarInvolved");
            entity.Property(e => e.Brand)
                .HasMaxLength(45)
                .HasColumnName("brand");
            entity.Property(e => e.Color)
                .HasMaxLength(45)
                .HasColumnName("color");
            entity.Property(e => e.Model)
                .HasMaxLength(45)
                .HasColumnName("model");
            entity.Property(e => e.Plate)
                .HasMaxLength(30)
                .HasColumnName("plate");
        });

        modelBuilder.Entity<Driverclient>(entity =>
        {
            entity.HasKey(e => e.IdDriverClient).HasName("PRIMARY");

            entity.ToTable("driverclient");

            entity.Property(e => e.IdDriverClient)
                .HasMaxLength(100)
                .HasColumnName("idDriverClient");
            entity.Property(e => e.DriverBirthday)
                .HasColumnType("datetime")
                .HasColumnName("driverBirthday");
            entity.Property(e => e.LastNameDriver)
                .HasMaxLength(100)
                .HasColumnName("lastNameDriver");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(30)
                .HasColumnName("licenseNumber");
            entity.Property(e => e.NameDriver)
                .HasMaxLength(100)
                .HasColumnName("nameDriver");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(10)
                .HasColumnName("telephoneNumber");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.Property(e => e.IdEmployee)
                .HasMaxLength(100)
                .HasColumnName("idEmployee");
            entity.Property(e => e.LastnameEmployee)
                .HasMaxLength(100)
                .HasColumnName("lastnameEmployee");
            entity.Property(e => e.NameEmployee)
                .HasMaxLength(100)
                .HasColumnName("nameEmployee");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("datetime")
                .HasColumnName("registrationDate");
            entity.Property(e => e.Rol)
                .HasMaxLength(45)
                .HasColumnName("rol");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Idimages).HasName("PRIMARY");

            entity.ToTable("image");

            entity.HasIndex(e => e.AccidentIdAccident, "fk_Image_Accident1_idx");

            entity.Property(e => e.Idimages)
                .HasMaxLength(100)
                .HasColumnName("idimages");
            entity.Property(e => e.AccidentIdAccident)
                .HasMaxLength(100)
                .HasColumnName("Accident_idAccident");
            entity.Property(e => e.ImageReport)
                .HasColumnType("blob")
                .HasColumnName("imageReport");

            entity.HasOne(d => d.AccidentIdAccidentNavigation).WithMany(p => p.Images)
                .HasForeignKey(d => d.AccidentIdAccident)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Image_Accident1");
        });

        modelBuilder.Entity<Insurancepolicy>(entity =>
        {
            entity.HasKey(e => e.IdInsurancePolicy).HasName("PRIMARY");

            entity.ToTable("insurancepolicy");

            entity.HasIndex(e => e.DriverClientIdDriverClient, "fk_InsurancePolicy_DriverClient1_idx");

            entity.Property(e => e.IdInsurancePolicy)
                .HasMaxLength(20)
                .HasColumnName("idInsurancePolicy");
            entity.Property(e => e.DriverClientIdDriverClient)
                .HasMaxLength(100)
                .HasColumnName("DriverClient_idDriverClient");
            entity.Property(e => e.EndTerm)
                .HasColumnType("datetime")
                .HasColumnName("endTerm");
            entity.Property(e => e.PolicyType)
                .HasMaxLength(45)
                .HasColumnName("policyType");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.StartTerm)
                .HasColumnType("datetime")
                .HasColumnName("startTerm");
            entity.Property(e => e.TermAmount).HasColumnName("termAmount");

            entity.HasOne(d => d.DriverClientIdDriverClientNavigation).WithMany(p => p.Insurancepolicies)
                .HasForeignKey(d => d.DriverClientIdDriverClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_InsurancePolicy_DriverClient1");
        });

        modelBuilder.Entity<Involved>(entity =>
        {
            entity.HasKey(e => e.IdInvolved).HasName("PRIMARY");

            entity.ToTable("involved");

            entity.HasIndex(e => e.AccidentIdAccident, "fk_Involved_Accident1_idx");

            entity.HasIndex(e => e.CarInvolvedIdCarInvolved, "fk_Involved_CarInvolved1_idx");

            entity.Property(e => e.IdInvolved)
                .HasMaxLength(100)
                .HasColumnName("idInvolved");
            entity.Property(e => e.AccidentIdAccident)
                .HasMaxLength(100)
                .HasColumnName("Accident_idAccident");
            entity.Property(e => e.CarInvolvedIdCarInvolved)
                .HasMaxLength(100)
                .HasColumnName("CarInvolved_idCarInvolved");
            entity.Property(e => e.LastNameInvolved)
                .HasMaxLength(100)
                .HasColumnName("lastNameInvolved");
            entity.Property(e => e.NameInvolved)
                .HasMaxLength(100)
                .HasColumnName("nameInvolved");

            entity.HasOne(d => d.AccidentIdAccidentNavigation).WithMany(p => p.Involveds)
                .HasForeignKey(d => d.AccidentIdAccident)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Involved_Accident1");

            entity.HasOne(d => d.CarInvolvedIdCarInvolvedNavigation).WithMany(p => p.Involveds)
                .HasForeignKey(d => d.CarInvolvedIdCarInvolved)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Involved_CarInvolved1");
        });

        modelBuilder.Entity<Opinionadjuster>(entity =>
        {
            entity.HasKey(e => e.IdOpinionAdjuster).HasName("PRIMARY");

            entity.ToTable("opinionadjuster");

            entity.Property(e => e.IdOpinionAdjuster)
                .HasMaxLength(100)
                .HasColumnName("idOpinionAdjuster");
            entity.Property(e => e.CreationDate).HasColumnName("creationDate");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Vehicleclient>(entity =>
        {
            entity.HasKey(e => e.IdVehicleClient).HasName("PRIMARY");

            entity.ToTable("vehicleclient");

            entity.HasIndex(e => e.InsurancePolicyIdInsurancePolicy, "fk_VehicleClient_InsurancePolicy1_idx");

            entity.Property(e => e.IdVehicleClient)
                .HasMaxLength(100)
                .HasColumnName("idVehicleClient");
            entity.Property(e => e.Brand)
                .HasMaxLength(45)
                .HasColumnName("brand");
            entity.Property(e => e.Color)
                .HasMaxLength(45)
                .HasColumnName("color");
            entity.Property(e => e.InsurancePolicyIdInsurancePolicy)
                .HasMaxLength(20)
                .HasColumnName("InsurancePolicy_idInsurancePolicy");
            entity.Property(e => e.Model)
                .HasMaxLength(45)
                .HasColumnName("model");
            entity.Property(e => e.Plate)
                .HasMaxLength(45)
                .HasColumnName("plate");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(45)
                .HasColumnName("serialNumber");
            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .HasColumnName("year");

            entity.HasOne(d => d.InsurancePolicyIdInsurancePolicyNavigation).WithMany(p => p.Vehicleclients)
                .HasForeignKey(d => d.InsurancePolicyIdInsurancePolicy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VehicleClient_InsurancePolicy1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
