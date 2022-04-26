using Models.Concrete;
using Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class StoreBuild<T> : Store<T> where T: class
    {
        private FakeContext<T> _store;
        public StoreBuild(FakeContext<T> store)
        {
            _store = store;
        }
        public override IQueryable<T> GetList()
        {
            return _store.GetList();
        }
        public override void Create(T Model)
        {
            _store.Create(Model);
        }

        public override IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _store.GetByCondition(expression);
        }
    }

    public class FakeContext<T>
    {
        private List<T> _currentStore { get; set; }
        private List<List<T>> _data;
        public FakeContext()
        {
            _currentStore = GetStore();
            if (_currentStore == null)
            {
                _currentStore = new List<T>();
            }
        }
        private List<T> GetStore()
        {
            if (_data == null)
                return null;
            return _data.Select(list => list).Where(y => y.Any(x => x.GetType().Equals(typeof(T)))).FirstOrDefault();
        }

        public IQueryable<T> GetList()
        {
            return _currentStore.AsQueryable<T>();
        }
        public void Create(T Model)
        {
            _currentStore.Add(Model);
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            if(_currentStore == null || _currentStore.Count() == 0)
            {
                return new List<T>().AsQueryable();
            }
            return _currentStore.AsQueryable<T>().Where(expression);
        }

    }
}
