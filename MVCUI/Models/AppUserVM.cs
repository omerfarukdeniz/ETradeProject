using MODEL.Entities;
using MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCUI.Models
{
    public class AppUserVM
    {
        public AppUser AppUser { get; set; }
        public AppUserDetail AppUserDetail { get; set; }
        public Gender Gender { get; set; }
    }
}