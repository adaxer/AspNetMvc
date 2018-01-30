// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace NorthwindUtil
{
    // Employees
    internal partial class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Employees");
            HasKey(x => x.EmployeeId);

            Property(x => x.EmployeeId).HasColumnName("EmployeeID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(20);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(10);
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(30);
            Property(x => x.TitleOfCourtesy).HasColumnName("TitleOfCourtesy").IsOptional().HasMaxLength(25);
            Property(x => x.BirthDate).HasColumnName("BirthDate").IsOptional();
            Property(x => x.HireDate).HasColumnName("HireDate").IsOptional();
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.HomePhone).HasColumnName("HomePhone").IsOptional().HasMaxLength(24);
            Property(x => x.Extension).HasColumnName("Extension").IsOptional().HasMaxLength(4);
            Property(x => x.Photo).HasColumnName("Photo").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Notes).HasColumnName("Notes").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ReportsTo).HasColumnName("ReportsTo").IsOptional();
            Property(x => x.PhotoPath).HasColumnName("PhotoPath").IsOptional().HasMaxLength(255);

            // Foreign keys
            HasOptional(a => a.Employee_ReportsTo).WithMany(b => b.Employees).HasForeignKey(c => c.ReportsTo); // FK_Employees_Employees
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
