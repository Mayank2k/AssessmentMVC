using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;


namespace AssessmentMVC.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        public ActionResult Index(int? SchoolId)
        {
            ViewBag.SchoolID = new SelectList(this.db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolName", SchoolId);
            IQueryable<School> source = (from x in this.db.Schools
                                         where x.SchoolID == SchoolId || SchoolId == null
                                         select x).Include((School s) => s.City).Include((School s) => s.State);
            
            return base.View(source.ToList<School>().Where(x => x.Active).OrderBy(x => x.SchoolName));
        }

        public ActionResult Report(int? schoolId, int? userId, int? classId, string sectionId = null, int? subjectId = null)
        {
            ViewBag.UserId = userId;
            ViewBag.SchoolId = schoolId;
            ViewBag.ClassId = classId;
            ViewBag.SectionId = sectionId;
            ViewBag.SubjectId = subjectId;
            return base.View();
        }

        public ActionResult SchoolInfo(int schoolId, int? classId, string sectionId = null, int? subjectId = null)
        {
            School model = (from s in db.Schools where s.SchoolID == schoolId select s).Single<School>();
            return this.PartialView("_SchoolInfo", model);
        }

        public ActionResult StudentReport(int userId)
        {
           ViewBag.UserId = userId;
            return base.View();
        }

        public ActionResult StudentInfo(int userId)
        {
            User model = (from u in this.db.Users
                          join um in this.db.UserMappings on u.UserID equals um.UserID
                          join s in this.db.Schools on u.SchoolID equals (int?)s.SchoolID
                          where u.UserID == userId
                          select u).Single<User>();
            return this.PartialView("_StudentInfo", model);
        }

        public JsonResult GetOverAllScores(int? schoolId, int? userId, int? classId, string sectionId = null, int? subjectId = null)
        {
            
            //var firstList =
            //    from user in db.fnSubjectBasedScore(null, userId, null, null, null)
            //    group user by new { user.UserID } into Group
            //    select new
            //    {
            //        UserID = Group.Key,
            //        Percentage = (Group.Sum(x => x.MarksObtained) / Group.Sum(x => x.TotalMarks)) * 100
            //    };

            //var secondList = db.fnCompetencyWiseScore(null, userId, null, null, null)
            //            //.GroupBy(f => new { f.UserID, f.FeeTypeID })
            //            .GroupBy(f => new { f.UserID })
            //            .Select(group => new
            //            {
            //                UserID = group.Key,
            //                Percentage = (group.Sum(f => f.MarksObtained) / group.Sum(f => f.TotalMarks)) * 100
            //            });

            //var thirdList = db.fnDifficultyLevelWiseScore(null, userId, null, null, null)
            //            //.GroupBy(f => new { f.UserID, f.FeeTypeID })
            //            .GroupBy(f => new { f.UserID })
            //            .Select(group => new
            //            {
            //                UserID = group.Key,
            //                Percentage = (group.Sum(f => f.MarksObtained) / group.Sum(f => f.TotalMarks)) * 100
            //            });
                        
            return base.Json(new
            {
                FirstList = db.fnSubjectBasedScore(schoolId, userId, classId, sectionId, subjectId).ToList(),
                SecondList = db.fnCompetencyBasedScore(schoolId, userId, classId, sectionId, subjectId).ToList(),
                ThirdList = db.fnDifficultyLevelBasedScore(schoolId, userId, classId, sectionId, subjectId).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubjectWiseScores(int? schoolId, int? userId, int? classId, string sectionId = null, int? subjectId = null)
        {
            List<fnSubjectWiseScore_Result> list = new List<fnSubjectWiseScore_Result>();
            list = db.fnSubjectWiseScore(schoolId, userId, classId, sectionId, subjectId).ToList();
            
            return base.Json(new
            {
                FirstList = list.Where(x => x.SubjectID.Equals(1)).ToList(),
                SecondList = list.Where(x => x.SubjectID.Equals(2)).ToList(),
                ThirdList = list.Where(x => x.SubjectID.Equals(3)).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompetencyWiseScores(int? schoolId, int? userId, int? classId, string sectionId = null, int? subjectId = null)
        {
            List<fnCompetencyWiseScore_Result> list = new List<fnCompetencyWiseScore_Result>();
            list = db.fnCompetencyWiseScore(schoolId, userId, classId, sectionId, subjectId).ToList();
            
            return base.Json(new
            {
                FirstList = list.Where(x => x.CompetencyID.Equals(1)).ToList(),
                SecondList = list.Where(x => x.CompetencyID.Equals(2)).ToList(),
                ThirdList = list.Where(x => x.CompetencyID.Equals(3)).ToList(),
                FourthList = list.Where(x => x.CompetencyID.Equals(4)).ToList(),
                FifthList = list.Where(x => x.CompetencyID.Equals(5)).ToList(),
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDifficultyLevelWiseScores(int? schoolId, int? userId, int? classId, string sectionId = null, int? subjectId = null)
        {
            List<fnDifficultyLevelWiseScore_Result> list = new List<fnDifficultyLevelWiseScore_Result>();
            list = db.fnDifficultyLevelWiseScore(schoolId, userId, classId, sectionId, subjectId).ToList();

            return base.Json(new
            {
                FirstList = list.Where(x => x.DifficultyLevelID.Equals(1)).ToList(),
                SecondList = list.Where(x => x.DifficultyLevelID.Equals(2)).ToList(),
                ThirdList = list.Where(x => x.DifficultyLevelID.Equals(3)).ToList(),
            }, JsonRequestBehavior.AllowGet);
        }
    }
}