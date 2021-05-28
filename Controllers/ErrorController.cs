using System;
using System.Web.Mvc;

namespace AssessmentMVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return base.View();
        }
    }
}
