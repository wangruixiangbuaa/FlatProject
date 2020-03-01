using HPIT.Data.Core;
using HPIT.Evalute.Data;
using HPIT.Evalute.Data.Model;
using HPIT.Flat.Data.Adapters;
using HPIT.Flat.Data.Entitys;
using HPIT.Flat.Portal.Common;
using HPIT.Flat.Portal.Models;
using HPIT.Web.Core.Deluxe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Flat.Portal.Controllers
{
    public class DormController : Controller
    {
        // GET: Dorm
        public ActionResult Index()
        {
            return View();
        }

        public DeluxeJsonResult QueryPageData(SearchModel<Dorm> search)
        {
            int total = 0;
            HPITMemberInfo currentUser = DeluxeUser.CurrentMember;
            search.UserName = currentUser.RealName;
            search.RoleName = currentUser.FullName;
            var result = DormDal.Instance.GetPageData(search, out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total, TotalPages = totalPages });
        }

        public DeluxeJsonResult QueryBatchs()
        {
            List<string> batchs = EvaluteDal.Instance.GetBatchs();
            return new DeluxeJsonResult(new { Data = batchs });
        }

        public DeluxeJsonResult QueryProjectNames()
        {
            List<string> projectNames = EvaluteDal.Instance.GetProjectNames();
            return new DeluxeJsonResult(new { Data = projectNames });
        }

        public DeluxeJsonResult Save(Dorm dorm)
        {
            int result = 0;
            if (string.IsNullOrEmpty(dorm.DID))
            {
               result = DormDal.Instance.AddDorm(dorm);
            }
            else
            {
                result = DormDal.Instance.UpdateDorm(dorm);
            }
            return new DeluxeJsonResult(new { Data = result, State = 200 });
        }

        public DeluxeJsonResult DormAssign(AssignModel assign)
        {
            int result = 0;
            result = DormDal.Instance.DormAssign(assign.DormAssigns);
            return new DeluxeJsonResult(new { Data = result, State = 200 });
        }

        public DeluxeJsonResult QueryStudentByBatch(string batch,string direction,string projectName)
        {
            List<EvalStudent> students = EvaluteDal.Instance.GetStudentsByBatchs(batch,direction,projectName);
            return new DeluxeJsonResult(new { Data = students });
        }
    }
}