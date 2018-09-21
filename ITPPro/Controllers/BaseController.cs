using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITPPro.Data;
using ITPPro.Security;

namespace ITPPro.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly BaseRepository repository;

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        public BaseController(BaseRepository repository)
        {
            this.repository = repository;
        }

       /* protected void AddError(string message)
        {
            TempData[Constants.ErrorMessageKey] = message;
        }*/

      /*  protected void AddError(Exception ex)
        {
            AddError(ex.Message);
        }*/

       /* protected void AddSuccess(string message)
        {
            TempData[Constants.SuccessMessageKey] = message;
        }*/
    }
}