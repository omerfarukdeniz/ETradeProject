using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Options
{
    public class ProductAttributeMap:BaseMap<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.AttributeID,
                x.ProductID
            });
        }
    }
}
