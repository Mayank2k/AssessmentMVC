using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;
using PagedList;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using System.Configuration;

namespace AssessmentMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        // GET: User
        //public ActionResult Index()
        //{
        //    var users = db.Users.Include(u => u.City).Include(u => u.Country).Include(u => u.Group).Include(u => u.RoleType).Include(u => u.School).Include(u => u.State).Include(u => u.User2);
        //    return View(users.ToList());
        //}

        public ActionResult Index(int? SchoolId = 0, string searchBy ="", string search ="", int? page = 1, string sortBy ="")
        {
            IQueryable<User> source;
            int RoleTypeID = 1;

            ViewBag.SchoolID = new SelectList(this.db.Schools.Where(x => x.Active).OrderBy(x => x.SchoolName), "SchoolID", "SchoolName", SchoolId);
                source = (from x in this.db.Users
                                           where x.SchoolID == SchoolId && x.RoleTypeID == RoleTypeID
                          select x).Include((User u) => u.City).Include((User u) => u.Country).Include((User u) => u.Group).Include((User u) => u.RoleType).Include((User u) => u.State).Include((User u) => u.User2).Include((User u) => u.School).AsQueryable<User>();
            
            if (searchBy == "Name")
            {
                source = from x in source
                         where x.FirstName.StartsWith(search) || x.LastName.StartsWith(search) || x.RegistrationNo.EndsWith(search)
                         select x;
            }
            source = from x in source
                     orderby x.UserMappings.FirstOrDefault<UserMapping>().ClassID, x.UserMappings.FirstOrDefault<UserMapping>().SectionID, x.UserMappings.FirstOrDefault<UserMapping>().RollNo
                     select x;
            return base.View(source.ToList<User>().ToPagedList(page ?? 1, 100));
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName");
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName");
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Description");
            ViewBag.RoleTypeID = new SelectList(db.RoleTypes.Where(x=>x.RoleTypeID>0), "RoleTypeID", "RoleTypeName");
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode");
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName");
            ViewBag.ParentUserId = new SelectList(db.Users, "UserID", "FirstName");
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                {   new SelectListItem{Text = "Male",Value = "Male"},
                    new SelectListItem{Text = "Female",Value = "Female"}
                }, "Value", "Text");
           return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,MiddleName,LastName,Gender,EmailId,ContactNo,RegistrationNo,Image,AddressLine1,AddressLine2,CityCode,ZipCode,StateCode,CountryCode,SchoolID,RoleTypeID,GroupID,ParentUserId,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", user.CityCode);
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", user.CountryCode);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Description", user.GroupID);
            ViewBag.RoleTypeID = new SelectList(db.RoleTypes, "RoleTypeID", "RoleTypeName", user.RoleTypeID);
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", user.SchoolID);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", user.StateCode);
            ViewBag.ParentUserId = new SelectList(db.Users, "UserID", "FirstName", user.ParentUserId);
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                {   new SelectListItem{Text = "Male",Value = "Male"},
                    new SelectListItem{Text = "Female",Value = "Female"}
                }, "Value", "Text", user.Gender);
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", user.CityCode);
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", user.CountryCode);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Description", user.GroupID);
            ViewBag.RoleTypeID = new SelectList(db.RoleTypes, "RoleTypeID", "RoleTypeName", user.RoleTypeID);
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", user.SchoolID);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", user.StateCode);
            ViewBag.ParentUserId = new SelectList(db.Users, "UserID", "FirstName", user.ParentUserId);
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                {   new SelectListItem{Text = "Male",Value = "Male"},
                    new SelectListItem{Text = "Female",Value = "Female"}
                }, "Value", "Text", user.Gender);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,MiddleName,LastName,Gender,EmailId,ContactNo,RegistrationNo,Image,AddressLine1,AddressLine2,CityCode,ZipCode,StateCode,CountryCode,SchoolID,RoleTypeID,GroupID,ParentUserId,Active,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityCode = new SelectList(db.Cities, "CityCode", "CityName", user.CityCode);
            ViewBag.CountryCode = new SelectList(db.Countries, "CountryCode", "CountryName", user.CountryCode);
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Description", user.GroupID);
            ViewBag.RoleTypeID = new SelectList(db.RoleTypes, "RoleTypeID", "RoleTypeName", user.RoleTypeID);
            ViewBag.SchoolID = new SelectList(db.Schools.Where(x => x.Active).OrderBy( x=> x.SchoolName), "SchoolID", "SchoolCode", user.SchoolID);
            ViewBag.StateCode = new SelectList(db.States, "StateCode", "StateName", user.StateCode);
            ViewBag.ParentUserId = new SelectList(db.Users, "UserID", "FirstName", user.ParentUserId);
            ViewBag.Gender = new SelectList(new List<SelectListItem>
                {   new SelectListItem{Text = "Male",Value = "Male"},
                    new SelectListItem{Text = "Female",Value = "Female"}
                }, "Value", "Text", user.Gender);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void FileUpload(HttpPostedFileBase file, string newName)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string text = Path.Combine(base.Server.MapPath("~/images"), fileName);
                text = text.Replace(fileName, newName + extension);
                file.SaveAs(text);
            }
        }

        public ActionResult ExportToExcel(int? SchoolId)
        {
            if (SchoolId == null)
                return RedirectToAction("Index");

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string strQuery = "select * From vwStudentRegistration Where SchoolID =" + SchoolId + "Order by 1,2,3; ";
                using (SqlCommand sqlCommand = new SqlCommand(strQuery))
                {
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandTimeout = 0;
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        using (DataSet dataSet = new DataSet())
                        {
                            sqlDataAdapter.Fill(dataSet);
                            using (XLWorkbook xLWorkbook = new XLWorkbook())
                            {
                                xLWorkbook.Worksheets.Add(dataSet.Tables[0], "Student");
                                base.Response.Clear();
                                base.Response.Buffer = true;
                                base.Response.Charset = "";
                                base.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                base.Response.AddHeader("content-disposition", "attachment;filename=StudentRegistration.xlsx");
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    xLWorkbook.SaveAs(memoryStream);
                                    memoryStream.WriteTo(base.Response.OutputStream);
                                    base.Response.Flush();
                                    base.Response.End();
                                }
                            }
                        }
                    }
                }
            }
            return base.RedirectToAction("Index");
        }
    }
}
