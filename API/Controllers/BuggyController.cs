using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController: BaseApiController
    {
        private readonly ShopContext _context;
        public BuggyController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] 
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(42);
            if(thing==null){return NotFound(new ApiResponse(404));}
            return Ok();
        }
        [HttpGet("servererror")] 
        public ActionResult GetServerEroor()
        {
            var thing = _context.Products.Find(42);
            var thingToReturn = thing.ToString();
            return Ok();
        }
        [HttpGet("badrequest")] 
        public ActionResult GetBadRequest()
        {
            
            return BadRequest(new ApiResponse(404));
        }
        [HttpGet("badrequest/{id}")] 
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}