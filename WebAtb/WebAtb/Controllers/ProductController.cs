using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAtb.Data;
using WebAtb.Model;

namespace WebAtb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;
        public ProductController(IMapper mapper, AppEFContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Category()
        {
            Thread.Sleep(2000);
            var list = await _context.Products.Select(x => _mapper.Map<ProductItemViewModel>(x))
                .AsQueryable().ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] ProductDelViewModel model)
        {

            var res = _context.Products.FirstOrDefault(x => x.Id == model.Id);
            if (res == null)
            {
                return BadRequest(new { message = "Check id!" });
            }

            _context.Products.Remove(res);
            _context.SaveChanges();
            return Ok(new { message = "User deleted" });
        }
    }
}
