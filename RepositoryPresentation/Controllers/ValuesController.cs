using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryPresentation.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            //var twice = (int x) => x * 2;

            Func<int, int> twiceD = x => x * 2;
            Expression<Func<int, int>> twiceE = y => y * 2;

            ParameterExpression z = Expression.Parameter(typeof(int), "x");
            Expression<Func<int, int>> twiceEex = Expression.Lambda<Func<int, int>>(
                Expression.Multiply(
                    z,
                    Expression.Constant(2)
                ),
                z
            );


            Func<int, int> twiceDEx = twiceE.Compile();

            int result = twiceDEx(2);



            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
