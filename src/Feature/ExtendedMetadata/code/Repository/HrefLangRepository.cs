using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.XA.Foundation.Multisite;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;
using SXA.Feature.MetadataExtended.Extensions;
using SXA.Feature.MetadataExtended.Models;

namespace SXA.Feature.MetadataExtended.Repositories
{
    public class HrefLangRepository : VariantsRepository, IHrefLangRepository
    {
        public override IRenderingModelBase GetModel()
        {
            var model = new HrefLangRenderingModel();
            FillBaseProperties(model);

            var item = Sitecore.Context.Item;
            var settings = GetSettingsItem(item).Axes.GetChild("Languages");
            var supportedLanguagesField = new MultilistField(settings.Fields["SupportedLanguages"]);
            var supportedLanguages = supportedLanguagesField.GetItems();
            
            foreach (var lang in supportedLanguages)
            {
                var localizedUrl = item.LocalizedUrl(lang.Name, includeServerUrl: true);
                if (!string.IsNullOrEmpty(localizedUrl))
                {
                    model.AlternateUrls.Add(lang.Name, localizedUrl);
                }
            }

            return model;
        }

        protected virtual Item GetSettingsItem(Item contextItem)
        {
            return ServiceLocator.ServiceProvider.GetService<IMultisiteContext>().GetSettingsItem(contextItem);
        }
    }
}