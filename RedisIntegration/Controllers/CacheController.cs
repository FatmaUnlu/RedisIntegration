using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using RedisIntegration.Business.Interfaces;

namespace RedisIntegration.Controllers
{
    public class CacheController : ControllerBase
    {
        private readonly IRedisCacheServices _redisCacheServices;

        public CacheController(IRedisCacheServices redisCacheServices)
        {
            _redisCacheServices = redisCacheServices;
        }
        [HttpPost("GetData/{key}")]
        public async Task<IActionResult> GetData(string key)
        {
            return Ok(await _redisCacheServices.GetValueAsync(key));
        }
        [HttpPost("SetData/{key}/{value}")]
        public async Task<IActionResult> SetData(string key,string value)
        {
            await _redisCacheServices.SetValueAsync(key, value);
            return Ok();
        }
        [HttpPost("Delete/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _redisCacheServices.ClearKeyAsync(key);
            return Ok();
        }
    }
}
