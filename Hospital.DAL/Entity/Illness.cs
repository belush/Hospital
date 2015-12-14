using Hospital.DAL.Entity;
using Hospital.DAL.EntityMetaData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    [MetadataType(typeof(IllnessMetaData))]
    public class Illness
    {
        public int IllnessId { get; set; }
        public virtual Patient Patient { get; set; }
        public string Claim { get; set; }
        public DateTime? EnterDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public virtual Doctor Doctor { get; set; }
        //public virtual List<Appointment>? Appointments { get; set; }
        public string Diagnosis { get; set; }
        public string FinalDiagnosis { get; set; }
    }
}
