using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class Category:BaseEntity
    {
        [Display(Name = "Kategori İsmi"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string CategoryName { get; set; }

        [Display(Name = "Açıklama"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string Description { get; set; }
        //
        public virtual List<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
