using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DAL.EntityMetaData
{
    [Table("Doctor")]
    public class DoctorMetaData
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Код врача")]
        public int DoctorId { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Пользователь")]
        //без virtual не хранило персону
        public virtual User User { get; set; }
    }
}
