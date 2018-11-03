using Sitecore.XA.Foundation.Mvc.Controllers;
using SXA.Feature.MetadataExtended.Repositories;

namespace SXA.Feature.MetadataExtended.Controllers.Controllers
{
    public class HrefLangController : StandardController
    {
        private readonly IHrefLangRepository _hrefLangRepository;

        public HrefLangController(IHrefLangRepository hrefLangRepository)
        {
            _hrefLangRepository = hrefLangRepository;
        }

        protected override object GetModel()
        {
            return _hrefLangRepository.GetModel();
        }
    }
}