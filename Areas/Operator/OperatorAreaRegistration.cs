using System.Web.Mvc;

namespace AssessmentMVC.Areas.Operator
{
    public class OperatorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Operator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Operator_default",
                "Operator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "AssessmentMVC.Areas.Operator.Controllers" }
            );
        }
    }
}