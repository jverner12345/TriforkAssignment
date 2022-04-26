using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class Transaction : Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Payer is required")]
        public string Payer { get; set; }
        [Required(ErrorMessage ="Expense name is required")]
        [MinLength(1, ErrorMessage ="A minimum length of One is required")]
        public string ExpenseName { get; set; }
        public string Description { get; set; }
        public string Payee { get; set; }
    }
}
