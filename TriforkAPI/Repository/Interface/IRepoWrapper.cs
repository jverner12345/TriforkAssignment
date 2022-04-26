using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepoWrapper<T> where T: class
    {
        Store<T> Shortener { get; }
    }
}
