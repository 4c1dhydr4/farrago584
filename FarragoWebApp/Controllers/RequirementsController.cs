using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarragoWebApp.Models;

namespace FarragoWebApp.Controllers
{
    public class RequirementsController : Controller
    {
        // GET: Requirements
        public ActionResult Index()
        {
            try
            {
                using (Farrago584Entities db = new Farrago584Entities())
                {
                    return View(db.Requirements.ToList());
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al mostrar Requerimientos Disponibles" + ex.Message);
                return View();
            }
        }
        public ActionResult RequirementList()
        {
            using (Farrago584Entities db = new Farrago584Entities())
            {
                List<Requirements> requirements = db.Requirements.ToList();
                RequirementList listRequirements = new RequirementList();
                foreach(var req in requirements)
                {
                    req.isChecked = true;
                }
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
                    foreach(var req in requirements.RequirementsList)
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
    }
}