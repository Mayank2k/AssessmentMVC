using System;
using System.ComponentModel.DataAnnotations;

namespace AssessmentMVC.Custom
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return System.Convert.ToDateTime(value) <= DateTime.Now;
        }
    }
}
