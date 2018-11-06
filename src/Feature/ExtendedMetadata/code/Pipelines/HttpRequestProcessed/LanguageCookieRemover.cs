using System.Linq;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;

namespace SXA.Feature.MetadataExtended.Pipelines.HttpRequestProcessed
{
    public class LanguageCookieRemover : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (Sitecore.Context.Site.Properties["disableLanguageCookie"] == "true")
            {
                foreach (var cookieKey in HttpContext.Current.Response.Cookies.AllKeys.Where(key => key.EndsWith("#lang")))
                {
                    HttpContext.Current.Response.Cookies.Remove(cookieKey);
                }
            }
        }
    }
}