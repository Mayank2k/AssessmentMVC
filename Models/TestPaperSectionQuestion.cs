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
    
    public partial class TestPaperSectionQuestion
    {
        public int TestPaperSectionQuestionID { get; set; }
        public int TestPaperID { get; set; }
        public int TestPaperSectionID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionSequence { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual TestPaperSection TestPaperSection { get; set; }
        public virtual TestPaper TestPaper { get; set; }
        public virtual QuestionMaster QuestionMaster { get; set; }
    }
}
