using System.Web.Mvc;
using BGL.Test.Portal.Controllers;
using BGL.Test.Service;
using BGL.Test.Service.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace BGL.Test.Portal
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IGitService, GitService>();
            container.RegisterType<IController, HomeController>("Home");

            return container;
        }
    }
}