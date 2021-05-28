using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentMVC.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload  
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string userFileName)
        {
            try
            {
                string _ImageFolder = ConfigurationManager.AppSettings["UploadedFilesPath"];
                string _ImageFolderPath = Server.MapPath(_ImageFolder);
                if (!Directory.Exists(_ImageFolderPath))
                {
                    Directory.CreateDirectory(_ImageFolderPath);
                }
                
                if (file.ContentLength > 0)
                {   
                    string _FileName = Path.GetFileName(file.FileName);
                    string _NewFileName = string.Empty;

                    if (string.IsNullOrEmpty(userFileName))
                    {
                        //_NewFileName = Guid.NewGuid().ToString() + "_" + _FileName;
                        _NewFileName = _FileName;
                    }
                    else
                    {
                        _NewFileName = userFileName + Path.GetExtension(file.FileName);
                    }                    

                    string _FilePath = Path.Combine(_ImageFolderPath, _NewFileName);
                    
                    if (System.IO.File.Exists(_FilePath))
                    {
                        ViewBag.Message = "Same Filename Already Exists!!";
                        return View();
                    }
                    
                    file.SaveAs(_FilePath);
                    ViewBag.Message = "Image Uploaded Successfully!!";
                    ViewBag.URL = Path.Combine(_ImageFolder, _NewFileName);
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Image upload failed!!";
                return View();
            }
        }
    }
}