using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class ProductAttribute:BaseEntity
    {
        public int AttributeID { get; set; }
        public int ProductID { get; set; }
        public string Value { get; set; }
        //

        public virtual EntityAttribute Attribute { get; set; }
        public virtual Product Product { get; set; }

    }
}
