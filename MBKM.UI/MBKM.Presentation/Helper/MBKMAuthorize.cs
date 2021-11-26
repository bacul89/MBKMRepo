using MBKM.Entities.Models;
using MBKM.Services;
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
        private IMenuRoleService _menuRoleService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            string area = filterContext.RequestContext.RouteData.DataTokens["area"].ToString();
            
            if (area.ToLower() == "admin")
            {
                filterContext.HttpContext.Session["emailMahasiswa"] = null;
                if (filterContext.HttpContext.Session["nopegawai"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Admin/Adminlogin/Login");
                    return;
                }
                string controllername = filterContext.RequestContext.RouteData.Values["controller"].ToString();
                string actionname = filterContext.RequestContext.RouteData.Values["action"].ToString();
                bool isauthorize = true;
                string url = "/" + area + "/" + controllername;
                if(controllername.ToLower() == "jadwalkuliah")
                {
                    if (actionname.ToLower() == "manage")
                        url = url + "/" + actionname;
                }
                if (filterContext.HttpContext.Session["MenuList"] == null)
                {
                    //TODO mengambil data menu berdasarkan Role ID
                    
                    _menuRoleService = DependencyResolver.Current.GetService<IMenuRoleService>();
                    double RoleId = Double.Parse(filterContext.HttpContext.Session["RoleID"].ToString());
                    var menuRoleList = _menuRoleService.Find(x => x.IsActive == true && x.IsDeleted == false && x.RoleID == RoleId && x.IsView == true).ToList();
                    filterContext.HttpContext.Session["MenuList"] = menuRoleList.Select(x => x.Menus).Where(y => y.MenuParent == null && y.IsActive == true && y.IsDeleted == false).OrderBy(x=>x.MenuOrder).ToList();
                    filterContext.HttpContext.Session["MenuListSub"] = menuRoleList.Select(x => x.Menus).Where(y => y.MenuParent != null && y.IsActive == true && y.IsDeleted == false).OrderBy(x => x.MenuOrder).ToList();
                    isauthorize = menuRoleList.Any(x => x.IsView == true && x.Menus.MenuUrl.ToLower() == url.ToLower());
                    filterContext.HttpContext.Session["MenuRole"] = menuRoleList;
                    if (!isauthorize)
                    {
                        filterContext.Result = new RedirectResult("~/UnAuthorized");
                        return;
                    }

                }
                else
                {
                    var ListMenuRole = (List<MenuRole>)filterContext.HttpContext.Session["MenuRole"];
                    isauthorize = ListMenuRole.Any(x => x.IsView == true && x.Menus.MenuUrl.ToLower() == url.ToLower());
                    if (!isauthorize)
                    {
                        filterContext.Result = new RedirectResult("~/UnAuthorized");
                        return;
                    }
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