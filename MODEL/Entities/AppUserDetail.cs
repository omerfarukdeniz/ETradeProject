using MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class AppUserDetail:BaseEntity
    {
        [Display(Name = "İsim"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string FirstName { get; set; }
        [Display(Name = "Soyisim"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string LastName { get; set; }
        
        
        public Gender? Gender
        {
            get;
            set;
        }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Adress1 { get; set; }
        public string Adress2 { get; set; }

        //
        public virtual AppUser AppUser { get; set; }


    }
}
