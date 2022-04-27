using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PaymentCheck : ValidationAttribute
    {

        public PaymentCheck()
        {
        }

        public override bool IsValid(object value)
        {
            if(value == null || (string)value != "Expense" && (string)value != "Payment")
            {
                return false;
            }

            return true;
        }
    }
}
