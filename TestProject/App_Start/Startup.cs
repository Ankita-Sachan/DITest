using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Ninject;
using ServicesAndInterfaces.Interfaces;
using ServicesAndInterfaces.Services;
using TestProject.DependencyResolver;

[assembly: OwinStartup(typeof(TestProject.App_Start.Startup))]

namespace TestProject.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IDataManager>().To<DataManager>();
            
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
