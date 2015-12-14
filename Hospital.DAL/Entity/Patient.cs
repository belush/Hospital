using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    [MetadataType(typeof(PatientMetaData))]
    public class Patient
    {
        public int PatientId { get; set; }        
        public virtual User User { get; set; }
    }
}
