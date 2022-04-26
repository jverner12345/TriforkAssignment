using Logic.Interface;
using Models.Concrete;
using Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = Models.Concrete;

namespace Logic.Concrete
{
    public class LedgerManager : BaseManager<Ledger>
    {
        public LedgerManager(StoreBuild<Ledger> Repo) : base(Repo)
        {
            _repo = Repo;
        }
        public Ledger GenerateLedger(List<Transaction> Transactions, Group Group)
        {
            Ledger _ledger = new Ledger();
            if (Transactions == null || Transactions.Count == 0)
            {
                return _ledger;
            }
            decimal _totalCost = Transactions.Sum(x => x.Cost);
            decimal _totalPaid = _totalCost - Transactions
                                                    .Where(x => x.PaymentType == "Payment")
                                                    .Sum(x => x.Cost);
            decimal _percentageRemaining = (_totalPaid / _totalCost) * 100;
            _ledger.PercentageRemaining = _percentageRemaining;
            _ledger.TotalCost = _totalCost;
            _ledger.TotalPaid = _totalPaid;
            _ledger.SettledDate = DateTime.Now;
            _ledger.Totals = Group.Participants.Select(member =>
            {
                return new MemberTotal(Transactions
                                    .Where(x => x.Payer == $"{member.FirstName} {member.LastName}")
                                    .Cast<Transaction>()
                                    .ToList(), Group.Participants.Count, _totalCost)
                {
                    MemberName = $"{member.FirstName} {member.LastName}"
                };
            }).ToList();
            _ledger.Id = Guid.NewGuid();
            return _ledger;
        }

        public override void Create(Ledger Model)
        {
            _repo.Create(Model);
        }

        public override Ledger GetById(Guid Id)
        {
            return _repo.GetByCondition(x => x.Id.Equals(Id)).FirstOrDefault();
        }
    }
}
