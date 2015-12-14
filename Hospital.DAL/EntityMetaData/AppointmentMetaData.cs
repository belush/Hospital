using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.Entity
{
    [Table("Appointment")]
    public class AppointmentMetaData
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Код назначения")]
        public int AppointmentId { get; set; }

        //[Display(Name = "Болезнь")]
        //public virtual Illness Illness { get; set; }

        [Display(Name = "Тип")]
        public string AppointmentType { get; set; }

        [Display(Name = "Название")]
        public string AppointmentName { get; set; }

        [Display(Name = "Выполнено")]
        public bool IsAppointmentDone { get; set; }
    }
}
