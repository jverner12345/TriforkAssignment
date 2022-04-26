using Logic.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupMemberController : ControllerBase
    {

        private readonly ILogger<GroupController> _logger;
        private readonly GroupMemberManager _manager;

        public GroupMemberController(ILogger<GroupController> logger, GroupMemberManager Man)
        {
            _logger = logger;
            _manager = Man;
        }

        [HttpGet]
        public IActionResult Get(Guid GroupId)
        {
            try
            {
                if (GroupId == Guid.Empty)
                {
                    return BadRequest("GroupId not found");
                }
                var _result = _manager.GetMembers(GroupId);
                return Ok(_result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }        
        }

        /*[HttpPost]
        public IActionResult Create([FromBody] GroupMember Request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _manager.Create(Request);
                    return StatusCode(201);
                }
                var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();
                return BadRequest(errors.FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }*/
    }
}
