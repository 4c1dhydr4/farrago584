using FarragoWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarragoWebApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public int reservationNumbers(User userDetails)
        {
            int reservationNumber = 0;
            using (Farrago584Entities db = new Farrago584Entities())
            {
                List<FarragoWebApp.Models.TableReservation> reservations = db.TableReservation.Where(x => x.UserId == userDetails.id).ToList();
                List<FarragoWebApp.Models.LocalReservation> reservationsL = db.LocalReservation.Where(x => x.UserId == userDetails.id).ToList();
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

        [HttpPost]
        public ActionResult Autherize(FarragoWebApp.Models.User userModel)
        {
            using (Farrago584Entities db = new Farrago584Entities())
            {
                var userDetails = db.User.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Error de autenticación";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.id;
                    Session["userName"] = userDetails.Username;
                    Session["userFullName"] = userDetails.FullName;
                    Session["userReservations"] = reservationNumbers(userDetails);
                    Session["userFaults"] = userDetails.FaultsNumber;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}