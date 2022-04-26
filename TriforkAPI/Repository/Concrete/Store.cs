using Models.Concrete;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{

    public class Store<T> : IRepo<T> where T : class
    {
        public virtual void Create(T Model)
        {
        }

        public virtual void Delete(T Model)
        {
        }

        public virtual IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> GetList()
        {
            return default;
        }

        public virtual void Update(T Model)
        {

        }
    }
}
