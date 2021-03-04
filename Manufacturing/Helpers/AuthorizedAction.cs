using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Helpers
{
    public class AuthorizedAction : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
           
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
            if(filterContext.HttpContext.Session.GetString("EMailAddress") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        Controller = "Account",
                        Action = "Index",
                        returnUrl = filterContext.HttpContext.Request.Path.ToUriComponent()+""+filterContext.HttpContext.Request.QueryString.ToUriComponent()                    
                    }));
                return;
            }
        }
    }
}
