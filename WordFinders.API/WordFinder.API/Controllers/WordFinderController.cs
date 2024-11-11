using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WordFinder.API.Helpers;

namespace WordFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFinderController : ControllerBase
    {

        private readonly IWebHostEnvironment  _hostingEnvironment;

        public WordFinderController(IWebHostEnvironment hostEnvironment)
        {
            _hostingEnvironment = hostEnvironment;
        }


        // POST api/<BoardController>
        [HttpPost("init")]
        public IActionResult Post([FromBody] string[] value)
        {
            var filename = string.Empty;
            if (value.Length == 0)
                return BadRequest();
            try
            {
                filename = Guid.NewGuid().ToString();
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, filename + ".txt");

                System.IO.File.WriteAllText(filePath, string.Join(Environment.NewLine, value));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok(filename);
        }

        // POST api/<BoardController>
        [HttpPost("find/{id}")]
        public IActionResult Find([FromBody] string[] value, string id)
        {
            var filename = string.Empty;
            IEnumerable<string> retval = new List<string>();
            if (value.Length == 0 || string.IsNullOrEmpty(id))
                return BadRequest();
            try
            {
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, id + ".txt");

                if (System.IO.File.Exists(filePath))
                {
                    var list = System.IO.File.ReadAllLines(filePath);
                    var finder = new FinderHelper(list);
                    retval = finder.Find(value);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NoContent, "The give id doesn't have a valid board.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok(retval);
        }
    }
}
