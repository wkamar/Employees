//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace employee_apis.Models
{

    public partial class employeeDBContext : DbContext
    {

        public employeeDBContext() :
            base()
        {
            OnCreated();
        }

        public employeeDBContext(DbContextOptions<employeeDBContext> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        private static string GetConnectionString(string connectionStringName)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<Department> Departments
        {
            get;
            set;
        }

        public virtual DbSet<Employee> Employees
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.DepartmentMapping(modelBuilder);
            this.CustomizeDepartmentMapping(modelBuilder);

            this.EmployeeMapping(modelBuilder);
            this.CustomizeEmployeeMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }

        #region Department Mapping

        private void DepartmentMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable(@"Departments", @"dbo");
            modelBuilder.Entity<Department>().Property<int>(x => x.ID).HasColumnName(@"ID").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property<string>(x => x.Name).HasColumnName(@"Name").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            modelBuilder.Entity<Department>().HasKey(@"ID");
        }

        partial void CustomizeDepartmentMapping(ModelBuilder modelBuilder);

        #endregion

        #region Employee Mapping

        private void EmployeeMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable(@"Employees", @"dbo");
            modelBuilder.Entity<Employee>().Property<int>(x => x.ID).HasColumnName(@"ID").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property<string>(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            modelBuilder.Entity<Employee>().Property<string>(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            modelBuilder.Entity<Employee>().Property<string>(x => x.LastName).HasColumnName(@"LastName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            modelBuilder.Entity<Employee>().Property<bool>(x => x.Gender).HasColumnName(@"Gender").HasColumnType(@"bit").IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql(@"1");
            modelBuilder.Entity<Employee>().Property<string>(x => x.JobTitle).HasColumnName(@"JobTitle").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            modelBuilder.Entity<Employee>().Property<int>(x => x.DepartmentID).HasColumnName(@"DepartmentID").HasColumnType(@"int").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Employee>().HasKey(@"ID");
        }

        partial void CustomizeEmployeeMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {

        #region Department Navigation properties

            modelBuilder.Entity<Department>().HasMany(x => x.Employees).WithOne(op => op.Department).IsRequired(true).HasForeignKey(@"DepartmentID");

        #endregion

        #region Employee Navigation properties

            modelBuilder.Entity<Employee>().HasOne(x => x.Department).WithMany(op => op.Employees).IsRequired(true).HasForeignKey(@"DepartmentID");

        #endregion
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
