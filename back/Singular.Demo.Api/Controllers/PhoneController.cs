using Microsoft.AspNetCore.Mvc;
using Singular.Demo.Api.Db;
using Singular.Demo.Api.Models;

namespace Singular.Demo.Api.Controllers
{
    [ApiController]
    [Route("api/phone")]
    public class PhoneController : Controller
    {
        [HttpPost()]
        public IActionResult Add(Phone phone)
        {
            DbPhones.Add(phone);
            return Ok("Created");
        }

        [HttpPut()]
        public IActionResult Update(Phone phone)
        {
            DbPhones.Update(phone);
            return Ok("Updated");
        }

        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            DbPhones.Delete(id);
            return Ok("Updated");
        }

        [HttpGet("{id}")]
        public ActionResult<Phone> Get(int id)
        {
            return Ok(DbPhones.Get(id));
        }

        [HttpGet()]
        public ActionResult<Phone> Get()
        {
            return Ok(DbPhones.List());
        }
    }
}
