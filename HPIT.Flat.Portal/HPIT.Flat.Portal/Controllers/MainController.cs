using HPIT.Evalute.Data.Model;
using HPIT.Flat.Data.Adapter;
using HPIT.Flat.Data.ExtEntitys;
using HPIT.Flat.Portal.Common;
using HPIT.Web.Core.Deluxe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Flat.Portal.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.User = DeluxeUser.CurrentMember;
            return View();
        }

        public DeluxeJsonResult GetCurrentRoleMenus()
        {
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            List<MenuExt> menuList = new List<MenuExt>();
            menuList = MenuDal.Instance.GetMenusByRoleName(currentUser.FullName);
            return new DeluxeJsonResult(new { data = menuList, code = 200 }, "yyyy-MM-dd HH:mm");
        }
    }
}