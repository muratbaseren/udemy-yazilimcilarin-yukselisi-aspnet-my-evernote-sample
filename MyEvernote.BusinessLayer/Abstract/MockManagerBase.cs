using MyEvernote.Core.DataAccess;
using MyEvernote.DataAccessLayer.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyEvernote.BusinessLayer.Abstract
{
    public abstract class MockManagerBase<T> : IDataAccess<T> where T : class
    {
        private MockRepository<T> repo = new MockRepository<T>();

        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public virtual int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
