//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssessmentMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class School
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School()
        {
            this.OrderMasters = new HashSet<OrderMaster>();
            this.School1 = new HashSet<School>();
            this.TestResults = new HashSet<TestResult>();
            this.TestResultDetails = new HashSet<TestResultDetail>();
            this.Users = new HashSet<User>();
            this.UserGroups = new HashSet<UserGroup>();
            this.UserMappings = new HashSet<UserMapping>();
        }
    
        public int SchoolID { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public Nullable<int> BoardID { get; set; }
        public Nullable<System.DateTime> AcademicSessionStart { get; set; }
        public Nullable<System.DateTime> AcademicSessionEnd { get; set; }
        public string SPOCName { get; set; }
        public string SPOCPhone { get; set; }
        public string SPOCEmail { get; set; }
        public string SPOCFax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CityCode { get; set; }
        public string ZipCode { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public string SchoolLogo { get; set; }
        public Nullable<int> ParentSchoolID { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual Board Board { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderMaster> OrderMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<School> School1 { get; set; }
        public virtual School School2 { get; set; }
        public virtual State State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestResult> TestResults { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestResultDetail> TestResultDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMapping> UserMappings { get; set; }
    }
}