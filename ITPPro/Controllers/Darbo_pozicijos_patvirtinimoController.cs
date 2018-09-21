using ITPPro.Data;
using ITPPro.Exceptions;
using ITPPro.Models;
using ITPPro.Models.Enums;
using ITPPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace ITPPro.Controllers
{
    public class Darbo_pozicijos_patvirtinimoController : BaseController
    {

        public Darbo_pozicijos_patvirtinimoController(BaseRepository repository) : base(repository) { }
        // GET: Darbo_pozicijos_patvirtinimo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Darbo_poziciju_langas(int page = 1, int items = 10)
        {
            if (page < 1)
                page = 1;

            ViewData["page"] = page;
            ViewData["items"] = items;
            List<Viesbutis> hotels = repository.Set<Viesbutis>().ToList();
            var model = repository.Set<Darbo_uzklausa>()
                .Where(x => x.fk_Klientaskliento_kodas == CurrentUser.UserId)
                .OrderBy(x => x.id)
                .Skip((page - 1) * items)
                .Take(items)
                .Select(x => new LookUpJobOffers()
                {
                    id = x.id,
                    Job = x.pareigos,
                    darbdavio_id = x.fk_Darbuotojasdarbuojo_kodas,
                    HotelNet = "to be implemented"

                });


            return View(model);
        }

        public ActionResult Darbo_pozicijos_patvirtinimo_langas(int id = 0)
        {

            try
            {
                var model = new AcceptJobOffer();
                if (id > 0)
                {
                    ViewData["code"] = id;
                }

                return View(model);
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Darbo_poziciju_langas");
            }
        }

        [HttpPost]
        public ActionResult Accept(AcceptJobOffer model, int id)
        {

            if (ModelState.IsValid)
            {
                var jobOffer = repository.Set<Darbo_uzklausa>().Where(x => x.id == id).First();
                bool isTheSame = false;
                if (model.Password.Equals(model.RepeatPassword))
                {
                    isTheSame = true;
                }
                if (jobOffer != null && isTheSame)
                {
                    /* Fetch the stored value */
                    string savedPasswordHash = jobOffer.slaptazodis;
                    /* Extract the bytes */
                    byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                    /* Get the salt */
                    byte[] salt = new byte[16];
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                    /* Compute the hash on the password the user entered */
                    var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    /* Compare the results */
                    for (int i = 0; i < 20; i++)
                        if (hashBytes[i + 16] != hash[i])
                            throw new UnauthorizedAccessException();

                    Klientas customer = repository.Set<Klientas>().Where(x => x.kliento_kodas == CurrentUser.UserId).First();
                    var owner = repository.Set<Darbuotojas>().Where(x => x.darbuojo_kodas == jobOffer.fk_Darbuotojasdarbuojo_kodas).First();
                    List<Viesbutis> hotels = repository.Set<Viesbutis>().Where(x => x.fk_savininkas == owner.darbuojo_kodas).ToList();

                    //Pirma kart prisijungus reiktu leist pasikeist :D
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                    pbkdf2 = new Rfc2898DeriveBytes("laikinas", salt, 10000);
                    hash = pbkdf2.GetBytes(20);

                    hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    savedPasswordHash = Convert.ToBase64String(hashBytes);
                    int type = 0;
                    if (jobOffer.pareigos.Equals("Buhalteris"))
                        type = 2;
                    else
                        type = 3;
                    WebSecurity.CreateUserAndAccount(customer.el_pastas, "laikinas", new
                    {

                        slaptazodis = savedPasswordHash,
                        vardas = customer.vardas,
                        pavarde = customer.pavarde,
                        el_pastas = customer.el_pastas,
                        adresas = customer.adresas,
                        lytis = customer.lytis,
                        telefonas = customer.telefonas,
                        darbo_pradzios_laikas = DateTime.Now,
                        darbuotojo_tipas = type,
                        fk_Viesbutisid = hotels[0].id

                    });
                    repository.SaveChanges();
                    var newEmp = repository.Set<Darbuotojas>().Where(x => x.el_pastas == customer.el_pastas).First();
                    foreach (var hotel in hotels)
                    {
                        IEnumerable<Teisiu_Tipo_Enum> teisiu_tipas = repository.Set<Teisiu_Tipo_Enum>();


                        var rights = repository.Set<Teises>().Add(new Teises
                        {
                            teisiu_statusas = true,
                            viesbuciu_tinklas = hotel.viesbuciu_tinklas,
                            viesbutis = hotel.pavadinimas,
                            fk_Darbuotojasdarbuojo_kodas = newEmp.darbuojo_kodas,
                            tipas = teisiu_tipas.Last(),
                            data_iki = DateTime.Now
                        });
                    }
                    repository.SaveChanges();

                    List<Teises> oldRights = repository.Set<Teises>().Where(x => x.fk_Klientaskliento_kodas == CurrentUser.UserId).ToList();
                    if (oldRights != null)
                    {
                        foreach (var rights in oldRights)
                        {
                            repository.Set<Teises>().Remove(rights);
                        }
                        repository.SaveChanges();
                    }
                    repository.Set<Darbo_uzklausa>().Remove(jobOffer);
                    repository.SaveChanges();
                    repository.Set<Klientas>().Remove(customer);
                    repository.SaveChanges();


                    return RedirectToAction("Logout", "Account");


                }
                return RedirectToAction("Sistemos_klientu_langas");
            }
            return View(model);
        }

    }
}