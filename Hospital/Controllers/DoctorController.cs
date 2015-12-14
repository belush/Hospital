using Hospital.DAL;
using Hospital.DAL.Entity;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        public Repository r = new Repository();

        public ActionResult Index(string sortBy)
        {

            ViewBag.SortLastNameParameter = string.IsNullOrEmpty(sortBy) ? "LastName desc" : "";
            ViewBag.SortCategoryParameter = sortBy == "Category" ? "Category desc" : "Category";
            ViewBag.SortCountParameter = sortBy == "Count" ? "Count desc" : "Count";

            List<Doctor> doctors2 = r.GetDoctors();

            var doctors = r.GetDoctors().AsQueryable();
            List<Illness> ills2 = r.GetIllnesses();
            switch (sortBy)
            {
                case "LastName desc":
                    doctors = doctors.OrderByDescending(c => c.User.LastName);
                    break;
                case "LastName":
                    doctors = doctors.OrderBy(c => c.User.LastName);
                    break;
                case "Category desc":
                    doctors = doctors.OrderByDescending(c => c.Category);
                    break;
                case "Category":
                    doctors = doctors.OrderBy(c => c.Category);
                    break;
                case "Count desc":
                    doctors = doctors.OrderByDescending(c=> ills2.Where(i=>i.ReleaseDate==null).Where(i => i.Doctor.DoctorId == c.DoctorId).Count());
                    break;
                case "Count":
                    doctors = doctors.OrderBy(c => ills2.Where(i => i.ReleaseDate == null).Where(i => i.Doctor.DoctorId == c.DoctorId).Count());
                    break;
                    
            }

            List<Illness> ills = r.GetIllnesses();
            ViewBag.ills = ills; 

            return View(doctors.ToList());
        }

        public ActionResult App(int illnessId)
        {
            Illness illness = r.GetIllness(illnessId);
            ViewBag.ill = illness;
            List<Appointment> apps = r.GetApps(illnessId);

            return View(apps);
        }


        [Authorize]
        [Authorize(Roles = "Doctor")]
        public ActionResult MyPatients()
        {
            int userId = WebSecurity.CurrentUserId;
            List<Illness> illnesses = r.GetDoctorsIllnesses(userId);
            illnesses = illnesses.Where(x => x.ReleaseDate == null).ToList();
            return View(illnesses);
        }

        public ActionResult MakeApp(int appId)
        {
            r.MakeApp(appId);
            Illness ill =  r.GetIllnessByAppId(appId);
            return RedirectToAction("CreateApp2", new { illId = ill.IllnessId});
        }

        public ActionResult CreateApp(Illness ill)
        {
            ViewBag.ill = ill;
            return View();
        }

        [HttpPost]
        public ActionResult CreateApp(Appointment app, FormCollection collection)
        {
            try
            {
                r.AddAppointment(app);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Release(int id)
        {
            Illness illness = r.GetIllness(id);
            ViewBag.claim = illness.Claim;
            ViewBag.enterDate = illness.EnterDate;
            return View(illness);            
        }

        [HttpPost]
        public ActionResult Release(Illness ill, FormCollection collection)
        {
            bool isValid = (ill.FinalDiagnosis != null)
                && (ill.ReleaseDate != null)
                && (ill.ReleaseDate >= ill.EnterDate)
                && (ill.Diagnosis != null);

            if (isValid)
            {
                try
                {
                    r.EditIllness(ill);

                    return RedirectToAction("MyPatients");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult CreateApp2(int illId)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Процедура", Value = "Процедура", Selected = true });
            items.Add(new SelectListItem { Text = "Лекарство", Value = "Лекарства"});
            items.Add(new SelectListItem { Text = "Операция", Value = "Операция" });

            ViewData["appTypes"] = items;

            ViewBag.ill = r.GetIllness(illId);
            List<Appointment> apps = r.GetApps(illId);
            ViewBag.apps = apps;
            return View();
        }

        [HttpPost]
        public ActionResult CreateApp2(Appointment app, FormCollection collection, int illId, string appTypes)
        {
            if (app.AppointmentName != null)
            {
                try
                {
                    app.Illness = r.GetIllness(illId);
                    r.AddAppointment(app);

                    return RedirectToAction("CreateApp2", "Doctor", new { illId = illId });
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("CreateApp2", "Doctor", new { illId = illId });
            }
        }

        public ActionResult Edit(int id)
        {
            Illness illness = r.GetIllness(id);
            ViewBag.claim = illness.Claim;
            ViewBag.enterDate = illness.EnterDate;
            return View(illness);
        }

        [HttpPost]
        public ActionResult Edit(Illness illness, FormCollection form)
        {
            try
            {
                r.EditIllness(illness);

                return RedirectToAction("MyPatients");
            }
            catch
            {
                return View();
            }
        }
    }
}
