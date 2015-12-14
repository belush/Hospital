using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    [Table("Patient")]
    public class PatientMetaData
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Код пациента")]
        public int PatientId { get; set; }

        [Display(Name = "Пользователь")]
        public virtual User User { get; set; }
    }
}
