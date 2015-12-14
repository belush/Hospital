using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL
{
    public class Visit
    {
        public DateTime Date{ get; set; }
        public List<Illness> Ills{ get; set; }
    }
}
