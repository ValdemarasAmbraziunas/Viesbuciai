using ITPPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPPro.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(BaseRepository repository) : base(repository) { }
        public ActionResult Index()
        {
            return View();
        }

    }
}