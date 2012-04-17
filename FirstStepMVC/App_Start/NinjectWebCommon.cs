using System.Reflection;
using Domain;
using FirstStepMVC.Code.ApplicationService;
using FirstStepMVC.Code.ApplicationService.Address;
using FirstStepMVC.Code.ApplicationService.Student;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Database.Server;

[assembly: WebActivator.PreApplicationStartMethod(typeof(FirstStepMVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(FirstStepMVC.App_Start.NinjectWebCommon), "Stop")]

namespace FirstStepMVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IStudentApplicationService>().To<StudentApplicationService>();
            kernel.Bind<IApplicationService<Address>>().To<AddressApplicationService>();
            kernel.Bind<IDocumentStore>()
                  .ToMethod(ctx => {
                                        var documentStore = new EmbeddableDocumentStore {DataDirectory = "App_Data/DocumentStore", UseEmbeddedHttpServer = true};
                                        NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
                                        documentStore.Initialize();
                                        IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), documentStore);
                                        return documentStore;
                                   })
                  .InSingletonScope();
            kernel.Bind<IDocumentSession>()
                  .ToMethod(ctx => ctx.Kernel.Get<IDocumentStore>().OpenSession())
                  .InRequestScope()
                  .OnDeactivation(s => s.SaveChanges());
        }
    }
}
