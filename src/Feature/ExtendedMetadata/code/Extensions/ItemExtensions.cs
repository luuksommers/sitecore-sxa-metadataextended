using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Links;

namespace SXA.Feature.MetadataExtended.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// This extension helps you getting the right localized url
        /// </summary>
        public static string LocalizedUrl(this Item item, string languageCode, bool? includeServerUrl = null)
        {
            var options = LinkManager.GetDefaultUrlOptions();
            if (includeServerUrl.HasValue)
            {
                options.AlwaysIncludeServerUrl = includeServerUrl.Value;
            }

            var language = Language.Parse(languageCode);

            // We have to get the language item for the right display-name
            var languageItem = item.Database.GetItem(item.ID, language);

            // And use the Language switcher to get the right language in the url
            using (new LanguageSwitcher(language))
            {
                return LinkManager.GetItemUrl(languageItem, options);
            }
        }
    }
}