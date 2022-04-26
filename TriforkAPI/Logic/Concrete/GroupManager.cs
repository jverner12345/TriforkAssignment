using Models.Concrete;
using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Concrete
{
    public class GroupManager : BaseManager<Group>
    {
        public GroupManager(StoreBuild<Group> Repo) : base(Repo)
        {
            _repo = Repo;
        }
        public override void Create(Group Model)
        {
            _repo.Create(Model);
        }
        public List<Group> GetList()
        {
            return _repo.GetList().ToList();
        }
        public override Group GetById(Guid Id)
        {
            return _repo.GetByCondition(x => x.Id.Equals(Id)).FirstOrDefault();
        }
    }
}
