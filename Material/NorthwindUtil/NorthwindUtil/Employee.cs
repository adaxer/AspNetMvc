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
    public partial class Employee
    {
        public int EmployeeId { get; set; } // EmployeeID (Primary key)
        public string LastName { get; set; } // LastName
        public string FirstName { get; set; } // FirstName
        public string Title { get; set; } // Title
        public string TitleOfCourtesy { get; set; } // TitleOfCourtesy
        public DateTime? BirthDate { get; set; } // BirthDate
        public DateTime? HireDate { get; set; } // HireDate
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string HomePhone { get; set; } // HomePhone
        public string Extension { get; set; } // Extension
        public byte[] Photo { get; set; } // Photo
        public string Notes { get; set; } // Notes
        public int? ReportsTo { get; set; } // ReportsTo
        public string PhotoPath { get; set; } // PhotoPath

        // Reverse navigation
        public virtual ICollection<Employee> Employees { get; set; } // Employees.FK_Employees_Employees

        // Foreign keys
        public virtual Employee Employee_ReportsTo { get; set; } // FK_Employees_Employees

        public Employee()
        {
            Employees = new List<Employee>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
