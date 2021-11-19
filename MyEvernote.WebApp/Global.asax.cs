using MyEvernote.BusinessLayer;
using MyEvernote.Common;
using MyEvernote.WebApp.Init;
using MyEvernote.WebApp.Models;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyEvernote.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            MockManager.Initialize();
            App.Common = new WebCommon();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            CacheHelper.RemoveCategoriesFromCache();
            MockManager.Initialize();
        }
    }
}
