using Models.Concrete;
using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Concrete
{
    public class GroupMemberManager : BaseManager<GroupMember>
    {
        public GroupMemberManager(StoreBuild<GroupMember> Repo) : base(Repo)
        {
            _repo = Repo;
        }
        public override void Create(GroupMember Model)
        {
            _repo.Create(Model);
        }
        public override GroupMember GetById(Guid Id)
        {
            return _repo.GetByCondition(x => x.Id.Equals(Id)).FirstOrDefault();
        }
        public List<GroupMember> GetMembers(Guid GroupId)
        {
            return _repo.GetByCondition(x => x.GroupId == GroupId).ToList();
        }
    }
}
