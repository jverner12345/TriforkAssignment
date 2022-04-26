using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    interface IRepo<T>
    {
        IQueryable<T> GetList();
        void Create(T Model);
        void Update(T Model);
        void Delete(T Model);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    }
}
