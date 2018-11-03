using Sitecore.XA.Foundation.Mvc.Controllers;
using SXA.Feature.MetadataExtended.Repository;

namespace SXA.Feature.MetadataExtended.Controllers
{
    public class FaviconExtendedController : StandardController
    {
        private readonly IFaviconExtendedRepository _faviconRepository;

        public FaviconExtendedController(IFaviconExtendedRepository faviconRepository)
        {
            this._faviconRepository = faviconRepository;
        }

        protected override object GetModel()
        {
            return (object)this._faviconRepository.GetFaviconModel(this.Rendering.Item);
        }
    }
}