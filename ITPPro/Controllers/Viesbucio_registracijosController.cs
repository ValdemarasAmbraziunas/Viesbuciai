using ITPPro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITPPro.ViewModels;
using ITPPro.Models;
using ITPPro.Models.Enums;
using ITPPro.Exceptions;

namespace ITPPro.Controllers
{
    [Authorize]
    public class Viesbucio_registracijosController : BaseController
    {

        public Viesbucio_registracijosController(BaseRepository repository) : base(repository) { }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterHotel()
        {
            var model = new CreateHotelViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterHotel(CreateHotelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hotel = repository.Set<Viesbutis>().Add(new Viesbutis
                {
                    pavadinimas = model.Title,
                    viesbuciu_tinklas = model.HotelsNet,
                    zvaigzduciu_sk = model.Stars,
                    miestas = model.City,
                    adresas = model.Address,
                    aprasymas = model.Description,
                    fk_savininkas = CurrentUser.UserId

                });
                repository.SaveChanges();
                var newHotel = repository.Set<Viesbutis>().Where(x => x.pavadinimas == model.Title && x.adresas == model.Address && x.miestas == model.City).FirstOrDefault();
                List<string> HotelsNet = repository.Set<Viesbutis>().Distinct().Where(x => x.id != newHotel.id).Select(x => x.viesbuciu_tinklas).ToList();
                if (!HotelsNet.Contains(model.HotelsNet))
                {

                    List<Klientas> clients = repository.Set<Klientas>().ToList();
                    IEnumerable<Teisiu_Tipo_Enum> teisiu_tipas = repository.Set<Teisiu_Tipo_Enum>();
                    if (clients != null)
                    {


                        foreach (var client in clients)
                        {
                            var rights = repository.Set<Teises>().Add(new Teises
                            {
                                teisiu_statusas = true,
                                viesbuciu_tinklas = newHotel.viesbuciu_tinklas,
                                viesbutis = newHotel.pavadinimas,
                                fk_Klientaskliento_kodas = client.kliento_kodas,
                                tipas = teisiu_tipas.First(),
                                data_iki = DateTime.Now
                            });
                        }
                    }
                    repository.SaveChanges();
                }

                var ownerfirstHotel = repository.Set<Viesbutis>().Where(x => x.fk_savininkas == CurrentUser.UserId).First();
                if (!ownerfirstHotel.Equals(newHotel))
                {
                    List<Darbuotojas> myEmp = repository.Set<Darbuotojas>().Where(x => x.fk_Viesbutisid == ownerfirstHotel.id).ToList();
                    if (myEmp != null)
                    {
                        IEnumerable<Teisiu_Tipo_Enum> teisiu_tipas = repository.Set<Teisiu_Tipo_Enum>();
                        foreach (var worker in myEmp)
                        {
                            var rights = repository.Set<Teises>().Add(new Teises
                            {
                                teisiu_statusas = true,
                                viesbuciu_tinklas = ownerfirstHotel.viesbuciu_tinklas,
                                viesbutis = newHotel.pavadinimas,
                                fk_Darbuotojasdarbuojo_kodas = worker.darbuojo_kodas,
                                tipas = teisiu_tipas.Last(),
                                data_iki = DateTime.Now
                            });
                        }
                    }
                    repository.SaveChanges();
                }

                return RedirectToAction("HotelModelList", "Viesbucio_registracijos");
            }
            return View(model);
        }


        public ActionResult HotelModelList(int page = 1, int items = 10)
        {
            if (page < 1)
                page = 1;

            ViewData["page"] = page;
            ViewData["items"] = items;

            var model = repository.Set<Viesbutis>()
                .Where(x => x.fk_savininkas == CurrentUser.UserId)
                .OrderBy(x => x.id)
                .Skip((page - 1) * items)
                .Take(items)
                .Select(x => new HotelsViewModel()
                {
                    id = x.id,
                    Title = x.pavadinimas,
                    HotelsNet = x.viesbuciu_tinklas,
                    Stars = x.zvaigzduciu_sk,
                    City = x.miestas,
                    Address = x.adresas,
                    Description = x.aprasymas
                });

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteHotel(int id)
        {
            try
            {


                if (id > 0)
                {
                    Viesbutis hotel = repository.Set<Viesbutis>().Find(id);
                    int count = repository.Set<Darbuotojas>().Where(x => x.fk_Viesbutisid == hotel.id).Count();
                    if (count > 0)
                    {
                        string error = "Viešbučio negalima ištrinti, nes jis turi darbuotojų";
                        ViewData["error"] = error;
                        throw new ITPProException(error);
                    }
                    List<Kambarys> rooms = repository.Set<Kambarys>().Where(x => x.fk_Viesbutisid == hotel.id).ToList();
                    foreach (var item in rooms)
                    {
                        List<Rezervacijos_kambarys> data = repository.Set<Rezervacijos_kambarys>().Where(x => x.fk_Kambarysid == item.id).ToList();
                        foreach (var item2 in data)
                        {
                            count = repository.Set<Rezervacija>().Where(x => x.id == item2.fk_Rezervacijaid && x.rezervacijos_pabaiga > DateTime.Now).Count();
                            if (count > 0)
                            {
                                string error = "Viešbučio negalima ištrinti, nes jis turi dar galiojančių rezervacijų susijusių su kambariais";
                                ViewData["error"] = error;
                                throw new ITPProException(error);
                            }
                        }
                    }
                    List<Papildoma_paslauga> services = repository.Set<Papildoma_paslauga>().Where(x => x.fk_Viesbutisid == hotel.id).ToList();
                    foreach (var item in services)
                    {
                        List<Rezervacijos_papildoma_paslauga> data = repository.Set<Rezervacijos_papildoma_paslauga>().Where(x => x.fk_Papildoma_paslaugaid == item.id).ToList();
                        foreach (var item2 in data)
                        {
                            count = repository.Set<Rezervacija>().Where(x => x.id == item2.fk_Papildoma_paslaugaid && x.rezervacijos_pabaiga > DateTime.Now).Count();
                            if (count > 0)
                            {
                                string error = "Viešbučio negalima ištrinti, nes jis turi dar galiojančių rezervacijų susijusių su papildomis paslaugomis";
                                ViewData["error"] = error;
                                throw new ITPProException(error);
                            }
                        }
                    }
                    count = repository.Set<Viesbutis>().Where(X => X.fk_savininkas == CurrentUser.UserId).Count();
                    if(count == 1)
                    {
                        List<Teises> cRights = repository.Set<Teises>().Where(x => x.viesbuciu_tinklas == hotel.viesbuciu_tinklas).ToList();
                        foreach (var item in cRights)
                            repository.Set<Teises>().Remove(item);
                    }
                    List<Teises> rigths = repository.Set<Teises>().Where(x => x.tipas.id == 2 && x.viesbutis == hotel.pavadinimas).ToList();
                    foreach(var item in rigths)
                    {
                        repository.Set<Teises>().Remove(item);
                    }             
                    foreach (var item in rooms)
                    {
                        repository.Set<Kambarys>().Remove(item);
                    }
                    foreach (var item in services)
                    {
                        repository.Set<Papildoma_paslauga>().Remove(item);
                    }
                    repository.Set<Viesbutis>().Remove(hotel);
                    repository.SaveChanges();
                }
                return RedirectToAction("HotelModelList");
            }
            catch (ITPProException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("HotelModelList");
        }


    }
}