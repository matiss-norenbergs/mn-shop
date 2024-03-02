using Microsoft.AspNetCore.Mvc;
using MN_Shop.Server.Helpers;
using MN_Shop.Server.ModelsJson;

namespace MN_Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<KeyValuePairJson>> Get()
        {
            try
            {
                var jsonData = new List<KeyValuePairJson>();

                var timeZoneCollection = GenericHelper.GetTimeZoneCollection();
                foreach (var item in timeZoneCollection)
                {
                    jsonData.Add(new KeyValuePairJson
                    {
                        Key = item.Key,
                        Value = item.Value.DisplayName
                    });
                }

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
