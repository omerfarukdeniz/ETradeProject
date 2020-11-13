using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCUI.Models
{
    public class ProductAttributesVM
    {
        public Product Product { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }

    }
}