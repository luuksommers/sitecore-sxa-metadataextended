using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.XA.Foundation.Multisite;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;
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
                // We have to get the language item for the right display-name
                var languageItem = item.Database.GetItem(item.ID, Language.Parse(lang.Name));

                // And use the Language switcher to get the right language in the url
                using (new LanguageSwitcher(lang.Name))
                {
                    var url = LinkManager.GetItemUrl(languageItem);
                    model.AlternateUrls.Add(lang.Name, url);
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