using ITPPro.Data;
using ITPPro.Exceptions;
using ITPPro.Models;
using ITPPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITPPro.Controllers
{
    public class Kliento_veiksmu_apribojimoController : BaseController
    {

        public Kliento_veiksmu_apribojimoController(BaseRepository repository) : base(repository) { }
        // GET: Kliento_veiksmu_apribojimo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sistemos_klientu_langas(int page = 1, int items = 10)
        {
            if (page < 1)
                page = 1;

            ViewData["page"] = page;
            ViewData["items"] = items;

            var model = repository.Set<Klientas>()
                .OrderBy(x => x.kliento_kodas)
                .Skip((page - 1) * items)
                .Take(items)
                .Select(x => new ClientsViewModel()
                {
                    id = x.kliento_kodas,
                    Name = x.vardas,
                    Surname = x.pavarde,
                    Gender = x.lytis,
                    Phone = x.telefonas,
                    Address = x.adresas,
                    Email = x.el_pastas
                });

            return View(model);
        }

        public ActionResult Kliento_informacijos_langas(int id = 0)
        {
            try
            {
                Klientas client = repository.Set<Klientas>().Find(id);
                Darbuotojas emp = repository.Set<Darbuotojas>().Find(CurrentUser.UserId);
                Viesbutis hotel = repository.Set<Viesbutis>().Find(emp.fk_Viesbutisid);
                Teises rights = repository.Set<Teises>().Where(x => x.viesbuciu_tinklas == hotel.viesbuciu_tinklas && x.fk_Klientaskliento_kodas == id).FirstOrDefault();
                bool isRestricted;
                if (rights.data_iki < DateTime.Now)
                {
                    isRestricted = false;
                }
                else
                    isRestricted = true;
                var model = new ClientsViewModel();
                if (client != null)
                {
                    model.id = client.kliento_kodas;
                    model.Name = client.vardas;
                    model.Surname = client.pavarde;
                    model.Email = client.el_pastas;
                    model.Phone = client.telefonas;
                    model.Address = client.adresas;
                    model.Gender = client.lytis;
                    model.isRestricted = isRestricted;

                }

                return View(model);
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Sistemos_klientu_langas");
            }
        }

        public ActionResult Kliento_teisiu_apribojimo_langas(int clientid)
        {
            try
            {
                var model = new RestrictRightsViewModel();
                if (clientid > 0)
                {
                    ViewData["code"] = clientid;
                }

                return View(model);
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Sistemos_klientu_langas");
            }
        }

        [HttpPost]
        public ActionResult Irasyti(RestrictRightsViewModel model, int clientid)
        {
            if (ModelState.IsValid)
           {
                Darbuotojas emp = repository.Set<Darbuotojas>().Find(CurrentUser.UserId);
                Viesbutis hotel = repository.Set<Viesbutis>().Find(emp.fk_Viesbutisid);
                Teises rights = repository.Set<Teises>().Where(x => x.viesbuciu_tinklas == hotel.viesbuciu_tinklas && x.fk_Klientaskliento_kodas == clientid).FirstOrDefault();
                rights.priezastis = model.Reason;
                rights.data_iki = model.DateEnd;
                rights.teisiu_statusas = false;
                repository.SaveChanges();
            }
            return RedirectToAction("Sistemos_klientu_langas");
        }
    }
}