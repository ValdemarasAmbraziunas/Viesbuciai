using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITPPro.Data;
using ITPPro.ViewModels;
using ITPPro.Exceptions;
using ITPPro.Models;
using WebMatrix.WebData;
using System.Security.Cryptography;
using System.Text;

namespace ITPPro.Controllers
{
    public class Valdytojo_registracijosController : BaseController
    {
        public Valdytojo_registracijosController(BaseRepository repository) : base(repository) { }

        // GET: Valdytojo_registracijos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {


            ValdytojoRegictracijosViewModel model = new ValdytojoRegictracijosViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(ValdytojoRegictracijosViewModel model)
        {
            try
            {
               
                if(ModelState.IsValid)
                {
                    bool userExists = repository.Set<Darbuotojas>().Any(x => x.el_pastas == model.Email);
                    bool userExists2 = repository.Set<Klientas>().Any(x => x.el_pastas == model.Email);
                    if (userExists || userExists2)
                        throw new ITPProException("Toks el. paštas jau registruotas sistemoje");
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

                    WebSecurity.CreateUserAndAccount(model.Email, model.Password, new
                    {
                       
                        slaptazodis = savedPasswordHash,
                        vardas = model.Name,
                        pavarde = model.Surname,
                        el_pastas = model.Email,
                        adresas = model.Address,
                        lytis = model.Gender,
                        telefonas = model.Phone,
                        darbo_pradzios_laikas = DateTime.Now,
                        darbuotojo_tipas = 1
                    });

                    return RedirectToAction("Index", "Home");
                }

            }
            catch(ITPProException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }


    }
}