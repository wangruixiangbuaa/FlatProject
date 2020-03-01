using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Filters
{
    public class ActionResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // throw new NotImplementedException();
            //if (context == null)
            //{
            //    throw new ArgumentNullException("context");
            //}
            //if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new InvalidOperationException("MvcResources.JsonRequest_GetNotAllowed");
            //}
            //HttpResponseBase base2 = context.HttpContext.Response;
            filterContext.HttpContext.Response.ContentType = "application/json";
            filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
            if (filterContext != null)
            {
                //转换System.DateTime的日期格式到 ISO 8601日期格式
                //ISO 8601 (如2008-04-12T12:53Z)
                IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
                //设置日期格式
                isoDateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                //序列化
                String jsonResult = JsonConvert.SerializeObject(filterContext.Result, isoDateTimeConverter);
                filterContext.HttpContext.Response.Write(jsonResult);
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            return;
        }
    }
}