using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace AssessmentMVC.Custom
{
    public class RemoteClientServerAttribute : RemoteAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Type type2 = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault((Type type) => type.Name.ToLower() == string.Format("{0}Controller", base.RouteData["controller"].ToString()).ToLower());
            if (type2 != null)
            {
                MethodInfo methodInfo = type2.GetMethods().FirstOrDefault((MethodInfo method) => method.Name.ToLower() == base.RouteData["action"].ToString().ToLower());
                if (methodInfo != null)
                {
                    object obj = Activator.CreateInstance(type2);
                    object obj2 = methodInfo.Invoke(obj, new object[]
                    {
                        value
                    });
                    if (obj2 is JsonResult)
                    {
                        object data = ((JsonResult)obj2).Data;
                        if (data is bool)
                        {
                            if (!(bool)data)
                            {
                                return new ValidationResult(base.ErrorMessage);
                            }
                            return ValidationResult.Success;
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }

        public RemoteClientServerAttribute(string routeName) : base(routeName)
        {
        }

        public RemoteClientServerAttribute(string action, string controller) : base(action, controller)
        {
        }

        public RemoteClientServerAttribute(string action, string controller, string areaName) : base(action, controller, areaName)
        {
        }
    }
}
