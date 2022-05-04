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
        public IActionResult Add(Phone phone)
        {
            _dbPhonesDbContext.Phones.Add(phone);
            _dbPhonesDbContext.SaveChanges();

            return Ok("Created");
        }

        [HttpPut()]
        public IActionResult Update(Phone phone)
        {
            var existing = _dbPhonesDbContext.Phones.FirstOrDefault(x => x.Id == phone.Id);

            if (existing == null)
                return NotFound();

            existing.Brand = phone.Brand;
            existing.Model = phone.Model;
            existing.Number = phone.Number;

            _dbPhonesDbContext.Update(existing);
            _dbPhonesDbContext.SaveChanges();

            return Ok("Updated");
        }

        [HttpDelete()]
        public IActionResult Delete(int id)
        {
            var existing = _dbPhonesDbContext.Phones.FirstOrDefault(x => x.Id == id);

            if (existing == null)
                return NotFound();

            _dbPhonesDbContext.Phones.Remove(existing);
            _dbPhonesDbContext.SaveChanges();

            return Ok("Deleted");
        }

        [HttpGet("{id}")]
        public ActionResult<Phone> Get(int id)
        {
            var existing = _dbPhonesDbContext.Phones.FirstOrDefault(x => x.Id == id);

            if (existing == null)
                return NotFound();

            return Ok(existing);
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Phone>> List()
        {
            return Ok(_dbPhonesDbContext.Phones.ToList());
        }
    }
}
