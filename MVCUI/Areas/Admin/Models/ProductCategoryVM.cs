using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCUI.Areas.Admin.Models
{
    public class ProductCategoryVM
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}