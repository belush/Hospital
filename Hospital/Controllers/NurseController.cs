using Hospital.DAL;
using Hospital.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class NurseController : Controller
    {
        public Repository r = new Repository();

        public ActionResult Index()
        {
            List<Nurse> nurses = r.GetNurses();
            return View(nurses);
        }

        public ActionResult Apps()
        {
            List<Appointment> apps = r.GetApps();
            return View(apps);
        }

        public ActionResult MakeApp(int appId)
        {
            r.MakeApp(appId);
            return RedirectToAction("Apps");
        }
    }
}
