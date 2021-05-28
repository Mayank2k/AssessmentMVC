using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AssessmentMVC.Custom
{
    public class TrackExecutionTime : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = string.Concat(new string[]
            {
                "\n",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                " -> ",
                filterContext.ActionDescriptor.ActionName,
                " -> OnActionExecuting \t- ",
                DateTime.Now.ToString(),
                "\n"
            });
            this.LogExecutionTime(message);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = string.Concat(new string[]
            {
                "\n",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                " -> ",
                filterContext.ActionDescriptor.ActionName,
                " -> OnActionExecuted \t- ",
                DateTime.Now.ToString(),
                "\n"
            });
            this.LogExecutionTime(message);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string message = string.Concat(new string[]
            {
                filterContext.RouteData.Values["controller"].ToString(),
                " -> ",
                filterContext.RouteData.Values["action"].ToString(),
                " -> OnResultExecuting \t- ",
                DateTime.Now.ToString(),
                "\n"
            });
            this.LogExecutionTime(message);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string message = string.Concat(new string[]
            {
                filterContext.RouteData.Values["controller"].ToString(),
                " -> ",
                filterContext.RouteData.Values["action"].ToString(),
                " -> OnResultExecuted \t- ",
                DateTime.Now.ToString(),
                "\n"
            });
            this.LogExecutionTime(message);
            this.LogExecutionTime("---------------------------------------------------------\n");
        }

        public void OnException(ExceptionContext filterContext)
        {
            string message = string.Concat(new string[]
            {
                filterContext.RouteData.Values["controller"].ToString(),
                " -> ",
                filterContext.RouteData.Values["action"].ToString(),
                " -> ",
                filterContext.Exception.Message,
                " \t- ",
                DateTime.Now.ToString(),
                "\n"
            });
            this.LogExecutionTime(message);
            this.LogExecutionTime("---------------------------------------------------------\n");
        }

        public void LogExecutionTime(string message)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Data/Data.txt"), message);
        }
    }
}
