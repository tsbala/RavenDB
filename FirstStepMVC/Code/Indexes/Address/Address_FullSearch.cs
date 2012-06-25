using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;


namespace FirstStepMVC.Code.Indexes.Address
{
    public class Address_FullSearch : AbstractIndexCreationTask<Domain.Address, Address_FullSearch.ReduceResult>
    {
        public class ReduceResult
        {
            public string Query { get; set; }
        }

        public Address_FullSearch()
        {
            Map = addresses => from a in addresses
                               select new {
                                  Query = new[] { a.HouseNumber, a.HouseName, a.StreetName, a.Town, a.County, a.Country, a.Postcode }
                               };
            Indexes.Add(r => r.Query, FieldIndexing.Analyzed);
        }

    }
}