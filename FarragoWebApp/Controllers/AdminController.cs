using FarragoWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarragoWebApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public int reservationPending()
        {
            int reservationNumber = 0;
            using (Farrago584Entities db = new Farrago584Entities())
            {
                List<FarragoWebApp.Models.TableReservation> reservations = db.TableReservation.Where(x => x.Confirmed == "N").ToList();
                List<FarragoWebApp.Models.LocalReservation> reservationsL = db.LocalReservation.Where(x => x.Confirmation == "N").ToList();
                foreach (var res in reservations)
                {
                    if (res.Status == "P")
                    {
                        reservationNumber++;
                    }
                }
                foreach (var res in reservationsL)
                {
                    if (res.Status == "P")
                    {
                        reservationNumber++;
                    }
                }
            }
            return reservationNumber;
        }

        public int reservationProx()
        {
            int reservationNumber = 0;
            using (Farrago584Entities db = new Farrago584Entities())
            {
                List<FarragoWebApp.Models.TableReservation> reservations = db.TableReservation.Where(x => x.Status == "P").ToList();
                List<FarragoWebApp.Models.LocalReservation> reservationsL = db.LocalReservation.Where(x => x.Status == "P").ToList();
                foreach (var res in reservations)
                {
                    reservationNumber++;
                }
                foreach (var res in reservationsL)
                {
                    reservationNumber++;
                }
            }
            return reservationNumber;
        }

        [HttpPost]
        public ActionResult Autherize(FarragoWebApp.Models.Admin userModel)
        {
            using (Farrago584Entities db = new Farrago584Entities())
            {
                var userDetails = db.Admin.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Error de autenticación";
                    return View("Index", userModel);
                }
                else
                {
                    Session["adminID"] = userDetails.id;
                    Session["userName"] = userDetails.Username;
                    Session["userFullName"] = userDetails.FullName;
                    Session["pendReservations"] = reservationPending();
                    Session["proxReservations"] = reservationProx();
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Dashboard()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    List<TableReservation> tableReservationsPend = db.TableReservation.Where(x => x.Status == "P").ToList();
                    List<LocalReservation> localReservationsPend = db.LocalReservation.Where(x => x.Status == "P").ToList();
                    Admin admin = new Admin();
                    admin.tableReservationsPend = tableReservationsPend;
                    admin.localReservationsPend = localReservationsPend;
                    return View(admin);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Reservaciones Pendientes: " + ex.Message);
                return View();
            }
        }

        public ActionResult UpdateAdmin()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    Admin admin = db.Admin.Find((int)Session["adminID"]);
                    admin.Password = "";
                    return View(admin);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error generar vista de Registro: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAdmin(Admin admin)
        {
            if (admin.Password != admin.PasswordConfirmation)
            {
                admin.LoginErrorMessage = "Las contraseñas no coinciden";
                return View(admin);
            }
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    Admin ad = db.Admin.Find(admin.id);
                    ad.Name = admin.Name;
                    ad.FirstName = admin.FirstName;
                    ad.LastName = admin.LastName;
                    ad.DNI = admin.DNI;
                    ad.Email = admin.Email;
                    ad.CellphoneNumber = admin.CellphoneNumber;
                    db.SaveChanges();
                    admin.LoginErrorMessage = "Algo sigue mal";
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
            catch (Exception ex)
            {
                admin.LoginErrorMessage = ex.Message;
                return View(admin);
            }
        }
    }
}