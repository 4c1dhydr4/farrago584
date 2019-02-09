using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarragoWebApp.Models;

namespace FarragoWebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    return View(db.User.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Usuarios " + ex.Message);
                return View();
            }
        }

        public ActionResult Details()
        {
            int userId = (int)Session["userID"];
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    User user = db.User.Find(userId);
                   return View(user);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Usuarios " + ex.Message);
                return View();
            }
        }

        public ActionResult UserProfile(int id)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    User user = db.User.Find(id);
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Usuarios " + ex.Message);
                return View();
            }
        }

        public static string UserName(int UserId)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    User user = db.User.Find(UserId);
                    return user.FullName;
                }
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        public static void UserCounters(int userId, string action, bool flag)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    User user = db.User.Find(userId);
                    if (!flag)
                    {
                        if (action == "R")
                        {
                            user.ReservationsNumber--;
                        }
                        if (action == "A")
                        {
                            user.AssistedNumber--;
                        }
                        if (action == "F")
                        {
                            user.FaultsNumber--;
                        }

                    }
                    else
                    {
                        if (action == "R")
                        {
                            user.ReservationsNumber++;
                        }
                        if (action == "A")
                        {
                            user.AssistedNumber++;
                        }
                        if (action == "F")
                        {
                            user.FaultsNumber++;
                        }

                    }
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Register()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    User user = new User();
                    user.BornDate = DateTime.Now;
                    return View(user);
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
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                if (user.Password != user.PasswordConfirmation)
                {
                    user.LoginErrorMessage = "La contraseña no coincide con la confirmación";
                    return View(user);
                }
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    foreach (var client in db.User.ToList())
                    {
                        if (user.Username == client.Username)
                        {
                            user.LoginErrorMessage = "El el usuario ya se encuentra registrado. Ingrese un usuario diferente";
                            return View(user);
                        }
                        if (user.CellphoneNumber == client.CellphoneNumber)
                        {
                            user.LoginErrorMessage = "El número telefónico ingresado ya se encuentra registrado";
                            return View(user);
                        }
                        if (user.DNI == client.DNI)
                        {
                            user.LoginErrorMessage = "El DNI ya se encuentra registrado";
                            return View(user);
                        }
                        if (user.Email == client.Email)
                        {
                            user.LoginErrorMessage = "El Correo Electrónico ingresado ya se encuentra registrado";
                            return View(user);
                        }
                    }
                    user.ReservationsNumber = 0;
                    user.AssistedNumber = 0;
                    user.FaultsNumber = 0;
                    string born = user.BornDate.ToShortDateString();
                    user.BornDate = DateTime.Parse(born);
                    db.User.Add(user);
                    db.SaveChanges();
                    return View("Confirmation", user);                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error en el Registro: " + ex.Message);
                return View();
            }
        }

        public ActionResult Confirmation(User newUser)
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    User user = db.User.Find(newUser.id);
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Usuario " + ex.Message);
                return View();
            }
        }
    }
}