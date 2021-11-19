using MyEvernote.Common;
using MyEvernote.Core.DataAccess;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyEvernote.DataAccessLayer.Mocks
{

    public class MockRepository<T> : IDataAccess<T> where T : class
    {
        public List<T> List()
        {
            var pi = typeof(MockDataSets).GetProperties().FirstOrDefault(x => x.PropertyType == typeof(List<T>));
            return (List<T>)pi.GetValue(null);
        }

        public IQueryable<T> ListQueryable()
        {
            return List().AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return ListQueryable().Where(where).ToList();
        }

        public int Insert(T obj)
        {
            List().Add(obj);

            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUsername = App.Common.GetCurrentUsername();
            }

            return Save();
        }

        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername();
            }

            return Save();
        }

        public int Delete(T obj)
        {
            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;

            //    o.ModifiedOn = DateTime.Now;
            //    o.ModifiedUsername = App.Common.GetUsername();
            //}

            List().Remove(obj);
            return Save();
        }

        public int Save()
        {
            return 1;
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return ListQueryable().FirstOrDefault(where);
        }
    }
}
