// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.7
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Southwind.Data
{

    // Categories
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.33.0.0")]
    public partial class CategoryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
            : this("dbo")
        {
        }

        public CategoryConfiguration(string schema)
        {
            ToTable("Categories", schema);
            Property(x => x.CategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName).HasColumnName(@"CategoryName").HasColumnType("nvarchar");
            Property(x => x.Description).HasColumnName(@"Description").HasColumnType("ntext").IsOptional();
            Property(x => x.Picture).HasColumnName(@"Picture").HasColumnType("image").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
