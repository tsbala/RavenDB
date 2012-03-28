using Raven.Client;
using Raven.Client.Embedded;

namespace FirstStepMVC.Code
{
    public class BootStrapper
    {
        private static IDocumentStore _documentStore { get; set; }
        
        public static IDocumentStore DocumentStore
        {
            get { return _documentStore; }
        }

        public static void Initialise()
        {
            if (_documentStore == null)
            {
                _documentStore = new EmbeddableDocumentStore {DataDirectory = "App_Data/DocumentStore"};
                _documentStore.Initialize();
            }
        }
    }
}