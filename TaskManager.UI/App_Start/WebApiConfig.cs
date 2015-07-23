using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Practices.Unity;
using TaskManager.Data.Repositories;
using TaskManager.Domain.Abstract.Repositories;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Concrete.Services;
using TaskManager.UI.Infrastructure;

namespace TaskManager.UI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            var container = new UnityContainer();
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.RegisterType<ITaskService, TaskService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubTaskService, SubTaskService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoryService, CategoryService>(new HierarchicalLifetimeManager());

            container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            container.RegisterType<ITaskRepository, TaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubTaskRepository, SubTaskRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoryRepository, CategoryRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
