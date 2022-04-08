using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NorthWind.Controllers
{
    public class CheckLoginState:ActionFilterAttribute
    {
        

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
            HttpContext context = HttpContext.Current;
            LoginRoute(filterContext.RouteData, context);
        }

        

        void LoginRoute(RouteData routeData, HttpContext context) 
        {
            string controllerName = routeData.Values["controller"].ToString();
            string actionName = routeData.Values["action"].ToString();

            if (controllerName != "Home")
                LoginState(context);
            else if           
                (actionName == "Order")
                LoginStateMember(context);
            else if
                (actionName == "Details" && controllerName=="Home")
                LoginStateDetails(context);

        }



        void LoginState(HttpContext context) 
        { 
        if (context.Session["user"] ==null ) 
            {
               context.Response.Redirect("/Manager/Login");
            }

        }

        void LoginStateMember(HttpContext context)
        {
            if (context.Session["member"] == null)
            {
                context.Response.Redirect("/Home/Login?q=1");
            }

        }

        void LoginStateDetails(HttpContext context)
        {
            if (context.Session["member"] == null)
            {
                context.Response.Redirect("/Home/Login");
            }

        }

    }
}