using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using System.Web.Http.Dependencies;
using ProfileParsing.Data.Contracts;
using ProfileParsing.Data.Repositories;
using ProfileParsing.Domain.Contracts;
using ProfileParsing.Domain.Services;

namespace ProfileParsing
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterIOC();
        }

         public void RegisterIOC()
        {            
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder
                .RegisterType<ProfileRep>()
                .As<IProfileRep>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Domain.Services.ProfileParsing>()
                .As<IProfileParsing>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ProfileQuery>()
                .As<IProfileQuery>()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}