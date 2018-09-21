using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ITPPro.Models;
using ITPPro.Data;
using ITPPro.ViewModels;

namespace ITPPro.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        public ManageController(BaseRepository repository) : base(repository) { }



        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            int userId = CurrentUser.UserId;
            var user = repository.Set<Darbuotojas>().Find(userId);
            var model = new ManageViewModel();
            if (user != null)
            {
                model = new ManageViewModel
                {
                    Email = user.el_pastas,
                    Name = user.vardas,
                    Surname = user.pavarde,
                };
            }
            else
            {
                throw new Exception("No user");
            }
            return View(model);
        }


    }
}