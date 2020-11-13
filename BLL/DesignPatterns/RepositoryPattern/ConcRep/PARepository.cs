using BLL.DesignPatterns.RepositoryPattern.BaseRep;
using MODEL.Entities;
using MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DesignPatterns.RepositoryPattern.ConcRep
{
    public class PARepository:BaseRepository<ProductAttribute>
    {
        public override void Update(ProductAttribute item)
        {
            ProductAttribute model = Default(x => x.ProductID == item.ProductID && x.AttributeID == item.AttributeID);
            item.Status = DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            db.Entry(model).CurrentValues.SetValues(item);
            Save();
        }
    }
}
