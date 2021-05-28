using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;

namespace AssessmentMVC.Areas.Operator.Controllers
{
    [Authorize]
    public class TestPaperController : AssessmentMVC.Controllers.TestPaperController
    { }       
}
