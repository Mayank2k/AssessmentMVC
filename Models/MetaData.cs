using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentMVC.Models
{
    public interface IBaseEntity
    {
        string CreatedBy
        {
            get;
            set;
        }

        DateTime CreatedDate
        {
            get;
            set;
        }

        string ModifiedBy
        {
            get;
            set;
        }

        DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(BoardMetadata))]
    public partial class Board : IBaseEntity { }

    internal class BoardMetadata
    {
        public int BoardID
        {
            get;
            set;
        }

        [Display(Name = "Board"), Required]
        public string BoardName
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(SchoolMetadata))]
    public partial class School : IBaseEntity { }

    internal class SchoolMetadata
    {
        public int SchoolID
        {
            get;
            set;
        }

        [Display(Name = "School Code"), Required, Remote("IsSchoolCodeAvailable", "School", ErrorMessage = "School Code already Exist !")]
        public string SchoolCode
        {
            get;
            set;
        }

        [Display(Name = "School Name"), Required]
        public string SchoolName
        {
            get;
            set;
        }

        [Display(Name = "Board"), Required]
        public int BoardID
        {
            get;
            set;
        }

        [Display(Name = "Session Start"), Required]
        public DateTime? AcademicSessionStart
        {
            get;
            set;
        }

        [Display(Name = "Session End")]
        public DateTime? AcademicSessionEnd
        {
            get;
            set;
        }

        [Display(Name = "SPOC Name"), Required]
        public string SPOCName
        {
            get;
            set;
        }

        [Display(Name = "SPOC Contact"), Required]
        public string SPOCPhone
        {
            get;
            set;
        }

        [DataType(DataType.EmailAddress), Display(Name = "SPOC Email"), Required]
        public string SPOCEmail
        {
            get;
            set;
        }

        [Display(Name = "SPOC Fax")]
        public string SPOCFax
        {
            get;
            set;
        }

        [Display(Name = "Address1"), Required]
        public string AddressLine1
        {
            get;
            set;
        }

        [Display(Name = "Address2")]
        public string AddressLine2
        {
            get;
            set;
        }

        [Display(Name = "Country"), Required]
        public string CountryCode
        {
            get;
            set;
        }

        [Display(Name = "State"), Required]
        public string StateCode
        {
            get;
            set;
        }

        [Display(Name = "City"), Required]
        public string CityCode
        {
            get;
            set;
        }

        [Display(Name = "Pincode")]
        public string ZipCode
        {
            get;
            set;
        }

        [Display(Name = "Logo")]
        public string SchoolLogo
        {
            get;
            set;
        }

        [Display(Name = "Parent School")]
        public int? ParentSchoolID
        {
            get;
            set;
        }

        [DefaultValue(true)]
        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(CityMetadata))]
    public partial class City : IBaseEntity { }

    internal class CityMetadata
    {
    }

    [MetadataType(typeof(QuestionMasterMetadata))]
    public partial class QuestionMaster : IBaseEntity { }

    internal class QuestionMasterMetadata
    {
        [Display(Name = "Question ID")]
        public int QuestionID
        {
            get;
            set;
        }

        [Display(Name = "Question Code"), Required, Remote("IsQuestionCodeAvailable", "QuestionMaster", AdditionalFields = "QuestionID", ErrorMessage = "Question Code already Exist !")]
        public string QuestionCode
        {
            get;
            set;
        }

        [Display(Name = "Question Type"), Required]
        public string QuestionTypeID
        {
            get;
            set;
        }

        [Display(Name = "Question Body"), Required, AllowHtml]
        public string QuestionBody
        {
            get;
            set;
        }

        [DataType(DataType.Duration), Display(Name = "Marks"), Range(0, 10), Required]
        public decimal Marks
        {
            get;
            set;
        }

        [Display(Name = "Status")]
        public int StatusID
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(QuestionOptionMetadata))]
    public partial class QuestionOption : IBaseEntity { }


    internal class QuestionOptionMetadata
    {
        public int QuestionOptionID
        {
            get;
            set;
        }

        [Display(Name = "Question Code"), Required]
        public int QuestionID
        {
            get;
            set;
        }

        [Display(Name = "Option ID"), Required, Remote("IsOptionIdAvailable", "QuestionOptions", AdditionalFields = "QuestionID", ErrorMessage = "OptionID Already Created for this Question !")]
        public string OptionID
        {
            get;
            set;
        }

        [Display(Name = "Option Text"), Required]
        public string OptionText
        {
            get;
            set;
        }

        [Display(Name = "Option Hint")]
        public string OptionHint
        {
            get;
            set;
        }

        [Display(Name = "Correct Option?"), Required]
        public bool CorrectOption
        {
            get;
            set;
        }

        [Display(Name = "Explanation For Option")]
        public string ExplanationForOption
        {
            get;
            set;
        }

        [Display(Name = "Misconception1")]
        public int? MisconceptionID
        {
            get;
            set;
        }

        [Display(Name = "Misconception2")]
        public int? OtherMisconceptionID1
        {
            get;
            set;
        }

        [Display(Name = "Misconception3")]
        public int? OtherMisconceptionID2
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(QuestionTagMetadata))]
    public partial class QuestionTag : IBaseEntity { }

    internal class QuestionTagMetadata
    {
        public int QuestionTagID
        {
            get;
            set;
        }

        [Display(Name = "Question Code"), Required]
        public int QuestionID
        {
            get;
            set;
        }

        [Display(Name = "Course"), Required]
        public int CourseID
        {
            get;
            set;
        }

        [Display(Name = "Unit"), Required]
        public int? UnitID
        {
            get;
            set;
        }

        [Display(Name = "Chapter")]
        public int? ChapterID
        {
            get;
            set;
        }

        [Display(Name = "Topic"), Required]
        public int? TopicID
        {
            get;
            set;
        }

        [Display(Name = "Sub Topic")]
        public int? SubTopicID
        {
            get;
            set;
        }

        [Display(Name = "Learning Objective")]
        public int? LearningObjectiveID
        {
            get;
            set;
        }

        [Display(Name = "Learning Outcome")]
        public int? LearningOutcomeID
        {
            get;
            set;
        }

        [Display(Name = "Learning Level"), Required]
        public int? LearningLevelID
        {
            get;
            set;
        }

        [Display(Name = "Difficulty Level"), Required]
        public int? DifficultyLevelID
        {
            get;
            set;
        }

        [Display(Name = "Competency"), Required]
        public int CompetencyID
        {
            get;
            set;
        }

        [Display(Name = "Skill"), Required]
        public int SkillID
        {
            get;
            set;
        }

        [Display(Name = "Sub Skill")]
        public int SubskillID
        {
            get;
            set;
        }

        public virtual Chapter Chapter
        {
            get;
            set;
        }

        public virtual Competency Competency
        {
            get;
            set;
        }

        public virtual Course Course
        {
            get;
            set;
        }

        public virtual DifficultyLevel DifficultyLevel
        {
            get;
            set;
        }

        public virtual LearningLevel LearningLevel
        {
            get;
            set;
        }

        public virtual LearningObjective LearningObjective
        {
            get;
            set;
        }

        public virtual LearningOutcome LearningOutcome
        {
            get;
            set;
        }

        public virtual QuestionMaster QuestionMaster
        {
            get;
            set;
        }

        public virtual Skill Skill
        {
            get;
            set;
        }

        public virtual SubSkill SubSkill
        {
            get;
            set;
        }

        public virtual SubTopic SubTopic
        {
            get;
            set;
        }

        public virtual Topic Topic
        {
            get;
            set;
        }

        public virtual Unit Unit
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(StateMetadata))]
    public partial class State : IBaseEntity { }

    internal class StateMetadata
    {
    }

    [MetadataType(typeof(TestPaperMetadata))]
    public partial class TestPaper : IBaseEntity
    {
        [Display(Name = "Total Questions")]
        public int TotalQuestions
        {
            get
            {
                if (this.TestPaperSections != null && this.TestPaperSections.Count > 0 && this.TestPaperSectionQuestions != null)
                {
                    return this.TestPaperSections.First<TestPaperSection>().TestPaperSectionQuestions.Count<TestPaperSectionQuestion>();
                }
                return 0;
            }
        }
    }

    internal class TestPaperMetadata
    {
        public int TestPaperID
        {
            get;
            set;
        }

        [Display(Name = "Paper Code"), Required, Remote("IsPaperCodeAvailable", "TestPaper", ErrorMessage = "Paper Code already Exist !")]
        public string TestPaperCode
        {
            get;
            set;
        }

        [Required]
        public string Description
        {
            get;
            set;
        }

        [Display(Name = "Instructions"), Required]
        public string Instructions
        {
            get;
            set;
        }

        [Display(Name = "Paper Set"), Required]
        public string TestPaperSetCode
        {
            get;
            set;
        }

        [Display(Name = "Activation Date"), Required]
        public DateTime ActivationDate
        {
            get;
            set;
        }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate
        {
            get;
            set;
        }

        [Display(Name = "Max Questions"), Required]
        public int MaxQuestions { get; set; }

        [DataType(DataType.Duration), Display(Name = "Time (in Mins.)"), Range(0, 180), Required]
        public decimal Time
        {
            get;
            set;
        }

        [DataType(DataType.Duration), Display(Name = "Max. Marks"), Range(0, 300), Required]
        public decimal Marks
        {
            get;
            set;
        }

        [Display(Name = "Course"), Required]
        public int CourseID
        {
            get;
            set;
        }
        [Display(Name = "Publish ?")]
        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(TestPaperSectionQuestionMetadata))]
    public partial class TestPaperSectionQuestion : IBaseEntity { }

    internal class TestPaperSectionQuestionMetadata
    {
        public int TestPaperSectionQuestionID
        {
            get;
            set;
        }

        [Display(Name = "Test Paper")]
        public int TestPaperID
        {
            get;
            set;
        }

        [Display(Name = "Paper Section")]
        public int TestPaperSectionID
        {
            get;
            set;
        }

        //[Remote("IsQuestionIDAvailable", "TestPaperSectionQuestions", AdditionalFields = "TestPaperID", ErrorMessage = "Either Question Code not found or Question Code already Added !")]
        [Display(Name = "Question Code")]
        public int QuestionID
        {
            get;
            set;
        }

        [Display(Name = "Seq#"), Remote("IsQuestionSequenceAvailable", "TestPaperSectionQuestions", AdditionalFields = "TestPaperID", ErrorMessage = "Question Sequence already Created !")]
        public int QuestionSequence
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public string ModifiedBy
        {
            get;
            set;
        }

        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(TestResultMetadata))]
    public partial class TestResult : IBaseEntity { }

    internal class TestResultMetadata
    {
        public int TestResultID
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }

        [Display(Name = "Test Paper")]
        public int TestPaperID
        {
            get;
            set;
        }

        public int TestCycleID
        {
            get;
            set;
        }

        [Display(Name = "Exam Date")]
        public DateTime ExamDate
        {
            get;
            set;
        }

        [Display(Name = "Marks Obtained")]
        public decimal? MarksObtained
        {
            get;
            set;
        }

        [Display(Name = "Questions Attempted")]
        public int? QuestionsAttempted
        {
            get;
            set;
        }

        [Display(Name = "Time Taken")]
        public decimal? TimeTaken
        {
            get;
            set;
        }

        public int SchoolID
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(UserMetadata))]
    public partial class User : IBaseEntity
    {
        [Display(Name = "Full Name")]
        public string Name
        {
            get
            {
                return string.Concat(new string[]
                {
                    this.FirstName,
                    " ",
                    this.MiddleName,
                    " ",
                    this.LastName
                });
            }
        }
    }

    internal class UserMetadata
    {
        public int UserID
        {
            get;
            set;
        }

        [Display(Name = "First Name"), Required]
        public string FirstName
        {
            get;
            set;
        }

        [Display(Name = "Middle Name")]
        public string MiddleName
        {
            get;
            set;
        }

        [Display(Name = "Last Name"), Required]
        public string LastName
        {
            get;
            set;
        }

        [Required]
        public string Gender
        {
            get;
            set;
        }

        [DataType(DataType.EmailAddress), Display(Name = "Email")]
        public string EmailId
        {
            get;
            set;
        }

        [Display(Name = "Contact No")]
        public string ContactNo
        {
            get;
            set;
        }

        [Display(Name = "Registration No")]
        public string RegistrationNo
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        [Display(Name = "Address1")]
        public string AddressLine1
        {
            get;
            set;
        }

        [Display(Name = "Address2")]
        public string AddressLine2
        {
            get;
            set;
        }

        [Display(Name = "Country")]
        public string CountryCode
        {
            get;
            set;
        }

        [Display(Name = "State")]
        public string StateCode
        {
            get;
            set;
        }

        [Display(Name = "City")]
        public string CityCode
        {
            get;
            set;
        }

        [Display(Name = "Pin Code")]
        public string ZipCode
        {
            get;
            set;
        }

        [Required]
        public int? SchoolID
        {
            get;
            set;
        }

        [Display(Name = "Role")]
        public int RoleTypeID
        {
            get;
            set;
        }

        public int? GroupID
        {
            get;
            set;
        }

        [Display(Name = "Parent")]
        public int? ParentUserId
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(UnitMetadata))]
    public partial class Unit : IBaseEntity
    {
        [Display(Name = "Total Questions")]
        public int TotalQuestions
        {
            get
            {
                if (this.QuestionTags != null)
                {
                    return this.QuestionTags.Where(x => x.Active).Count();
                }
                return 0;
            }
        }
    }

    internal class UnitMetadata
    {
        [Display(Name = "Unit")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(TopicMetadata))]
    public partial class Topic : IBaseEntity
    {
        [Display(Name = "Total Questions")]
        public int TotalQuestions
        {
            get
            {
                if (this.QuestionTags != null)
                {
                    return this.QuestionTags.Where(x => x.Active).Count();
                }
                return 0;
            }
        }
    }

    internal class TopicMetadata
    {
        [Display(Name = "Topic")]
        public string Description { get; set; }
    }


    [MetadataType(typeof(LearningObjectiveMetadata))]
    public partial class LearningObjective : IBaseEntity { }

    internal class LearningObjectiveMetadata
    {
        [Display(Name = "Learning Objective")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(LearningOutcomeMetadata))]
    public partial class LearningOutcome : IBaseEntity { }

    internal class LearningOutcomeMetadata
    {
        [Display(Name = "Learning Outcome")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(TestPaperSetMetadata))]
    public partial class TestPaperSet : IBaseEntity { }

    internal class TestPaperSetMetadata
    {
        [Display(Name = "Set Code"), Required, Remote("IsSetCodeAvailable", "TestPaperSet", ErrorMessage = "Set Code already Exist !")]
        public string TestPaperSetCode
        {
            get;
            set;
        }

        [Display(Name = "Set Name"), Required]
        public string Description
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        [Display(Name = "Created By")]
        public string CreatedBy
        {
            get;
            set;
        }

        [Display(Name = "Created On")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Modified By")]
        public string ModifiedBy
        {
            get;
            set;
        }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
    }

    [MetadataType(typeof(PhoneMetadata))]
    public partial class Phone : IBaseEntity
    {
        public string CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public string ModifiedBy
        {
            get;
            set;
        }

        public DateTime? ModifiedDate
        {
            get;
            set;
        }
        
    }

    internal class PhoneMetadata
    {
        [Display(Name = "ID")]
        [Key]
        public int PhoneId { get; set; }

        [Required]
        [Display(Name = "Model Name")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}