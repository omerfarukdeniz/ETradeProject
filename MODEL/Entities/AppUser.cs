using MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class AppUser:BaseEntity
    {
        [Display(Name = "Kullanıcı İsmi"),Required(ErrorMessage ="{0} girmek zorunludur!")]
        public string UserName { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string Password { get; set; }
        [Display(Name = "E-Posta"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public bool IsBanned { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivationCode { get; set; }
        public AppUser()
        {
            ActivationCode = Guid.NewGuid();
            Role = UserRole.Member;
        }

        //
        public virtual AppUserDetail AppUserDetail { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
