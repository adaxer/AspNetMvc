using Southwind.BusinessObjects;
using Southwind.CommonLib;
using Southwind.DataAccess;
using Southwind.Interfaces;
using Southwind.Models;
using System;
using System.Data.Entity;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace Southwind.NetMvcClient
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            string shopUrl = System.Configuration.ConfigurationManager.AppSettings["ShopUrl"].ToString();
            string loginUrl = System.Configuration.ConfigurationManager.AppSettings["LoginUrl"].ToString();
            container.RegisterType<IShopService>(new InjectionFactory(c=>new RestShopService(c.Resolve<IRestService>(), shopUrl)));
            container.RegisterType<IRestService, RestService>();
            container.RegisterType<IAuthenticationClient>(new InjectionFactory(c=>new AuthenticationClient(
                    new LoginData { LoginUrl = $"{loginUrl}", UserName = "test", Password = "test" })));
        }
    }
}