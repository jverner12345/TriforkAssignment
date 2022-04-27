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
    public class GroupController : ControllerBase
    {

        private readonly ILogger<GroupController> _logger;
        private readonly BaseManager<Group> _manager;

        public GroupController(ILogger<GroupController> logger, GroupManager Man)
        {
            _logger = logger;
            _manager = Man;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetList()
        {
            try
            {
                var _res = ((GroupManager)_manager).GetList();
                return Ok(_res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
                var _result = _manager.GetById(GroupId);
                return Ok(_result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Group Request)
        {
            //try
            //{
            _manager.Create(Request);
            return Ok(Request.Id);
            //}
            /*catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }*/
        }
    }
}
