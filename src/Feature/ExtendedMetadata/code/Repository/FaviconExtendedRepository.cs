using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Resources.Media;
using Sitecore.XA.Foundation.Multisite;
using SXA.Feature.MetadataExtended.Models;

namespace SXA.Feature.MetadataExtended.Repository
{

    public class FaviconExtendedRepository : IFaviconExtendedRepository
    {
        public virtual FaviconExtendedModel GetFaviconModel(Item contextItem)
        {
            FaviconExtendedModel str = new FaviconExtendedModel();
            var settingsItem = this.GetSettingsItem(contextItem);

            str.AppleTouchIcon = GetFieldImage(settingsItem, "AppleTouchIcon");
            str.Icon32x32 = GetFieldImage(settingsItem, "Icon32x32");
            str.Icon16x16 = GetFieldImage(settingsItem, "Icon16x16");
            str.SiteManifest = GetFieldImage(settingsItem, "SiteManifest");
            str.MaskIcon = GetFieldImage(settingsItem, "MaskIcon");
            str.MaskIconColor = GetFieldString(settingsItem, "MaskIconColor");
            str.BrowserConfig = GetFieldImage(settingsItem, "BrowserConfig");
            str.TileColor = GetFieldString(settingsItem, "TileColor");
            str.ThemeColor = GetFieldString(settingsItem, "ThemeColor");
            return str;
        }

        protected virtual string GetFieldImage(Item item, string fieldName)
        {
            Field field = item.Fields[fieldName];
            if (field != null)
            {
                return GetImageUrl(field);
            }

            return null;
        }

        protected virtual string GetFieldString(Item item, string fieldName)
        {
            Field field = item.Fields[fieldName];
            if (field != null)
            {
                return field.Value;
            }

            return null;
        }

        protected virtual Item GetSettingsItem(Item contextItem)
        {
            return ServiceLocator.ServiceProvider.GetService<IMultisiteContext>().GetSettingsItem(contextItem);
        }

        private static string GetImageUrl(Field field)
        {
            string empty = null;
            if (field != null)
            {
                ImageField imageField = (ImageField)field;
                if (imageField != null && imageField.MediaItem != null)
                {
                    return MediaManager.GetMediaUrl((MediaItem)imageField.MediaItem, new MediaUrlOptions());
                }
            }

            return empty;
        }
    }
}