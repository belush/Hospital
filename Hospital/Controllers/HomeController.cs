using Hospital.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        public Repository r = new Repository();

        public ActionResult Index()
        {
            List<Doctor> docs = r.GetDoctors();
            
            ViewBag.IsAdmin = User.IsInRole("Admin");
            ViewBag.IsDoctor = User.IsInRole("Doctor");
            ViewBag.IsNurse = User.IsInRole("Nurse");
            ViewBag.IsPatient = User.IsInRole("Patient");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Visits()
        {
            List<Illness> ills = r.GetIllnesses();

            List<Doctor> docs = r.GetWorkDoctors();

            List<Patient> pats = r.GetPatients();

            List<Visit> vis = new List<Visit>();

            List<DateTime> dates = ills.Select(x => (DateTime)x.EnterDate).Distinct().ToList();
            
            foreach (var date in dates)
            {
                Visit visit = new Visit();
                
                visit.Date = date;
                foreach (Illness ill in ills)
                {
                    visit.Ills.Add(ill);
                }
                vis.Add(visit);
            }

            ViewBag.dates = dates;
            ViewBag.docs = docs;
            ViewBag.pats = pats;
            return View(vis);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult CheckName(string name)
        {
            bool ifNameUnique = r.IsNameUnique(name);

            return Json(ifNameUnique, JsonRequestBehavior.AllowGet);
        }
    }
}
