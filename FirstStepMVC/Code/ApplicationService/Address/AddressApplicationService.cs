using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace FirstStepMVC.Code.ApplicationService.Address
{
    public class AddressApplicationService : ApplicationServiceBase<Domain.Address>
    {
        public AddressApplicationService(IDocumentSession session) : base(session)
        {
        }

        public override IEnumerable<Domain.Address> Search(string query)
        {
            return Session.Query<Domain.Address>().ToList();
        }
    }
}