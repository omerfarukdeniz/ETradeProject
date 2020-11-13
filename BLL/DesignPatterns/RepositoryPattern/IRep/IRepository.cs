using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DesignPatterns.RepositoryPattern.IRep
{
    interface IRepository<T> where T:BaseEntity
    {
        //Linq
        List<T> Where(Expression<Func<T, bool>> exp);
        T Default(Expression<Func<T, bool>>exp);
        bool Any(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, bool>>exp);
        T Find(int id);

        //Modify
        void Add(T item);
        void Delete(T item);
        void Destroy(T item);
        void Update(T item);

        //List
        List<T> GetAll();
        List<T> GetActives();
        List<T> GetModifieds();
        List<T> GetPassives();
    }
}
