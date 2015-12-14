using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    [MetadataType(typeof(NurseMetaData))]
    public class Nurse
    {
        public int NurseId { get; set; }
        public virtual User User { get; set; }
    }
}
