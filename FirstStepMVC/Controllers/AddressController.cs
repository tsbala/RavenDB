using System.Linq;
using System.Web.Mvc;
using Domain;
using FirstStepMVC.Code.ApplicationService;

namespace FirstStepMVC.Controllers
{
    public class AddressController : Controller
    {
        private readonly IApplicationService<Address> _addresApplicationService;

        public AddressController(IApplicationService<Domain.Address> addresApplicationService)
        {
            _addresApplicationService = addresApplicationService;
        }

        //
        // GET: /Address/

        public ActionResult Index()
        {
            var addresses = _addresApplicationService.Search(null);

            if (!addresses.Any())
            {
                addresses = _addresApplicationService.PopulateDummyDataAndReturnAsList();
            }

            return View(addresses);
        }
    }
}
