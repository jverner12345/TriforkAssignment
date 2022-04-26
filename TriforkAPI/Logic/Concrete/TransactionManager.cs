using Models.Concrete;
using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Concrete
{
    public class TransactionManager : BaseManager<Transaction>
    {
        LedgerManager _ledger;
        public TransactionManager(StoreBuild<Transaction> Repo, LedgerManager Ledger) : base(Repo)
        {
            _repo = Repo;
            _ledger = Ledger;
        }
        public override void Create(Transaction Model)
        {
            _repo.Create(Model);
        }
        public List<Transaction> GetListByGroupId(Guid Id)
        {
            return _repo.GetByCondition(x => x.GroupId == Id).ToList();
        }


        public List<Transaction> GetUserTransactionsByType(List<Transaction> Transactions)
        {
            return Transactions.GroupBy(x => new { x.Payer, x.PaymentType })
                                .Select(X => new Transaction
                                {
                                    Cost = X.Sum(x => x.Cost),
                                    Payer = X.Key.Payer
                                }).ToList();
        }

        public List<Transaction> GroupTotalByUser(List<Transaction> Transactions)
        {
            return Transactions.GroupBy(x => new { x.Payer })
                                .Select(X => new Transaction
                                {
                                    Cost = X.Sum(x => x.Cost),
                                    Payer = X.Key.Payer
                                }).ToList();
        }
        public override Transaction GetById(Guid Id)
        {
            return _repo.GetByCondition(x => x.Id.Equals(Id)).FirstOrDefault();
        }
    }
}
