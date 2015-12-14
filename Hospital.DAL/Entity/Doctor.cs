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
    [MetadataType(typeof(DoctorMetaData))]
    public class Doctor
    {       
        public int DoctorId { get; set; }
        public string Category { get; set; }
        public virtual User User { get; set; }
    }
}
