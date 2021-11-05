using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MBKM.Presentation.Helper
{
    public class MBKMAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string area = filterContext.RequestContext.RouteData.DataTokens["area"].ToString();

            if (area.ToLower() == "admin")
            {
                if (filterContext.HttpContext.Session["nopegawai"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Admin/Adminlogin/Login");
                    return;
                }
            }
            else if (area.ToLower() == "portal")
            {
                if (filterContext.HttpContext.Session["emailMahasiswa"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Portal/Home");
                    return;
                }
            }
        }
        //private const string IS_AUTHORIZED = "isAuthorized";

        //public string RedirectUrl = "/UnAuthorized";
        //protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        //{
        //    bool isAuthorized = base.AuthorizeCore(httpContext);

        //    httpContext.Items.Add(IS_AUTHORIZED, isAuthorized);

        //    return isAuthorized;
        //}

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);

        //    //var isAuthorized = filterContext.HttpContext.Items[IS_AUTHORIZED] != null
        //    //    ? Convert.ToBoolean(filterContext.HttpContext.Items[IS_AUTHORIZED])
        //    //    : false;

        //    string area = filterContext.RequestContext.RouteData.DataTokens["area"].ToString();

        //    if (area.ToLower() == "admin")
        //    {
        //        if (filterContext.HttpContext.Session["nopegawai"] == null)
        //        {
        //            filterContext.Result = new RedirectResult("~/Admin/Adminlogin/Login");
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        if (filterContext.HttpContext.Session["email"] == null)
        //        {
        //            filterContext.Result = new RedirectResult("~/Portal/Home");
        //            return;
        //        }
        //    }
        //    if (!IsUserAuthenticated(filterContext))
        //    {
        //        //ErrorLogging errLog = new ErrorLogging();
        //        //errLog.WriteLog(filterContext.Controller.ToString(), filterContext.ToString());
        //        filterContext.RequestContext.HttpContext.Response.Redirect(RedirectUrl);
        //    }
        //}

        //private bool IsUserAuthenticated(AuthorizationContext filterContext)
        //{

        //    bool result = true;
        //    string controller = filterContext.RequestContext.RouteData.Values["controller"].ToString();
        //    string action = filterContext.RequestContext.RouteData.Values["action"].ToString();
        //    var area = filterContext.RequestContext.RouteData.DataTokens["area"];
        //    string Url = "/" + controller;
        //    if (area != null)
        //        Url = "/" + area.ToString() + Url;

        //    return result;
        //}
    }
}