using KLA.NumberToText.Exceptions;
using KLA.NumberToText.NumberConverter;
using Microsoft.AspNetCore.Mvc;

namespace KLA.NumberToText.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumbertToTextController : ControllerBase
    {
        private readonly INumberConverterService _numberConverterService;
        public NumbertToTextController(INumberConverterService  numberConverterService)
        {
         _numberConverterService= numberConverterService;
        }

        [HttpGet(Name = "Convert")]
        public ActionResult<string> Get(string number)
        {

            try 
            {
                return Ok(_numberConverterService.Convert(number));
            }
            catch(NumberToTextException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}