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
using ProfileParsing.Controllers;
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
        }
    }
}