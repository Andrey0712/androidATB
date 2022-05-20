using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAtb.Data;
using WebAtb.Data.Entities;
using WebAtb.Model;

namespace WebAtb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;
        public CategoryController(IMapper mapper, AppEFContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Category()
        {
            Thread.Sleep(2000);
            var list = await _context.ProductCategories.Select(x => _mapper.Map<CategoryItemViewModel>(x))
                .AsQueryable().ToListAsync();

            return Ok(list);
        }

        [HttpPost("create")]
        //[Authorize]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            ProductCategory category = _mapper.Map<ProductCategory>(model);
            _context.ProductCategories.Add(category);
            _context.SaveChanges();

            return Ok(new { id = category.Id });
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] CategoryDelViewModel model)
        {

            var res = _context.ProductCategories.FirstOrDefault(x => x.Id == model.Id);
            if (res == null)
            {
                return BadRequest(new { message = "Check id!" });
            }

            _context.ProductCategories.Remove(res);
            _context.SaveChanges();
            return Ok(new { message = "User deleted" });
        }
    }
}
