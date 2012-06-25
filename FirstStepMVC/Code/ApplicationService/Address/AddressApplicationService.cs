using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using FirstStepMVC.Code.Indexes.Address;
using Raven.Client.Linq;

namespace FirstStepMVC.Code.ApplicationService.Address
{
    public class AddressApplicationService : ApplicationServiceBase<Domain.Address>
    {
        public AddressApplicationService(IDocumentSession session) : base(session)
        {
        }

        public override IEnumerable<Domain.Address> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Session.Query<Domain.Address>()
                              .ToList();
            }

            return Session.Query<Address_FullSearch.ReduceResult, Address_FullSearch>()
                          .Where(q => q.Query == query)
                          .As<Domain.Address>()
                          .ToList();
        }
    }
}