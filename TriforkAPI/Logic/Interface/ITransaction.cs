using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interface
{
    public interface ILedger<T> where T : class
    {
       Ledger GenerateLedger(List<Transaction> Transactions, Group Group);
        Transaction GeneratePossiblePaymentPlan(List<Transaction> transactions, Group group);
    }
}
