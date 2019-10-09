using System;
using System.Linq;
using System.Text.RegularExpressions;
using charl.test.stringExtensions.Extensions;
using charl.test.WebApi.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace charl.test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // POST api/students
        [SwaggerOperation(
            Summary = "Takes a collection of students and returns the refined results",
            Description = "Can be filtered by a string search and all results returns as lower-case",
            OperationId = "ProcessStudents",
            Tags = new[] { "Student searcher" }
        )]
        [HttpPost]
        public ActionResult<object> Post([FromBody] JArray students, string filter = "")
        {
            //For error handling I would usually implement global middleware but I expect it falls outside the scope of this test.
            try
            {
                //Here I filter the JArray items using Linq and Regular expressions.
                var searchResults = students.Where(x => Regex.IsMatch(x[ApiResources.NameField].Value<string>(), filter, RegexOptions.IgnoreCase));
                var returnValue = searchResults.Select(s =>
                    new
                    {
                        //Here I am using my custom "ToLower()" string extension cleverly named GetLower() to transform the values to lower case.
                        //The code is in the charl.test.stringExtensions.Extensions library.
                        Name = ((string)s[ApiResources.NameField]).GetLower(),
                        Surname = ((string)s[ApiResources.SurnameField]).GetLower(),
                        State = ((string)s[ApiResources.StateField]).GetLower()
                    }).ToArray();

                return Ok(returnValue);
            }
            catch (Exception e)
            {
                //Log the error here
                var apEx = new Exception(ApiResources.StudentControllerException, e);
                Console.WriteLine(apEx);
                throw apEx;
            }
        }
    }
}