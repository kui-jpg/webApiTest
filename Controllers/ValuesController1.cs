using Microsoft.AspNetCore.Mvc;
using webApiTest.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webApiTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController1 : ControllerBase
    {
        // GET: api/<ValuesController1>
        [HttpGet]
        public IEnumerable<student> Get()
        {
            return new student[] { new student() {
             Id=1,
            Name="Alan.hsiang",
            Age=20,
             Sex=true
         }, new student() {
            Id=2,
            Name="Json.hsiang",
            Age=18,
            Sex=false
         } };
        }

        // GET api/<ValuesController1>/5
        [Route("student/{id}")]
        [HttpGet]
        public student Get(int id,string name)
        {
            return new student()
            {
                Id = id,
                Name =name,
                Age = 20,
                Sex = true
            };
        }

        [Route("Query/{id}")]
        [HttpGet]
        public string Query(int id, string name)
        {
            return string.Format("[Query]的值：id={0},name={1}", id, name);
        }

        //POST api/<ValuesController1>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController1>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController1>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        

    }
}
