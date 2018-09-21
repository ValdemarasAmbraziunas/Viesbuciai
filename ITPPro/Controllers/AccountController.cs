using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ITPPro.Models;
using ITPPro.Data;
using WebMatrix.WebData;
using ITPPro.ViewModels;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Collections.Generic;
using System.Security.Cryptography;
using ITPPro.Security;
using ITPPro.Exceptions;
using ITPPro.Models.Enums;

namespace ITPPro.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(BaseRepository repository) : base(repository) { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var user = User as CustomPrincipal;

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (WebSecurity.Login(model.Email, model.Password, false))
            {
                int userId = WebSecurity.GetUserId(model.Email);
                var user = repository.Set<Darbuotojas>().Find(userId);
                if (user != null)
                {

                    var serializableModel = new Darbuotojas();
                    serializableModel.darbuojo_kodas = userId;
                    serializableModel.el_pastas = user.el_pastas;
                    serializableModel.darbuotojo_tipas = user.darbuotojo_tipas;

                    var serializer = new JavaScriptSerializer();
                    string userData = serializer.Serialize(serializableModel);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        user.el_pastas, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    throw new Exception("user not found");
                }

            }
            else
            {
                bool userExists2 = repository.Set<Klientas>().Any(x => x.el_pastas == model.Email);
                if (userExists2)
                {
                    var user2 = repository.Set<Klientas>().Where(x => x.el_pastas == model.Email).First();
                    if (user2 != null)
                    {
                        /* Fetch the stored value */
                        string savedPasswordHash = user2.slaptazodis;
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
                        {
                            byte first = hashBytes[i + 16];
                            byte second = hash[i];
                            if (hashBytes[i + 16] != hash[i])
                                throw new UnauthorizedAccessException();
                        }
                        var serializableModel = new Darbuotojas();
                        serializableModel.darbuojo_kodas = user2.kliento_kodas;
                        serializableModel.el_pastas = user2.el_pastas;
                        Darbuotoju_Tipai_Enum test = new Darbuotoju_Tipai_Enum();
                        test.id = 0;
                        serializableModel.darbuotojo_tipas = test;

                        var serializer = new JavaScriptSerializer();
                        string userData = serializer.Serialize(serializableModel);

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                            user2.el_pastas, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);

                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("Index", "Home");
                        //Kaip geriausia paprastai prijungti ji? :D
                    }
                }
                else
                    throw new Exception("user not found");
            }
            return RedirectToAction("Login");

        }


        //
        [HttpGet]
        public ActionResult Register()
        {

            ValdytojoRegictracijosViewModel model = new ValdytojoRegictracijosViewModel();
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(ValdytojoRegictracijosViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
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

                    var client = repository.Set<Klientas>().Add(new Klientas
                    {
                        vardas = model.Name,
                        pavarde = model.Surname,
                        el_pastas = model.Email,
                        slaptazodis = savedPasswordHash,
                        adresas = model.Address,
                        lytis = model.Gender,
                        telefonas = model.Phone,
                        sukurimo_data = DateTime.Now

                    });
                    repository.SaveChanges();
                    int clientid = repository.Set<Klientas>().Where(x => x.el_pastas == model.Email).Select(x => x.kliento_kodas).FirstOrDefault();
                    List<string> HotelsNet = repository.Set<Viesbutis>().Select(x => x.viesbuciu_tinklas).ToList();
                    List<string> uniqNet = HotelsNet.Distinct().ToList();
                    if (uniqNet != null)
                    {
                        IEnumerable<Teisiu_Tipo_Enum> teisiu_tipas = repository.Set<Teisiu_Tipo_Enum>();
                        foreach (string value in uniqNet)
                        {
                            var rights = repository.Set<Teises>().Add(new Teises
                            {
                                teisiu_statusas = true,
                                viesbuciu_tinklas = value,
                                fk_Klientaskliento_kodas = clientid,
                                tipas = teisiu_tipas.First(),
                                data_iki = DateTime.Now

                            });
                        }
                    }
                    repository.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }

            }
            catch (ITPProException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }




        public ActionResult Logout()
        {
            WebSecurity.Logout();
            //Cia reik panaikint cookie, kad atsijungtum

            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}