using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarragoWebApp.Models;

namespace FarragoWebApp.Controllers
{
    public class LocalReservationController : Controller
    {
        // GET: LocalReservation
        public ActionResult Index()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    int userId = (int)Session["userID"];
                    return View(db.LocalReservation.Where(x => x.UserId == userId).ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar las Reservaciones de Local" + ex.Message);
                return View();
            }
        }

        public ActionResult Admin()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    return View(db.LocalReservation.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar las Reservaciones de Local" + ex.Message);
                return View();
            }
        }

        public ActionResult Create()
        {
            LocalReservation reservation = new LocalReservation();
            reservation.Date = DateTime.Now;
            reservation.PersonsNumber = 150;
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocalReservation reservation)
        {
            //Modelo con errores
            //if (!ModelState.IsValid) { return View(); }
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    string reservationDate = reservation.Date.ToShortDateString();
                    DateTime dateReservation = DateTime.Parse(reservationDate);
                    reservation.UserId = (int)Session["userID"];
                    reservation.Date = dateReservation;
                    reservation.Confirmation = "N";
                    reservation.AdminId = db.Admin.First().id;
                    reservation.Status = "P";
                    reservation.FinalPrice = 0.00;
                    db.LocalReservation.Add(reservation);
                    db.SaveChanges();
                    Session["userReservations"] = (int)Session["userReservations"] + 1;
                    FarragoWebApp.Controllers.UserController.UserCounters(reservation.UserId, "R", true);
                    return RedirectToAction("RequirementList", "Requirements", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar Reservación: " + ex.Message);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(id);
                    List<LocalRequirements> localRequirements = db.LocalRequirements.Where(x => x.LocalReservation == reservation.id).ToList();
                    List<Requirements> requirements = new List<Requirements>();
                    foreach (var req in localRequirements)
                    {
                        requirements.Add(db.Requirements.Find(req.Requirement));
                    }
                    reservation.RequirementsList = requirements;
                    return View(reservation);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Reservación: " + ex.Message);
                throw;
            }
        }

        public ActionResult DetailsAdmin(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(id);
                    List<LocalRequirements> localRequirements = db.LocalRequirements.Where(x => x.LocalReservation == reservation.id).ToList();
                    List<Requirements> requirements = new List<Requirements>();
                    foreach (var req in localRequirements)
                    {
                        requirements.Add(db.Requirements.Find(req.Requirement));
                    }
                    reservation.RequirementsList = requirements;
                    return View(reservation);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Reservación: " + ex.Message);
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(id);
                    List<LocalRequirements> localRequirements = db.LocalRequirements.Where(x => x.LocalReservation == reservation.id).ToList();
                    List<Requirements> requirements = new List<Requirements>();
                    foreach (var req in localRequirements)
                    {
                        requirements.Add(db.Requirements.Find(req.Requirement));
                    }
                    reservation.RequirementsList = requirements;
                    return View(reservation);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Editar Alumno " + ex.Message);
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocalReservation localReservation)
        {
            //if (!ModelState.IsValid) { return View(); }
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(localReservation.id);
                    reservation.Date = localReservation.Date;
                    reservation.PersonsNumber = localReservation.PersonsNumber;
                    reservation.UserObservation = localReservation.UserObservation;
                    db.SaveChanges();
                    return RedirectToAction("Index","LocalReservation");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Editar Reservación: " + ex.Message);
                return View(localReservation);
            }
        }

        public ActionResult Requirements(int id)
        {
            using (Farrago584Entities db = new Farrago584Entities())
            {
                List<LocalRequirements> localRequirements = db.LocalRequirements.Where(x => x.LocalReservation == id).ToList();
                List<Requirements> requirements = db.Requirements.ToList(); ;
                foreach (var req in requirements)
                {
                    foreach (var locReq in localRequirements)
                    {
                        if(locReq.Requirement == req.id)
                        {
                            req.isChecked = true;
                        }
                    }
                }
                foreach (var locReq in localRequirements)
                {
                    db.LocalRequirements.Remove(locReq);
                    db.SaveChanges();
                }
                RequirementList listRequirements = new RequirementList();
                listRequirements.RequirementsList = requirements;
                return View(listRequirements);
            }
        }

        [HttpPost]
        public ActionResult RequirementList(RequirementList requirements)
        {
            //Modelo con errores
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    int userId = (int)Session["userID"];
                    User user = db.User.Find(userId);
                    LocalReservation actualReservation = db.LocalReservation.Where(x => x.UserId == userId && x.Status == "P").ToList().LastOrDefault();
                    double price = 0;
                    foreach (var req in requirements.RequirementsList)
                    {
                        if (req.isChecked)
                        {
                            Requirements actualReq = db.Requirements.Find(req.id);
                            LocalRequirements atualLocalRequirement = new LocalRequirements();
                            price += req.Cost;
                            atualLocalRequirement.Requirement = actualReq.id;
                            atualLocalRequirement.LocalReservation = actualReservation.id;
                            db.LocalRequirements.Add(atualLocalRequirement);
                            db.SaveChanges();
                        }
                    }
                    actualReservation.FinalPrice = price;
                    db.SaveChanges();
                    return RedirectToAction("Index", "LocalReservation", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar Lista de Requerimientos: " + ex.Message);
                return View(requirements);
            }
        }

        public ActionResult Cancel(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(id);
                    reservation.Status = "C";
                    db.SaveChanges();
                    return RedirectToAction("Index", "LocalReservation", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Cancelar Reserva: " + ex.Message);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Confirm(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(id);
                    return View(reservation);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(LocalReservation res)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(res.id);
                    reservation.Confirmation = "C";
                    reservation.AdminObservation = res.AdminObservation;
                    db.SaveChanges();
                    return RedirectToAction("Dashboard", "Admin", new { area = "" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Asisted(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(id);
                    return View(reservation);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Asisted(LocalReservation res)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(res.id);
                    reservation.Status = "A";
                    reservation.AdminObservation = res.AdminObservation;
                    db.SaveChanges();
                    FarragoWebApp.Controllers.UserController.UserCounters(reservation.UserId, "A", true);
                    return RedirectToAction("Dashboard", "Admin", new { area = "" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Fault(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(id);
                    return View(reservation);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fault(LocalReservation res)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    LocalReservation reservation = db.LocalReservation.Find(res.id);
                    reservation.Status = "F";
                    reservation.AdminObservation = res.AdminObservation;
                    db.SaveChanges();
                    FarragoWebApp.Controllers.UserController.UserCounters(reservation.UserId, "F", true);
                    return RedirectToAction("Dashboard", "Admin", new { area = "" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}