using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Singular.Demo.Api.Db;
using Singular.Demo.Api.Models;

namespace Singular.Demo.Api.Controllers
{
    [ApiController]
    [Route("api/phone")]
    public class PhoneController : Controller
    {
        private readonly DbPhonesDbContext _dbPhonesDbContext;

        public PhoneController(DbPhonesDbContext dbPhonesDbContext)
            => _dbPhonesDbContext = dbPhonesDbContext;

        [HttpPost()]
        [Authorize("create:phones")]
        public ActionResult<Phone> Add(Phone phone)
        {
            _dbPhonesDbContext.Phones.Add(phone);
            _dbPhonesDbContext.SaveChanges();

            return Ok(phone);
        }

        [HttpPut()]
        [Authorize("update:phones")]
        public ActionResult<Phone> Update(Phone phone)
        {
            var existing = _dbPhonesDbContext.Phones.FirstOrDefault(x => x.Id == phone.Id);

            if (existing == null)
                return NotFound();

            existing.Brand = phone.Brand;
            existing.Model = phone.Model;
            existing.Number = phone.Number;

            _dbPhonesDbContext.Update(existing);
            _dbPhonesDbContext.SaveChanges();

            return Ok(phone);
        }

        [HttpDelete("{id}")]
        [Authorize("delete:phones")]
        public ActionResult<Phone> Delete(int id)
        {
            var existing = _dbPhonesDbContext.Phones.FirstOrDefault(x => x.Id == id);

            if (existing == null)
                return NotFound();

            _dbPhonesDbContext.Phones.Remove(existing);
            _dbPhonesDbContext.SaveChanges();

            return Ok(existing);
        }

        [HttpGet("{id}")]
        [Authorize("get:phones")]
        public ActionResult<Phone> Get(int id)
        {
            var existing = _dbPhonesDbContext.Phones.FirstOrDefault(x => x.Id == id);

            if (existing == null)
                return NotFound();

            return Ok(existing);
        }

        [HttpGet()]
        [Authorize("list:phones")]
        public ActionResult<IEnumerable<Phone>> List()
        {
            return Ok(_dbPhonesDbContext.Phones.ToList());
        }
    }
}
