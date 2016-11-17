using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Http.Dependencies;
using ProfileParsing.Data.Contracts;
using ProfileParsing.Data.Repositories;

namespace ProfileParsing
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterIOC();
        }

        private void RegisterIOC()
        {            
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder
                .RegisterType<ProfileRep>()
                .As<IProfileRep>()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}