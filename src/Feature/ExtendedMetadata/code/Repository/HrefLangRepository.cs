using Sitecore.Data.Fields;
using Sitecore.Globalization;
using Sitecore.Links;
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

            var supportedLanguagesField = new MultilistField(model.Item.Fields["SupportedLanguages"]);
            var supportedLanguages = supportedLanguagesField.GetItems();
            var item = Sitecore.Context.Item;
            
            foreach (var lang in supportedLanguages)
            {
                var languageItem = item.Database.GetItem(item.ID, Language.Parse(lang.Name));
                using (new LanguageSwitcher(lang.Name))
                {
                    var url = LinkManager.GetItemUrl(languageItem);
                    model.AlternateUrls.Add(lang.Name, url);
                }
            }

            return model;
        }
    }
}