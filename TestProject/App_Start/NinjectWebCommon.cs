[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TestProject.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TestProject.App_Start.NinjectWebCommon), "Stop")]

namespace TestProject.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using ServicesAndInterfaces.Interfaces;
    using ServicesAndInterfaces.Services;
    using System.IO;
    using Ninject.Parameters;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Persons.xml");
                //CheckNode m = kernel.Get<CheckNode>(new ConstructorArgument("_fileName", fileName));
                kernel.Bind<ICheckNode>().To<CheckNode>().WithConstructorArgument(typeof(string), fileName);
                kernel.Bind<IDataManager>().To<DataManager>();
                
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Load(new TestProject.NinjectConfiguration());
        }        
    }
}
