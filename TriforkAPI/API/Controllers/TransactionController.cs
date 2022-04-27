using Logic.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly BaseManager<Transaction> _manager;
        private readonly BaseManager<Ledger> _ledger;
        private readonly BaseManager<Group> _group;

        public TransactionController(ILogger<TransactionController> logger, TransactionManager Man, GroupManager Group, LedgerManager Ledger)
        {
            _logger = logger;
            _manager = Man;
            _group = Group;
            _ledger = Ledger;
        }

        [HttpGet]
        [Route("getledger")]
        public IActionResult GenerateLedger([FromHeader(Name = "Id")] Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    return BadRequest("GroupId not found");
                }
                List<Transaction> _transactions = ((TransactionManager)_manager).GetListByGroupId(Id);
                Group group = _group.GetById(Id);
                Ledger ledger = ((LedgerManager)_ledger).GenerateLedger(_transactions, group);
                _ledger.Create(ledger);

                return Ok(ledger);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("generate-payments")]
        public IActionResult GeneratePayments([FromHeader(Name = "Id")] Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    return BadRequest("GroupId not found");
                }
                var _res = ((TransactionManager)_manager).GetListByGroupId(Id);
                var _members = ((GroupManager)_group).GetById(Id);
                var _notIn = _members.Participants.Where(x => !_res.Any(c => c.Payer == $"{x.FirstName} {x.LastName}")).ToList();
                _notIn.ForEach(x =>
                {
                    _res.Add(new Transaction
                    {
                        Payer = $"{x.FirstName} {x.LastName}",
                        Cost = 0
                    });
                });
                return Ok(_res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetList([FromHeader(Name = "Id")] Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    return BadRequest("GroupId not found");
                }
                var _res = ((TransactionManager)_manager).GetListByGroupId(Id);
                if (_res.Count > 0)
                {
                    var r = ((TransactionManager)_manager).GroupTotalByUser(_res);
                    Console.WriteLine(r);
                    var s = ((TransactionManager)_manager).GetUserTransactionsByType(_res);
                    Console.WriteLine(r);
                }
                return Ok(_res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get([FromHeader(Name = "Id")] Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    return BadRequest("GroupId not found");
                }
                var _result = _manager.GetById(Id);
                return Ok(_result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] Transaction Request)
        {
            try
            {
                Group group = _group.GetById(Request.GroupId);
                if(group == null || group.Participants.Count == 0)
                {
                    return BadRequest("No group members found ot process this transaction");
                }
                if (group.Participants.Select(x => $"{x.FirstName} {x.LastName}").Contains(Request.Payer) == false)
                {
                    return BadRequest("Payer and Payee names are made up by Group members First and Last names. To create a transaction they must match");
                }
                if(Request.Payee != null && Request.PaymentType == "Payment")
                {
                    if (group.Participants.Select(x => $"{x.FirstName} {x.LastName}").Contains(Request.Payee) == false)
                    {
                        return BadRequest("Payer and Payee names are made up by Group members First and Last names. To create a transaction they must match.");
                    }
                }
                _manager.Create(Request);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
