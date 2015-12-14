using Hospital.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("UserContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
    }
}
