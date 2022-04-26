using Repository.Concrete;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Concrete
{
    public abstract class BaseManager<T> where T: class
    {
        protected  Store<T> _repo;
        public BaseManager(Store<T> Repo)
        {
            _repo = Repo;
        }
        public abstract T GetById(Guid Id);
        public abstract void Create(T Model);
    }
}
