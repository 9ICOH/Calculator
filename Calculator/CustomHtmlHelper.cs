using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculator
{
    public static class CustomHtmlHelper
    {

        public static MvcHtmlString Submit(this HtmlHelper html, string buttonText)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("type", "submit");
            tag.MergeAttribute("value", buttonText);
            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));

        }
    }
}