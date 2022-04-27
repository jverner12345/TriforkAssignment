using API.Controllers;
using Logic.Concrete;
using Microsoft.AspNetCore.Mvc;
using Models.Concrete;
using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TriforkTesting
{
    public class TestManager
    {
       
        public List<Transaction> _data = new List<Transaction>()
        {
                new Transaction
                {
                    Cost = 10,
                    Id = new Guid("321e6718-c1f9-46a4-8024-66a1fb469852"),
                    Payee = "Jamie",
                    Payer = "Test",
                    PaymentType = "Expense",
                    ExpenseName = "job",
                }
         };

        public void Create(Transaction Model)
        {
            _data.Add(Model);
        }

        public Transaction GetById(Guid Id)
        {
            var res = _data.Where(x => x.Id == Id); 
            if(res == null || res.Count() == 0)
            {
                return null;
            }
            return res.First();
        }
    }
    public class GroupManagerTest : BaseManager<Group>
    {
        public GroupManagerTest(Store<Group> s) : base(s)
        {

        }
        private List<Group> _data = new List<Group>()
            {
                new Group
                {
                    Id = new Guid("321e6718-c1f9-46a4-8024-66a1fb469852"),
                    Participants = new System.Collections.Generic.List<GroupMember>
                    {
                        new GroupMember
                        {
                            FirstName = "Jamie",
                            LastName = "Verner",
                            Email = "jverner75@gmailcom"
                        }
                    }
                }
            };

        public override void Create(Group Model)
        {
            _data.Add(Model);
        }

        public override Group GetById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
    public class TestTransactions
    {
        private readonly TestManager _manager;

        public TestTransactions()
        {
            _manager = new TestManager();
        }

        [Fact]
        public void Task_Get_Transaction_By_ID()
        {
            //321e6718-c1f9-46a4-8024-66a1fb469852
            var id = new Guid("321e6718-c1f9-46a4-8024-66a1fb469852");
            // Act  
            var res = _manager.GetById(id);
            //Assert  
            Assert.NotNull(res);
        }

        [Fact]
        public void Task_Add_New_Transaction()
        {
            // Arrange  
            _manager._data = new List<Transaction>();
            var _t = new Transaction
            {
                Cost = 10,
                GroupId = new Guid(),
                Payee = "Jamie",
                Payer = "Test",
                PaymentType = "Expense",
                ExpenseName = "job"
            };
            // Act  
            _manager.Create(_t);
            //Assert  
            Assert.Single(_manager._data);
        }

    }

}
