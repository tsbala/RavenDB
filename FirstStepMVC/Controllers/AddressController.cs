using System.Linq;
using System.Web.Mvc;
using Domain;
using FirstStepMVC.Code.ApplicationService;

namespace FirstStepMVC.Controllers
{
    public class AddressController : Controller
    {
        private readonly IApplicationService<Address> _addressApplicationService;

        public AddressController(IApplicationService<Domain.Address> addressApplicationService)
        {
            _addressApplicationService = addressApplicationService;
        }

        public ActionResult Index(string query)
        {
            var addresses = _addressApplicationService.Search(query);

            if (!addresses.Any())
            {
                addresses = _addressApplicationService.PopulateDummyDataAndReturnAsList();
            }

            return View(addresses);
        }
    }
}
