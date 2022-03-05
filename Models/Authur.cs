using Book.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;

public class ArticleContext : DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options)
            : base(options)
        { }
        public DbSet<User> Users { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
          
        public DbSet<DoctorModel> DoctorModels { get; set; }

        public DbSet<Patient> Patient { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>(entity =>
            {
                entity.ToTable("users", "core");
                entity.Property(x => x.Id).HasColumnName("id").UseSerialColumn();
                entity.Property(x => x.UserName).HasColumnName("user_name");
                entity.Property(x => x.UserTypeId).HasColumnName("user_type_id");
                entity.Property(x => x.Address).HasColumnName("address");
                entity.Property(x => x.MobileNo).HasColumnName("mobile_no");
                entity.Property(x => x.ContactNumber).HasColumnName("contact_number");
                entity.Property(x => x.CreatedOn).HasColumnName("created_on");
                entity.Property(x => x.Password).HasColumnName("password");
            });

            model.Entity<Doctor>(entity =>
        {
            entity.ToTable("doctors", "appointment");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("id").UseSerialColumn();
            entity.Property(x => x.DoctorName).HasColumnName("doctor_name");
            entity.Property(x => x.Specification).HasColumnName("specification");
            entity.Property(x => x.CreatedBy).HasColumnName("created_by");
            entity.Property(x => x.CreatedOn).HasColumnName("created_on");
            entity.Property(x => x.Password).HasColumnName("password");
        });

             model.Entity<Patient>(entity =>  
        {
            entity.ToTable("patients", "appointment");
            entity.Property(x => x.Id).HasColumnName("id").UseSerialColumn(); 
            entity.Property(x => x.PatientName).HasColumnName("user_name");
            entity.Property(x => x.Password).HasColumnName("password");
            entity.Property(x => x.MobileNumber).HasColumnName("mobile_number");
            entity.Property(x => x.Gender).HasColumnName("gender");
            entity.Property(x => x.DateOfBirth).HasColumnName("date_Of_birth");
            entity.Property(x => x.CreatedBy).HasColumnName("created_by");
            entity.Property(x => x.CreatedOn).HasColumnName("created_on");
        });

    }
}
