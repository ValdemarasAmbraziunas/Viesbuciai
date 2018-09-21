using ITPPro.Data;
using ITPPro.Exceptions;
using ITPPro.Models;
using ITPPro.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITPPro.Models.Enums;
using ITPPro.Exceptions;

namespace ITPPro.Controllers
{
    public class Viesbucio_administravimoController : BaseController
    {

        public Viesbucio_administravimoController(BaseRepository repository) : base(repository) { }
        // GET: Viesbucio_administravimo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Viesbuciu_langas(int page = 1, int items = 10)
        {
            if (page < 1)
                page = 1;

            ViewData["page"] = page;
            ViewData["items"] = items;
            Darbuotojas emp = repository.Set<Darbuotojas>().Find(CurrentUser.UserId);
            Viesbutis hotel = repository.Set<Viesbutis>().Find(emp.fk_Viesbutisid);
            HotelsViewModel model = new HotelsViewModel();
            List<HotelsViewModel> hotels = new List<HotelsViewModel>();
            List<Teises> rights = repository.Set<Teises>().Where(x => x.fk_Darbuotojasdarbuojo_kodas == CurrentUser.UserId).ToList();
            foreach (var item in repository.Set<Viesbutis>())
            {
                if (item.viesbuciu_tinklas == hotel.viesbuciu_tinklas)
                {
                    hotels.Add(new HotelsViewModel()
                    {
                        id = item.id,
                        Title = item.pavadinimas,
                        HotelsNet = item.viesbuciu_tinklas,
                        Stars = item.zvaigzduciu_sk,
                        City = item.miestas,
                        Address = item.adresas,
                        Description = item.aprasymas,
                        Rights = rights.Where(x => x.viesbutis == item.pavadinimas).Select(x => x.teisiu_statusas).FirstOrDefault()
                    });
                }
            }
            hotels.OrderBy(x => x.id);
            model.hotels = hotels.ToPagedList(1, 10);

            return View(model);
        }

        public ActionResult Viesbucio_informacijos_langas(int id = 0)
        {
            try
            {
                Viesbutis hotel = repository.Set<Viesbutis>().Find(id);
                List<RoomViewModel> rooms = new List<RoomViewModel>();
                List<Kambarys> items = repository.Set<Kambarys>().ToList();
                foreach (var item in items)
                {
                    if (item.fk_Viesbutisid == id)
                    {
                        rooms.Add(new RoomViewModel()
                        {
                            id = item.id,
                            Capacity = item.vietu_sk,
                            Number = item.numeris,
                            Price = item.kaina,
                            Description = item.aprasymas,
                            Type = item.tipas.name.First().ToString().ToUpper() + item.tipas.name.Substring(1)
                        });
                    }
                }
                rooms.OrderBy(x => x.id);
                List<ServiceViewModel> services = new List<ServiceViewModel>();
                List<Papildoma_paslauga> items2 = repository.Set<Papildoma_paslauga>().ToList();
                foreach (var item in items2)
                {
                    if (item.fk_Viesbutisid == id)
                    {
                        services.Add(new ServiceViewModel()
                        {
                            id = item.id,
                            Description = item.aprasymas,
                            Price = item.kaina
                        });
                    }
                }
                services.OrderBy(x => x.id);
                var model = new HotelsViewModel();
                if (hotel != null)
                {
                    model.id = hotel.id;
                    model.Title = hotel.pavadinimas;
                    model.HotelsNet = hotel.viesbuciu_tinklas;
                    model.Stars = hotel.zvaigzduciu_sk;
                    model.City = hotel.miestas;
                    model.Address = hotel.adresas;
                    model.Description = hotel.aprasymas;
                }
                model.Rooms = rooms;
                model.Services = services;
                model.RoomsTitle = new RoomViewModel();
                model.ServicesTitle = new ServiceViewModel();
                return View(model);
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Sistemos_klientu_langas");
            }
        }

        [HttpGet]
        public ActionResult Kambario_kurimo_langas(int hotelid)
        {
            if (hotelid > 0)
            {
                ViewData["code"] = hotelid;
            }
            RoomViewModel model = new RoomViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterRoom(RoomViewModel model, int hotelid)
        {
            if (ModelState.IsValid)
            {
                int cnt = repository.Set<Kambarys>().Where(x => x.fk_Viesbutisid == hotelid && x.numeris == model.Number).Count();
                if (cnt == 0)
                {
                    IEnumerable<Kambario_Tipai_Enum> kambario_tipas = repository.Set<Kambario_Tipai_Enum>();
                    int value = Convert.ToInt16(model.Type);
                    var room = repository.Set<Kambarys>().Add(new Kambarys
                    {
                        vietu_sk = model.Capacity,
                        numeris = model.Number,
                        kaina = model.Price,
                        fk_Viesbutisid = hotelid,
                        tipas = kambario_tipas.Where(x => x.id == value).First(),
                        aprasymas = model.Description

                    });
                    repository.SaveChanges();
                }
                return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
            }
            return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
        }

        [HttpPost]
        public ActionResult DeleteRoom(int id, int hotelid)
        {
            int count = repository.Set<Rezervacijos_kambarys>().Where(x => x.fk_Kambarysid == id).Count();
            if (count == 0)
            {
                Kambarys room = repository.Set<Kambarys>().Find(id);
                repository.Set<Kambarys>().Remove(room);
                repository.SaveChanges();
            }
            return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
        }

        [HttpPost]
        public ActionResult Kambario_informacijos_keitimo_langas(int id, int hotelid)
        {
            try
            {
                if (hotelid > 0)
                {
                    ViewData["code"] = hotelid;
                }
                Kambarys room = repository.Set<Kambarys>().Find(id);
                var model = new RoomViewModel();
                if (room != null)
                {
                    model.id = room.id;
                    model.Number = room.numeris;
                    model.Capacity = room.vietu_sk;
                    model.Price = room.kaina;
                    model.Description = room.aprasymas;
                    model.Type = room.tipas.name.First().ToString().ToUpper() + room.tipas.name.Substring(1).TrimEnd();
                }

                return View(model);
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
            }
        }

        [HttpPost]
        public ActionResult EditRoom(RoomViewModel model, int hotelid)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Kambario_Tipai_Enum> kambario_tipas = repository.Set<Kambario_Tipai_Enum>();
                int value = Convert.ToInt16(model.Type);
                Kambarys room = repository.Set<Kambarys>().Find(model.id);
                room.numeris = model.Number;
                room.vietu_sk = model.Capacity;
                room.kaina = model.Price;
                room.aprasymas = model.Description;
                room.tipas = kambario_tipas.Where(x => x.id == value).First();
                repository.SaveChanges();
            }
            return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
        }

