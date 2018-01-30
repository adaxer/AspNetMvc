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
    public partial class NWContext : DbContext, INWContext
    {
        public IDbSet<Category> Categories { get; set; } // Categories
        public IDbSet<Employee> Employees { get; set; } // Employees

        static NWContext()
        {
            Database.SetInitializer<NWContext>(null);
        }

        public NWContext()
            : base("Name=nw")
        {
        InitializePartial();
        }

        public NWContext(string connectionString) : base(connectionString)
        {
        InitializePartial();
        }

        public NWContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        InitializePartial();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
        OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration(schema));
            modelBuilder.Configurations.Add(new EmployeeConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}
