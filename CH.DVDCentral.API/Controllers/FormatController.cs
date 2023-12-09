using CH.DVDCentral.BL;
using CH.DVDCentral.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CH.DVDCentral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Format> Get()
        {
            return FormatManager.Load();
        }


        [HttpGet("{id}")]
        public BL.Models.Format Get(int id)
        {
            return FormatManager.LoadById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Format format)
        {
            try
            {
                int results = FormatManager.Insert(format);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Format format)
        {
            try
            {
                int results = FormatManager.Update(format);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int results = FormatManager.Delete(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
