using System.Text.RegularExpressions;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace SXA.Feature.MetadataExtended.Providers
{
    /// <summary>
    /// This Link provider generates localized urls, 
    /// for all languages except en which is concidered the root of the site.
    /// </summary>
    public class ExtendedLinkProvider : Sitecore.XA.Foundation.Multisite.LinkManagers.LocalizableLinkProvider
    {
        private const string _defaultLanguageRegex = "^\\/en(?!\\w)"; // Matches only /en and /en/<text>

        public override string GetItemUrl(Item item, UrlOptions options)
        {
            var url = base.GetItemUrl(item, options);
            
            if (url.StartsWith("/"))
            {
                url = Regex.Replace(url, _defaultLanguageRegex, string.Empty, RegexOptions.IgnoreCase);
                if (string.IsNullOrEmpty(url)) // if the url is only /en it will be replaced with string.empty 
                {
                    url = "/";
                }
            }

            // Remove trailing slash, except when the link is '/'
            if (url.Length > 1 && url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1);
            }

            return url;
        }
    }
}