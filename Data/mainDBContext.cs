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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public partial class mainDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken> 
    {
        public mainDBContext() :
            base()
        {
            OnCreated();
        }

        public mainDBContext(DbContextOptions<mainDBContext> options) :
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
            return configuration.GetConnectionString(connectionStringName);
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

        public virtual DbSet<EmpData> EmpDatas
        {
            get;
            set;
        }

        #region Methods

        public List<Employee> GetEmployee()
        {

            List<Employee> result = new List<Employee>();
            DbConnection connection = this.Database.GetDbConnection();
            bool needClose = false;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                needClose = true;
            }

            try
            {
                using (DbCommand cmd = connection.CreateCommand())
                {
                    if (this.Database.GetCommandTimeout().HasValue)
                        cmd.CommandTimeout = this.Database.GetCommandTimeout().Value;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"dbo.GetEmployees";
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                        while (reader.Read())
                        {
                            Employee row = new Employee();
                            if (fieldNames.Contains("ID") && !reader.IsDBNull(reader.GetOrdinal("ID")))
                                row.ID = (int)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"ID")), typeof(int));

                            if (fieldNames.Contains("Code") && !reader.IsDBNull(reader.GetOrdinal("Code")))
                                row.Code = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Code")), typeof(string));

                            if (fieldNames.Contains("FirstName") && !reader.IsDBNull(reader.GetOrdinal("FirstName")))
                                row.FirstName = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"FirstName")), typeof(string));

                            if (fieldNames.Contains("LastName") && !reader.IsDBNull(reader.GetOrdinal("LastName")))
                                row.LastName = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"LastName")), typeof(string));

                            if (fieldNames.Contains("Gender") && !reader.IsDBNull(reader.GetOrdinal("Gender")))
                                row.Gender = (bool)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Gender")), typeof(bool));

                            if (fieldNames.Contains("JobTitle") && !reader.IsDBNull(reader.GetOrdinal("JobTitle")))
                                row.JobTitle = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"JobTitle")), typeof(string));
                            else
                                row.JobTitle = null;

                            if (fieldNames.Contains("DepartmentID") && !reader.IsDBNull(reader.GetOrdinal("DepartmentID")))
                                row.DepartmentID = (int)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"DepartmentID")), typeof(int));

                            result.Add(row);
                        }
                    }
                }
            }
            finally
            {
                if (needClose)
                    connection.Close();
            }
            return result;
        }

        public async Task<List<Employee>> GetEmployeeAsync()
        {

            List<Employee> result = new List<Employee>();
            DbConnection connection = this.Database.GetDbConnection();
            bool needClose = false;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
                needClose = true;
            }

            try
            {
                using (DbCommand cmd = connection.CreateCommand())
                {
                    if (this.Database.GetCommandTimeout().HasValue)
                        cmd.CommandTimeout = this.Database.GetCommandTimeout().Value;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = @"dbo.GetEmployees";
                    using (IDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                        while (reader.Read())
                        {
                            Employee row = new Employee();
                            if (fieldNames.Contains("ID") && !reader.IsDBNull(reader.GetOrdinal("ID")))
                                row.ID = (int)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"ID")), typeof(int));

                            if (fieldNames.Contains("Code") && !reader.IsDBNull(reader.GetOrdinal("Code")))
                                row.Code = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Code")), typeof(string));

                            if (fieldNames.Contains("FirstName") && !reader.IsDBNull(reader.GetOrdinal("FirstName")))
                                row.FirstName = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"FirstName")), typeof(string));

                            if (fieldNames.Contains("LastName") && !reader.IsDBNull(reader.GetOrdinal("LastName")))
                                row.LastName = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"LastName")), typeof(string));

                            if (fieldNames.Contains("Gender") && !reader.IsDBNull(reader.GetOrdinal("Gender")))
                                row.Gender = (bool)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"Gender")), typeof(bool));

                            if (fieldNames.Contains("JobTitle") && !reader.IsDBNull(reader.GetOrdinal("JobTitle")))
                                row.JobTitle = (string)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"JobTitle")), typeof(string));
                            else
                                row.JobTitle = null;

                            if (fieldNames.Contains("DepartmentID") && !reader.IsDBNull(reader.GetOrdinal("DepartmentID")))
                                row.DepartmentID = (int)Convert.ChangeType(reader.GetValue(reader.GetOrdinal(@"DepartmentID")), typeof(int));

                            result.Add(row);
                        }
                    }
                }
            }
            finally
            {
                if (needClose)
                    connection.Close();
            }
            return result;
        }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.DepartmentMapping(modelBuilder);
            this.CustomizeDepartmentMapping(modelBuilder);

            this.EmployeeMapping(modelBuilder);
            this.CustomizeEmployeeMapping(modelBuilder);

            this.EmpDataMapping(modelBuilder);
            this.CustomizeEmpDataMapping(modelBuilder);

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

        #region EmpData Mapping

        private void EmpDataMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpData>().HasNoKey();
            modelBuilder.Entity<EmpData>().ToView(@"EmpData", @"dbo");
            modelBuilder.Entity<EmpData>().Property<int>(x => x.ID).HasColumnName(@"ID").HasColumnType(@"int").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<EmpData>().Property<string>(x => x.Code).HasColumnName(@"Code").HasColumnType(@"nvarchar(32)").IsRequired().ValueGeneratedNever().HasMaxLength(32);
            modelBuilder.Entity<EmpData>().Property<string>(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            modelBuilder.Entity<EmpData>().Property<string>(x => x.LastName).HasColumnName(@"LastName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            modelBuilder.Entity<EmpData>().Property<bool>(x => x.Gender).HasColumnName(@"Gender").HasColumnType(@"bit").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<EmpData>().Property<string>(x => x.JobTitle).HasColumnName(@"JobTitle").HasColumnType(@"nvarchar(64)").ValueGeneratedNever().HasMaxLength(64);
            modelBuilder.Entity<EmpData>().Property<int>(x => x.DepartmentID).HasColumnName(@"DepartmentID").HasColumnType(@"int").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<EmpData>().Property<string>(x => x.DepartmentName).HasColumnName(@"DepartmentName").HasColumnType(@"nvarchar(64)").IsRequired().ValueGeneratedNever().HasMaxLength(64);
            
        }

        partial void CustomizeEmpDataMapping(ModelBuilder modelBuilder);

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
