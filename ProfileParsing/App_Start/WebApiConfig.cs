using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ProfileParsing.Data.Contracts;
using ProfileParsing.Data.Repositories;
using ProfileParsing.Domain.Contracts;
using ProfileParsing.Domain.Services;
using System.Web.Http.Dependencies;

namespace ProfileParsing
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );
            var container = RegisterIoc();
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer RegisterIoc()
        {
            var monogConnectStr = ConfigurationManager.AppSettings["MongoConnect"];
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder
                .RegisterType<ProfileRep>()
                .As<IProfileRep>()
                .WithParameter(new TypedParameter(typeof(string), monogConnectStr))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Domain.Services.ProfileParsing>()
                .As<IProfileParsing>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ProfileQuery>()
                .As<IProfileQuery>()
                .InstancePerLifetimeScope();

            return builder.Build();
            
        }
    }
}
