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
    
    public partial class fnDifficultyLevelBasedScore_Result
    {
        public int SchoolID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string SectionID { get; set; }
        public Nullable<decimal> MarksObtained { get; set; }
        public Nullable<int> TotalMarks { get; set; }
        public Nullable<decimal> Percentage { get; set; }
    }
}
