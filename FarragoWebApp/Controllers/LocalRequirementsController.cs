using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarragoWebApp.Models;

namespace FarragoWebApp.Controllers
{
    public class LocalRequirementsController : Controller
    {
        // GET: LocalRequirements
        public ActionResult Index()
        {
            return View();
        }
    }
}