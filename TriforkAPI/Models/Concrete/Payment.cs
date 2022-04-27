using API.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class MemberTotal
    {
        public string MemberName { get; set; }
        public List<Transaction> Transactions { get; set; }
        private decimal _totalAmount { get; set; }
        public decimal TotalAmount
        {
            get
            {
                return _totalAmount;
            }
        }
        private decimal _totalPaid { get; set; }
        public decimal TotalPaid
        {
            get
            {
                return _totalPaid;
            }
        }
        private decimal _amountOwed { get; set; }
        public decimal AmountOwed
        {
            get
            {
                return _amountOwed;
            }
        }
        public MemberTotal(List<Transaction> transactions, int Members, decimal totalAmount)
        {
            Transactions = transactions;
            _totalAmount = totalAmount;
            _totalPaid = Transactions.Sum(x => x.Cost);
            _amountOwed = Members == 0 ? _totalAmount : _totalAmount - _totalPaid;
        }
    }
    public class Payment
    {
        public Guid GroupId { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public decimal Cost { get; set; }
        public DateTime CreateDate { get; set; }
        [Required(ErrorMessage = "Expense name is required.")]
        [PaymentCheck(ErrorMessage = "Either 'Payment' or 'Expense' is required")]
        public string PaymentType { get; set; }

    }
}
