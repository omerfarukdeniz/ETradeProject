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
    public class ProductRepository:BaseRepository<Product>
    {
        public override void Delete(Product item)
        {
            item.DeletedDate = DateTime.Now;
            item.Status = DataStatus.Deleted;
            item.CategoryID = null;
            Save();
        }
    }
}
