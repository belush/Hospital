using Hospital.DAL;
using Hospital.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class IllnessController : Controller
    {
        public Repository r = new Repository();

        public ActionResult Index()
        {

            return View(r.GetIllnesses());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Illness illness, FormCollection collection)
        {
            try
            {
                int idd = int.Parse((Request.Params["Idd"]));
                int idp = int.Parse((Request.Params["Idp"]));
                Doctor d = r.GetDoctorById(idd);
                Patient p = r.GetPatient(idp);
                illness.Doctor = d;

                illness.Patient = p;
                r.AddIllness(illness);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
