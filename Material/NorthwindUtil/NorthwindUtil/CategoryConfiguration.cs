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
    // Categories
    internal partial class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Categories");
            HasKey(x => x.CategoryId);

            Property(x => x.CategoryId).HasColumnName("CategoryID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Picture).HasColumnName("Picture").IsOptional().HasMaxLength(2147483647);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
