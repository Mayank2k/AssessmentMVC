using AssessmentMVC.Models;
//using Rotativa;
//using Rotativa.Options;
using System;
using System.Web.Mvc;

namespace AssessmentMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AssesmentContext db = new AssesmentContext();

        public ActionResult Index()
        {
            return base.View();
        }

        //public ActionResult Test()
        //{
        //    return new ActionAsPdf("Index", new
        //    {
        //        name = "Giorgio"
        //    })
        //    {
        //        FileName = "Test.pdf"
        //    };
        //}

        //public ActionResult TestImage()
        //{
        //    return new ActionAsImage("Index", new
        //    {
        //        name = "Giorgio"
        //    })
        //    {
        //        FileName = "Test.jpg"
        //    };
        //}

        //public ActionResult TestImagePng()
        //{
        //    return new ActionAsImage("Index", new
        //    {
        //        name = "Giorgio"
        //    })
        //    {
        //        FileName = "Test.png",
        //        Format = new ImageFormat?(ImageFormat.png)
        //    };
        //}

        //public ActionResult TestUrl()
        //{
        //    return new UrlAsPdf(new UrlHelper(base.Request.RequestContext).Action("Index", new
        //    {
        //        name = "Giorgio II."
        //    }))
        //    {
        //        FileName = "TestUrl.pdf"
        //    };
        //}
    }
}
