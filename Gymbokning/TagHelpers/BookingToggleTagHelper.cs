using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace Booking.Web.TagHelpers
{
    public class BookingToggleTagHelper : TagHelper
    {
        public BookingToggleTagHelper(IUrlHelper urlHelper) 
        {
            UrlHelper = urlHelper;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName= "a";
            output.AddClass("btn", HtmlEncoder.Default);
            output.AddClass("btn-primary", HtmlEncoder.Default);
            output.Content.SetHtmlContent("Book/Unbook");
            //output.Attributes.SetAttribute("href",{ UrlHelper.Action(name, controllerName)})
           
        }
        private IUrlHelper UrlHelper { get; }
    }
}
