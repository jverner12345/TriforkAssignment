using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class Ledger
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SettledDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal PercentageRemaining { get; set; }
        public List<MemberTotal> Totals { get; set; }
        public List <Transaction> PaymentPlan { get; set; }
    }


}
