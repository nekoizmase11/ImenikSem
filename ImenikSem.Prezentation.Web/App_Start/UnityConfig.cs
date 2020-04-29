using ImenikSem.Bussines;
using ImenikSem.Data;
using ImenikSem.Data.UnitOfWorkSve;
using ImenikSem.Data.UnitOfWorkSve.Interfejsi;
using System.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace ImenikSem.Prezentation.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            
            container.RegisterType<IBiznis, Biznis>();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new InjectionConstructor(new ImenikBazaContext()));


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}