        [HttpGet]
        public ActionResult Paslaugos_kurimo_langas(int hotelid)
        {
            if (hotelid > 0)
            {
                ViewData["code"] = hotelid;
            }
            ServiceViewModel model = new ServiceViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterService(ServiceViewModel model, int hotelid)
        {
            if (ModelState.IsValid)
            {
                int cnt = repository.Set<Papildoma_paslauga>().Where(x => x.fk_Viesbutisid == hotelid && x.aprasymas == model.Description).Count();
                if (cnt == 0)
                {
                    var service = repository.Set<Papildoma_paslauga>().Add(new Papildoma_paslauga
                    {
                        kaina = model.Price,
                        fk_Viesbutisid = hotelid,
                        aprasymas = model.Description

                    });
                    repository.SaveChanges();
                }
                return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
            }
            return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
        }

        [HttpPost]
        public ActionResult DeleteService(int id, int hotelid)
        {
            int count = repository.Set<Rezervacijos_papildoma_paslauga>().Where(x => x.fk_Papildoma_paslaugaid == id).Count();
            if (count == 0)
            {
                Papildoma_paslauga service = repository.Set<Papildoma_paslauga>().Find(id);
                repository.Set<Papildoma_paslauga>().Remove(service);
                repository.SaveChanges();
            }
            return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
        }

        [HttpPost]
        public ActionResult Paslaugos_informacijos_keitimo_langas(int id, int hotelid)
        {
            try
            {
                if (hotelid > 0)
                {
                    ViewData["code"] = hotelid;
                }
                Papildoma_paslauga room = repository.Set<Papildoma_paslauga>().Find(id);
                var model = new ServiceViewModel();
                if (room != null)
                {
                    model.id = room.id;
                    model.Price = room.kaina;
                    model.Description = room.aprasymas;
                }

                return View(model);
            }
            catch (ITPProException ex)
            {
                return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
            }
        }

        [HttpPost]
        public ActionResult EditService(ServiceViewModel model, int hotelid)
        {
            if (ModelState.IsValid)
            {
                Papildoma_paslauga service = repository.Set<Papildoma_paslauga>().Find(model.id);
                service.kaina = model.Price;
                service.aprasymas = model.Description;
                repository.SaveChanges();
            }
            return RedirectToAction("Viesbucio_informacijos_langas", new { id = hotelid });
        }

        [HttpGet]
        public ActionResult Kambariu_uzimtumo_ataskaitos_langas(int hotelid)
        {

            if (hotelid > 0)
            {
                ViewData["code"] = hotelid;
            }
            RoomsBusynessViewModel model = new RoomsBusynessViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateReport(RoomsBusynessViewModel model, int hotelid)
        {
            TimeSpan xd = model.EndTime - model.StartTime;
            List<Kambarys> rooms = repository.Set<Kambarys>().Where(x => x.fk_Viesbutisid == hotelid).ToList();
            Viesbutis hotel = repository.Set<Viesbutis>().Find(hotelid);
            RoomsReportResultsViewModel model2 = new RoomsReportResultsViewModel();
            model2.Title = hotel.pavadinimas;
            model2.StartTime = model.StartTime;
            model2.EndTime = model.EndTime;
            List<RoomResult> results = new List<RoomResult>();
            foreach(var item in rooms)
            {
                RoomResult result = new RoomResult();
                result.Number = item.numeris;
                List<Rezervacijos_kambarys> data = repository.Set<Rezervacijos_kambarys>().Where(x => x.fk_Kambarysid == item.id).ToList();
                double count = 0;
                foreach(var item2 in data)
                {
                    Rezervacija data2 = repository.Set<Rezervacija>().Find(item2.fk_Rezervacijaid);
                    if(data2.rezervacijos_pradzia >= model.EndTime)
                    {
                        count += 0;
                    }
                    if(data2.rezervacijos_pradzia >= model.StartTime && data2.rezervacijos_pabaiga <= model.EndTime)
                    {
                        count += data2.rezervacijos_pabaiga.Subtract(data2.rezervacijos_pradzia).TotalDays;
                    }
                    if(data2.rezervacijos_pradzia >= model.StartTime && data2.rezervacijos_pradzia < model.EndTime && data2.rezervacijos_pabaiga > model.EndTime)
                    {
                        count += model.EndTime.Subtract(data2.rezervacijos_pradzia).TotalDays;
                    }
                }

                double lol = xd.TotalDays;
                result.Busyness = count * 100 / lol;
                results.Add(result);
            }
            model2.results = results;
            return View(model2);
        }
    }
}