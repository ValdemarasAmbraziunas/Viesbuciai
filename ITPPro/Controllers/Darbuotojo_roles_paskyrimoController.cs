using ITPPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITPPro.ViewModels;
using ITPPro.Models;
using ITPPro.Exceptions;
using System.Security.Cryptography;

namespace ITPPro.Controllers
{
    [Authorize]
    public class Darbuotojo_roles_paskyrimoController : BaseController
    {
        public Darbuotojo_roles_paskyrimoController(BaseRepository repository) : base(repository) { }

        // GET: Darbuotojo_roles_paskyrimo
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
                var model = new ClientsViewModel();
                if(client != null)
                {
                    model.id = client.kliento_kodas;
                    model.Name = client.vardas;
                    model.Surname = client.pavarde;
                    model.Email = client.el_pastas;
                    model.Phone = client.telefonas;
                    model.Address = client.adresas;
                    model.Gender = client.lytis;
                   
                }

                return View(model);
            }
            catch(ITPProException ex)
            {
                return RedirectToAction("Sistemos_klientu_langas");
            }
        }

        public ActionResult Darbuotojo_roles_paskyrimo_langas(int clientid)
        {
            try
            {
                var model = new CreateJobOfferModel();
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
        public ActionResult Register(CreateJobOfferModel model, int clientid)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.RepeatPassword)
                    throw new ITPProException("Slaptažodis blogai įvestas");

                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                var jobOffer = repository.Set<Darbo_uzklausa>().Add(new Darbo_uzklausa
                {
                    pareigos = model.Job,
                    fk_Klientaskliento_kodas = clientid,
                    fk_Darbuotojasdarbuojo_kodas = CurrentUser.UserId,
                    slaptazodis = savedPasswordHash
                   
                });
                repository.SaveChanges();
                return RedirectToAction("Sistemos_klientu_langas");
            }
            return View(model);
        }

    }
}