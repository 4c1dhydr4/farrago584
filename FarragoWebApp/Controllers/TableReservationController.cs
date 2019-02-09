using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarragoWebApp.Models;

namespace FarragoWebApp.Controllers
{
    public class TableReservationController : Controller
    {
        // GET: TableReservation
        public ActionResult Index()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    int userId = (int)Session["userID"];
                    return View(db.TableReservation.Where(x => x.UserId == userId).ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar las Reservaciones: " + ex.Message);
                return View();
            }
        }

        public ActionResult Admin()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    return View(db.TableReservation.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar las Reservaciones: " + ex.Message);
                return View();
            }
        }

        public ActionResult Create()
        {
            TableReservation reservation = new TableReservation();
            reservation.DateTime = DateTime.Now;
            reservation.Hour = 13;
            reservation.Minute = 50;
            reservation.PersonsNumber = 2;
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TableReservation reservation)
        {
            //Modelo con errores
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    int hour = reservation.Hour;
                    int minute = reservation.Minute;
                    string reservationDate = reservation.DateTime.ToShortDateString() + " " + hour.ToString() + ":" + minute.ToString() + ":" + "00";
                    DateTime dateReservation = DateTime.Parse(reservationDate);
                    reservation.DateTime = dateReservation;
                    reservation.UserId = (int)Session["userID"];
                    reservation.Confirmed = "N";
                    reservation.AdminId = db.Admin.First().id;
                    reservation.Status = "P";
                    db.TableReservation.Add(reservation);
                    db.SaveChanges();
                    Session["userReservations"] = (int)Session["userReservations"] + 1;
                    FarragoWebApp.Controllers.UserController.UserCounters(reservation.UserId, "R", true);
                    return RedirectToAction("Index", "TableReservation", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar Reservación: " + ex.Message);
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(id);
                    reservation.Hour = reservation.DateTime.Hour;
                    reservation.Minute = reservation.DateTime.Minute;
                    return View(reservation);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Obtener Reservación en el Sistema: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TableReservation reservation)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservationUpdate = db.TableReservation.Find(reservation.id);
                    int hour = reservation.Hour;
                    int minute = reservation.Minute;
                    string reservationDate = reservation.DateTime.ToShortDateString() + " " + hour.ToString() + ":" + minute.ToString() + ":" + "00";
                    DateTime dateReservation = DateTime.Parse(reservationDate);
                    reservationUpdate.DateTime = dateReservation;
                    reservationUpdate.UserId = (int)Session["userID"];
                    reservationUpdate.Confirmed = "N";
                    reservationUpdate.AdminId = db.Admin.First().id;
                    reservationUpdate.PersonsNumber = reservation.PersonsNumber;
                    reservationUpdate.UserObservations = reservation.UserObservations;
                    reservationUpdate.Status = "P";
                    db.SaveChanges();
                    return RedirectToAction("Index", "TableReservation", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar Reservación: " + ex.Message);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(id);
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
        public ActionResult Delete(TableReservation reservation)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservationDelete = db.TableReservation.Find(reservation.id);
                    db.TableReservation.Remove(reservationDelete);
                    db.SaveChanges();
                    Session["userReservations"] = (int)Session["userReservations"] - 1;
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Cancel(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(id);
                    reservation.Status = "C";
                    db.SaveChanges();
                    return RedirectToAction("Index", "TableReservation", new { area = "" });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al Cancelar Reserva: " + ex.Message);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(id);
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
                    TableReservation reservation = db.TableReservation.Find(id);
                    return View(reservation);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Reservación: " + ex.Message);
                throw;
            }
        }

        public static string AdminContact(int AdminId)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    Admin admin = db.Admin.Find(AdminId);
                    return admin.Name + " " + admin.FirstName + " - Cel: " + admin.CellphoneNumber;
                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }


        [HttpGet]
        public ActionResult Confirm(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(id);
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
        public ActionResult Confirm(TableReservation res)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(res.id);
                    reservation.Confirmed = "C";
                    reservation.AdminObservations = res.AdminObservations;
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
                    TableReservation reservation = db.TableReservation.Find(id);
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
        public ActionResult Asisted(TableReservation res)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(res.id);
                    reservation.Status = "A";
                    reservation.AdminObservations = res.AdminObservations;
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
                    TableReservation reservation = db.TableReservation.Find(id);
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
        public ActionResult Fault(TableReservation res)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    TableReservation reservation = db.TableReservation.Find(res.id);
                    reservation.Status = "F";
                    reservation.AdminObservations = res.AdminObservations;
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

