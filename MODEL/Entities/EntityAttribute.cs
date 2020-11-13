using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class EntityAttribute:BaseEntity
    {
        [Display(Name = "Özellik İsmi"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string AttributeName { get; set; }
        [Display(Name = "Açıklama"), Required(ErrorMessage = "{0} girmek zorunludur!")]
        public string Description { get; set; }
        //
        public virtual List<ProductAttribute> ProductAttributes { get; set; }

    }
}
