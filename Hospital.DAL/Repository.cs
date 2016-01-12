using Hospital.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.WebPages.Html;

namespace Hospital.DAL
{
    public class Repository
    {
        UserContext p = new UserContext();

        //DOCTOR LOGIC

        public int GetDoctorId(string doctorName)
        {
            Doctor doctor = new Doctor();
            doctor = p.Doctors.FirstOrDefault(x => x.User.UserName == doctorName);
            int doctorId = doctor.DoctorId;
            return doctorId;
        }

        public Doctor GetDoctorByName(string doctorName)
        {
            Doctor doctor = new Doctor();
            doctor = p.Doctors.FirstOrDefault(x => x.User.UserName == doctorName);
            return doctor;
        }

        public Doctor GetDoctorById(int id)
        {
            Doctor doctor = new Doctor();
            doctor = p.Doctors.FirstOrDefault(x => x.DoctorId == id);
            return doctor;
        }

        public Doctor GetDoctorByUserId(int id)
        {
            Doctor doctor = new Doctor();
            doctor = p.Doctors.FirstOrDefault(x => x.User.UserId == id);
            return doctor;
        }

        public void AddDoctor(Doctor doctor)
        {
            p.Doctors.Add(doctor);
            p.SaveChanges();
        }

        public int GetDoctorCount()
        {
            int count = p.Doctors.Count();
            return count;
        }

        public List<Doctor> GetDoctors()
        {
            return p.Doctors.ToList();
        }

        public List<Doctor> GetWorkDoctors()
        {
            List<Illness> ills = GetIllnesses();
            List<Doctor> docs = new List<Doctor>();
            foreach (var item in ills)
            {
                docs.Add(item.Doctor);
            }
            docs = docs.Distinct().ToList();

            return docs.ToList();
        }

        //PATIENT LOGIC

        public List<Patient> GetPatients()
        {
            return p.Patients.ToList();
        }

        public Patient GetPatient(int id)
        {
            Patient patient = p.Patients.Single(x => x.PatientId == id);
            return patient;
        }

        public void EditPatients(Patient patient)
        {
            p.Entry(patient).State = EntityState.Modified;
            p.SaveChanges();
        }

        public bool IsPatientOnTreatment(int id)
        {
            bool isOnTreat = false;
            List<Illness> ills = GetIllnesses();
            foreach (var item in ills)
            {

                if ((item.Patient.PatientId == id) && (item.FinalDiagnosis == null))
                {
                    isOnTreat = true;
                }
            }
            return isOnTreat;
        }

        public void AddPatient(Patient patient)
        {
            p.Patients.Add(patient);
            p.SaveChanges();
        }

        public void EditPatient(Patient patient)
        {
            p.Entry(patient).State = EntityState.Modified;
            p.SaveChanges();
        }


        //Appointments LOGIC

        public List<Appointment> GetApps(int id)
        {
            List<Appointment> apps = p.Appointments.Where(x => x.Illness.IllnessId == id).ToList();

            return apps;
        }
        public List<Appointment> GetApps()
        {
            List<Appointment> apps = p.Appointments.ToList();
            return apps;
        }

        public void MakeApps(int illId)
        {
            List<Appointment> apps = GetApps(illId);
            foreach (var item in apps)
            {
                item.IsAppointmentDone = true;
            }

            p.SaveChanges();
        }

        public void MakeApp(int appId)
        {
            Appointment app = p.Appointments.FirstOrDefault(x => x.AppointmentId == appId);
            app.IsAppointmentDone = true;

            p.SaveChanges();
        }

        public void AddAppointment(Appointment app)
        {
            p.Appointments.Add(app);
            p.SaveChanges();
        }

        public void EditAppointment(Appointment app, string drug, string operation)
        {
            //p.Appointments.Add(app);
            p.Entry(app).State = EntityState.Modified;
            p.SaveChanges();

            if (drug != "" && drug != null)
            {
                Appointment a = new Appointment();
                a.Illness = GetIllnessByAppId(app.AppointmentId);
                a.AppointmentType = "Лекарства";
                a.AppointmentName = drug;
                p.Appointments.Add(a);

            }
            if (operation != "" && operation != null)
            {
                Appointment a2 = new Appointment();
                a2.Illness = app.Illness;
                a2.AppointmentType = "Операции";
                a2.AppointmentName = operation;
                p.Appointments.Add(a2);
            }

            p.SaveChanges();
            int m = 8;
        }

        //TODO: choose one function
        public void EditAppointment2(Appointment app, string drug, string operation)
        {
            Illness illness = GetIllnessByAppId(app.AppointmentId);
            if (drug != "" && drug != null)
            {
                Appointment a = new Appointment();
                a.Illness = illness;
                a.AppointmentType = "Лекарства";
                a.AppointmentName = drug;
                p.Appointments.Add(a);

            }
            if (operation != "" && operation != null)
            {
                Appointment a2 = new Appointment();
                a2.Illness = app.Illness;
                a2.AppointmentType = "Операции";
                a2.AppointmentName = operation;
                p.Appointments.Add(a2);
            }
            int o = 8;
            p.SaveChanges();
            int m = 8;
        }

        //Illness logic

        public void EditIllness(Illness illness)
        {
            p.Entry(illness).State = EntityState.Modified;
            p.SaveChanges();
        }

        public List<Illness> GetIllnesses()
        {
            return p.Illnesses.ToList();
        }

        public Illness GetIllnessByAppId(int appId)
        {
            Appointment app = p.Appointments.FirstOrDefault(x => x.AppointmentId == appId);
            Illness illness = app.Illness;

            return illness;
        }

        public void AddIllness(Illness illness)
        {
            p.Illnesses.Add(illness);
            p.SaveChanges();
        }

        public Illness GetIllness(int id)
        {
            Illness illness = p.Illnesses.FirstOrDefault(x => x.IllnessId == id);

            return illness;
        }


        //Nurses logic

        public void AddNurse(Nurse nurse)
        {
            p.Nurses.Add(nurse);
            p.SaveChanges();
        }

        public List<Nurse> GetNurses()
        {
            return p.Nurses.ToList();
        }
       

      //User logic

        public List<User> GetUsers()
        {
            return p.Users.ToList();
        }

        public User GetUserById(int id)
        {
            User User = new User();
            User = p.Users.FirstOrDefault(x => x.UserId == id);
            return User;
        }

        public void EditUser(User user)
        {
            p.Entry(user).State = EntityState.Modified;
            p.SaveChanges();
        }

        public User GetUser(int id)
        {
            User user = p.Users.Single(x => x.UserId == id);
            return user;
        }

        //other

        public void EditVisit(Illness visit)
        {
            p.Entry(visit).State = EntityState.Modified;
            p.SaveChanges();
        }

        public List<Illness> GetDoctorsIllnesses(int id)
        {
            Doctor doctor = p.Doctors.FirstOrDefault(d => d.User.UserId == id);

            List<Illness> illnesses = p.Illnesses.Where(x => x.Doctor.DoctorId == doctor.DoctorId).ToList();

            return illnesses;
        }

        public List<Illness> GetPatientIlls(int userId)
        {
            return p.Illnesses.Where(x => x.Patient.User.UserId == userId).ToList();
        }

        public bool IsNameUnique(string name)
        {
            bool IsNameRepeat = p.Users.Where(x => x.UserName == name).Any();
            return !IsNameRepeat;
        }
    }
}
