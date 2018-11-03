using System.Collections.Generic;
using Sitecore.XA.Foundation.Variants.Abstractions.Models;

namespace SXA.Feature.MetadataExtended.Models
{
    public class HrefLangRenderingModel : VariantsRenderingModel
    {
        public Dictionary<string, string> AlternateUrls { get; set; }

        public HrefLangRenderingModel()
        {
            AlternateUrls = new Dictionary<string, string>();
        }
    }
}