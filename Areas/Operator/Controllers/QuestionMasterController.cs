using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentMVC.Models;
using PagedList;

namespace AssessmentMVC.Areas.Operator.Controllers
{
    [Authorize]
    public class QuestionMasterController : AssessmentMVC.Controllers.QuestionMasterController
    { }
}
