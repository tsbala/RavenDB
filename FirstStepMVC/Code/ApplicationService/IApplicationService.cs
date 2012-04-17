using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Raven.Client;

namespace FirstStepMVC.Code.ApplicationService
{
    public interface IApplicationService<T>
    {
        void Add(T item);
        IEnumerable<T> Search(string query);
        void PopulateDummyData(int? size = null);
        IEnumerable<T> PopulateDummyDataAndReturnAsList();
    }

    public abstract class ApplicationServiceBase<T> : IApplicationService<T>
    {
        protected readonly IDocumentSession Session;

        protected ApplicationServiceBase(IDocumentSession session)
        {
            Session = session;
        }

        public void Add(T item)
        {
            Session.Store(item);
        }

        public abstract IEnumerable<T> Search(string query);

        public void PopulateDummyData(int? size = null)
        {
            var data = Builder<T>.CreateListOfSize(size ?? 10).Build();
            foreach (var item in data)
            {
                Session.Store(item);
            }
        }

        public IEnumerable<T> PopulateDummyDataAndReturnAsList()
        {
            PopulateDummyData();
            return Search(null);
        }
    }
}
