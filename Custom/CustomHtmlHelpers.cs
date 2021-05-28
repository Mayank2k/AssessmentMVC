using System;
using System.Web;
using System.Web.Mvc;

namespace AssessmentMVC.Custom
{
    public static class CustomHtmlHelpers
    {
        //img-rounded, img-circle, img-thumbnail, img-responsive		

        public static IHtmlString Image(this HtmlHelper helper, string src)
        {
            return new MvcHtmlString(new TagBuilder("img")
            {
                Attributes =
                {
                    {"src", VirtualPathUtility.ToAbsolute(src)},
                    {"alt","No Image"},                    
                    {"class", "img-thumbnail"}
                }
            }.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString ImageCircle(this HtmlHelper helper, string src)
        {
            return new MvcHtmlString(new TagBuilder("img")
            {
                Attributes =
                {
                    {"src", VirtualPathUtility.ToAbsolute(src)},
                    {"alt","No Image"},
                    {"width", "100"},
                    {"height","100"},
                    {"class", "img-circle"}
                }
            }.ToString(TagRenderMode.SelfClosing));
        }

        public static string HtmlDecode(string s)
        {
            return HttpUtility.HtmlDecode(s);
        }
    }
}
