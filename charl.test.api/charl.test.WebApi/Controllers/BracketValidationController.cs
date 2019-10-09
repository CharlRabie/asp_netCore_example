using System;
using charl.test.stringExtensions.Extensions;
using charl.test.WebApi.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace charl.test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BracketValidationController : Controller
    {
        [SwaggerOperation(
            Summary = "Validates a code string and returns true or false",
            Description = "Validates a code string and returns true or false based on the brackets",
            OperationId = "BracketValidator",
            Tags = new[] { "Bracket Validator" }
        )]
        [HttpPost]
        public ActionResult<bool> Post(string input)
        {
            //For error handling I would usually implement global middleware to handle exception but I expect it falls outside the scope of this test.
            try
            {
                //Here I use my ValidateBrackets string extension to validate the brackets.
                //The code is in the charl.test.stringExtensions.Extensions library.
                var returnValue = input.ValidateBrackets();

                return Ok(returnValue);
            }
            catch (Exception e)
            {
                var apEx = new Exception(ApiResources.BracketControllerException, e);
                Console.WriteLine(apEx);
                throw apEx;
            }
        }
    }
}