using Hospital.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.EntityMetaData
{
    [Table("Illness")]
    public class IllnessMetaData
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IllnessId { get; set; }

        public Patient Patient { get; set; }

       
        [Display(Name = "Жалоба")]
        public string Claim { get; set; }

        
        [Display(Name = "Дата обращения")]
        [DataType(DataType.Date)]
        public DateTime EnterDate { get; set; }

      
        [Display(Name = "Дата выписки")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public virtual Doctor Doctor { get; set; }

        //public virtual List<Appointment> Appointments { get; set; }
        
        [Display(Name = "Диагноз")]
        public string Diagnosis { get; set; }

        
        [Display(Name = "Окончательный диагноз")]
        public string FinalDiagnosis { get; set; }
    }
}
