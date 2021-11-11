using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETCalculator.Model;
using RestWithASPNETCalculator.Services;

namespace RestWithASPNETCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {


        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }   
        
        [HttpPost("{id}")]
        public IActionResult Post([FromBody] Person person)
        {            
            if (person == null) return BadRequest();
            return Ok(_personService.Create(person));
        }  
        
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {            
            if (person == null) return BadRequest();
            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }

        //[HttpGet("sub/{firstNumber}/{secondNumber}")]
        //public IActionResult Sub(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
        //        return Ok(sub.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("div/{firstNumber}/{secondNumber}")]
        //public IActionResult Div(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
        //        return Ok(div.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("mult/{firstNumber}/{secondNumber}")]
        //public IActionResult Mult(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var mult = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
        //        return Ok(mult.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("med/{firstNumber}/{secondNumber}")]
        //public IActionResult Med(string firstNumber, string secondNumber)
        //{
        //    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        //    {
        //        var med = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
        //        return Ok(med.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        //[HttpGet("sqrt/{firstNumber}")]
        //public IActionResult Sqrt(string firstNumber)
        //{
        //    if (IsNumeric(firstNumber))
        //    {
        //        var sqrt = Math.Sqrt((double)ConvertToDecimal(firstNumber));
        //        return Ok(sqrt.ToString());
        //    }
        //    return BadRequest("Invalid Input");
        //}

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

    }
}
