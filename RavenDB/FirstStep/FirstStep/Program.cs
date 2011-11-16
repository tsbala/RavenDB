using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;

namespace FirstStep
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var documentStore = new DocumentStore {Url = "http://localhost:8080"}) 
            {
                documentStore.Initialize();
                using(var documentSession = documentStore.OpenSession())
                {

                }
            }
        }
    }
}
