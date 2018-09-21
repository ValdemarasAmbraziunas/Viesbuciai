using ITPPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITPPro.Models;
using ITPPro.ViewModels;
using ITPPro.Exceptions;

namespace ITPPro.Controllers
{
    public class Darbuotoju_teisiu_priskyrimoController : BaseController
    {
        public Darbuotoju_teisiu_priskyrimoController(BaseRepository repository) : base(repository) { }
        // GET: Darbuotoju_teisiu_priskyrimo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Viesbucio_tinklo_Darbuotoju_langas(int page = 1, int items = 10)
        {
            if (page < 1)
                page = 1;

            ViewData["page"] = page;
            ViewData["items"] = items;

            var hotel = repository.Set<Viesbutis>().Where(x => x.fk_savininkas == CurrentUser.UserId).First();
            if (hotel != null)
            {
                var model = repository.Set<Darbuotojas>()
                    .Where(x => x.fk_Viesbutisid == hotel.id)
                    .OrderBy(x => x.darbuojo_kodas)
                    .Skip((page - 1) * items)
                    .Take(items)
                    .Select(x => new EmpViewModel()
                    {
                        id = x.darbuojo_kodas,
                        Name = x.vardas,
                        Surname = x.pavarde,
                        Phone = x.telefonas,
                        Email = x.el_pastas
                    });
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Darbuotojo_informacijos_langas(int id = 0)
        {
            try
            {
                Darbuotojas empl = repository.Set<Darbuotojas>().Find(id);
                var model = new EmpViewModel();
                if (empl != null)
                {
                    model.id = empl.darbuojo_kodas;
                    model.Name = empl.vardas;
                    model.Surname = empl.pavarde;
                    model.Email = empl.el_pastas;
                    model.Phone = empl.telefonas;
                    model.Address = empl.adresas;
                    model.Gender = empl.lytis;
                    model.StartTime = empl.darbo_pradzios_laikas;
                    model.Role = empl.darbuotojo_tipas.name.First().ToString().ToUpper() + empl.darbuotojo_tipas.name.Substring(1);
                }

                return View(model);
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Sistemos_klientu_langas");
            }
        }

        public ActionResult Darbuotojo_teisiu_keitimo_langas(int emplid)
        {
            try
            {
                
                if (emplid > 0)
                {
                    ViewData["code"] = emplid;

                   var model = repository.Set<Teises>()
                   .Where(x => x.fk_Darbuotojasdarbuojo_kodas == emplid)
                   .OrderBy(x => x.id)
                   .Select(x => new EmplRightsViewModel()
                   {
                       id = x.id,
                       Hotel = x.viesbutis,
                       RightsStatus = x.teisiu_statusas
                   });
                    return View(model);

                }

                return RedirectToAction("Viesbucio_tinklo_Darbuotoju_langas");
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Sistemos_klientu_langas");
            }
        }

        [HttpGet]
        public ActionResult Change(int id, int eid)
        {
            Teises right = repository.Set<Teises>().Where(x => x.id == id).FirstOrDefault();
            if (right.teisiu_statusas)
            {
                right.teisiu_statusas = false;
            }
            else
                right.teisiu_statusas = true;
            repository.SaveChanges();
            return RedirectToAction("Darbuotojo_teisiu_keitimo_langas", new { emplid = eid });
        }
    }
}