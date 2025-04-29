using System.Web.Http;
using Architecture.API.Repository._Repository.Implementations;
using Architecture.API.Repository._Repository.Interface;
using Architecture.API.Service._Service.Implementations;
using Architecture.API.Service._Service.Interfaces;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Architecture.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            // 🔥 在這裡註冊 Service、Repository
            container.RegisterType<IMemberService, MemberService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMemberRepository, MemberRepository>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}