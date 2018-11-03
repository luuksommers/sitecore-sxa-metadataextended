using Sitecore.Data.Items;
using SXA.Feature.MetadataExtended.Models;

namespace SXA.Feature.MetadataExtended.Repository
{
    public interface IFaviconExtendedRepository
    {
        FaviconExtendedModel GetFaviconModel(Item contextItem);
    }
}