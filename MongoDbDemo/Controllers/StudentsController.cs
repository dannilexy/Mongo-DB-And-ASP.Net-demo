using Microsoft.AspNetCore.Mvc;
using MongoDbDemo.Models;
using MongoDbDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: api/<StudentsController>
        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
           return _studentService.Get();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id)
        {
            return _studentService.GetById(id);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student value)
        {
            return _studentService.Create(value);   
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Student value)
        {

            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentService.Update(id, value);
            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentService.Delete(id);
            return Ok();
        }
    }
}
