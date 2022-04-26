using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class RepoWrapper<T> : IRepoWrapper<T> where T: class
    {
        private Store<T> _shortener;

        public RepoWrapper()
        {
        }
        public Store<T> Shortener
        {
            get
            {
                if (_shortener == null)
                {
                    _shortener = new Store<T>();
                }
                return _shortener;
            }
        }
    }
}
