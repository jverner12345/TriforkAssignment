using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Concrete;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace payroc_shortener.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ShortenController : ControllerBase
    {
        
        private readonly ILogger<ShortenController> _logger;
        private readonly IRepoWrapper _repoWrapper;
        public ShortenController(ILogger<ShortenController> logger, IRepoWrapper Wrapper)
        {
            _logger = logger;
            _repoWrapper = Wrapper;
        }

        [HttpGet]
        public string GetLink([FromHeader(Name ="key")] string Key)
        {
            try
            {
                ShortenModel exists = null;
                if (!string.IsNullOrEmpty((string)Key))
                {
                    exists = _repoWrapper.Shortener.GetByCondition(x => x.Key.Equals(Key)).FirstOrDefault();
                    if (exists != null)
                    {
                        exists.ClickCount = exists.ClickCount + 1;
                        return exists.LongLink;
                    }
                }
                return default;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public ShortenModel GenerateLink([FromBody] ShortenModel Request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IQueryable<ShortenModel> exists = null;
                    if (!string.IsNullOrEmpty(Request.Key))
                    {
                        exists = _repoWrapper.Shortener.GetByCondition(x => x.LongLink.Equals(Request.LongLink));
                    }
                    if (exists != null && exists.Any())
                    {
                        return exists.FirstOrDefault();
                    }
                    Request.GenerateNewKey();
                    _repoWrapper.Shortener.Create(Request);
                    return Request;
                }
                return default;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }


    }
}
