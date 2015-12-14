using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entity
{
    [MetadataType(typeof(AppointmentMetaData))]
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public virtual Illness Illness { get; set; }
        public string AppointmentType { get; set; }
        public string AppointmentName { get; set; }
        public bool IsAppointmentDone { get; set; }
    }
}
