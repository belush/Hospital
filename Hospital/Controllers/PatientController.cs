using Hospital.DAL;
using Hospital.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Hospital.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PatientController : Controller
    {
        Repository r = new Repository();

        public ActionResult Index(string sortBy)
        {
            ViewBag.SortLastNameParameter = string.IsNullOrEmpty(sortBy) ? "LastName desc" : "";
            ViewBag.SortBirthDateParameter = sortBy == "BirthDate" ? "BirthDate desc" : "BirthDate";

            List<Patient> patients2 = new List<Patient>();

            patients2 = r.GetPatients();
            var patients = r.GetPatients().AsQueryable();

            switch (sortBy)
            {
                case "LastName desc":
                    patients = patients.OrderByDescending(c => c.User.LastName);
                    break;
                case "LastName":
                    patients = patients.OrderBy(c => c.User.LastName);
                    break;
                case "BirthDate desc":
                    patients = patients.OrderByDescending(c => c.User.BirthDate);
                    break;
                case "BirthDate":
                    patients = patients.OrderBy(c => c.User.BirthDate);
                    break;
            }
            return View(patients.ToList());
        }

        public ActionResult PatientOnTreatment()
        {
            return View();
        }

        public ActionResult Create(int id)
        {

            try
            {
                bool isPatOnTreatment = r.IsPatientOnTreatment(id);
                if (isPatOnTreatment)
                {
                    return RedirectToAction("PatientOnTreatment");
                }
                else
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    List<Doctor> docs = r.GetDoctors();
                    foreach (var item in docs)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = (item.User.FirstName + " " + item.User.LastName + " | " + item.Category),
                            Value = item.DoctorId.ToString()
                        });
                    }

                    ViewData["doctor"] = items;

                    return View();
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(Illness illness, int id, int doctor)
        {
            bool isDateIsValid = ((illness.Claim != null) && (illness.EnterDate != null));
            if (isDateIsValid)
            {
                try
                {
                    Doctor doctorEntity = r.GetDoctorById(doctor);
                    Patient patient = r.GetPatient(id);
                    illness.Patient = patient;
                    illness.Doctor = doctorEntity;

                    r.AddIllness(illness);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Card()
        {
            int userId = WebSecurity.CurrentUserId;
            List<Illness> ills = r.GetPatientIlls(userId);

            return View(ills);
        }

        public ActionResult Edit(int id)
        {
            User user = r.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user, FormCollection collection)
        {
            try
            {
                r.EditUser(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
