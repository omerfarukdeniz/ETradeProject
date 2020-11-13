using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class Product:BaseEntity
    {
        [Display(Name = "Ürün İsmi"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string ProductName { get; set; }        
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        public int? CategoryID { get; set; }

        //
        public virtual List<ProductAttribute> ProductAttributes { get; set; }
        public virtual Category Category { get; set; }

    }
}
