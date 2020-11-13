using BLL.DesignPatterns.RepositoryPattern.IRep;
using BLL.DesignPatterns.SingletonPattern;
using DAL.Context;
using MODEL.Entities;
using MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DesignPatterns.RepositoryPattern.BaseRep
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MyContext db;
        public BaseRepository()
        {
            db = DBTool.DBInstance;
        }

        public void Save()
        {
            db.SaveChanges();
        }


        //Linq
        public T Find(int id)
        {
            return db.Set<T>().Find(id);
        }
        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Where(exp).ToList();
        }
        public bool Any(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Any(exp);
        }
        public object Select(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Select(exp).ToList();
        }
        public T Default(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().FirstOrDefault(exp);
        }



        //Modify
        public void Add(T item)
        {
            db.Set<T>().Add(item);
            Save();
        }
        public virtual void Delete(T item)
        {
            item.DeletedDate = DateTime.Now;
            item.Status = DataStatus.Deleted;
            Save();
        }
        public void Destroy(T item)
        {
            db.Set<T>().Remove(item);
            Save();
        }
        public virtual void Update(T item)
        {
            T model = db.Set<T>().Find(item.ID);
            item.ModifiedDate = DateTime.Now;
            item.Status = DataStatus.Updated;
            db.Entry(model).CurrentValues.SetValues(item);
            Save();
        }



        //List

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }
        public List<T> GetActives()
        {
            return db.Set<T>().Where(x => x.Status != DataStatus.Deleted).ToList();
        }
        public List<T> GetModifieds()
        {
            return db.Set<T>().Where(x => x.Status == DataStatus.Updated).ToList();
        }
        public List<T> GetPassives()
        {
            return db.Set<T>().Where(x => x.Status == DataStatus.Deleted).ToList();
        }

        
    }
}
