using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MBKM.Presentation.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MBKMAuthorize : AuthorizeAttribute
    {
        private const string IS_AUTHORIZED = "isAuthorized";

        public string RedirectUrl = "/UnAuthorized";
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            bool isAuthorized = base.AuthorizeCore(httpContext);

            httpContext.Items.Add(IS_AUTHORIZED, isAuthorized);

            return isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var isAuthorized = filterContext.HttpContext.Items[IS_AUTHORIZED] != null
                ? Convert.ToBoolean(filterContext.HttpContext.Items[IS_AUTHORIZED])
                : false;

            string area = filterContext.RequestContext.RouteData.DataTokens["area"].ToString();


            if (filterContext.HttpContext.Session["email"] == null)
            {
                if (area.ToLower() == "admin")
                {
                    filterContext.Result = new RedirectResult("~/Admin/Adminlogin/Login");
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Portal/Home");
                    return;
                }
            }
            if (!IsUserAuthenticated(filterContext))
            {
                //ErrorLogging errLog = new ErrorLogging();
                //errLog.WriteLog(filterContext.Controller.ToString(), filterContext.ToString());
                filterContext.RequestContext.HttpContext.Response.Redirect(RedirectUrl);
            }
        }

        private bool IsUserAuthenticated(AuthorizationContext filterContext)
        {

            bool result = false;
            string controller = filterContext.RequestContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RequestContext.RouteData.Values["action"].ToString();
            var area = filterContext.RequestContext.RouteData.DataTokens["area"];
            string Url = "/" + controller;
            if (area != null)
                Url = "/" + area.ToString() + Url;

            return result;
        }
    }
}