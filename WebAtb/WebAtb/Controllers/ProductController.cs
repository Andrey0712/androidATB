using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebAtb.Data;
using WebAtb.Data.Entities;
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

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetData(int id)
        {
            var cultureInfo = new CultureInfo("uk-UA");
            var product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            ProductViewModelImages model = new ProductViewModelImages
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description=product.Description,
                StartPhoto=product.StartPhoto,
                DateCreate=product.DateCreate.ToString("dd.MM.yyyy HH:mm:ss"),
                DateFinish=product.DateFinish.ToString("dd.MM.yyyy HH:mm:ss"),
                Images = product.ProductImages.Select(x => new ProductImageItemViewModel
                {
                    Path = x.Name
                }).ToList()
            };
            return Ok(model);
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
            return Ok(new { message = "Product deleted" });
        }

        [HttpPost]
        [Route("rate")]
        public IActionResult Rate(ProductRateViewModel model)
        {
            //var product = null;
            var res = _context.Products.Include(i => i.ProductImages).FirstOrDefault(x => x.Id == model.Id);
            if (res == null)
            {
                return BadRequest(new { message = "Check id!" });
            }
            else
            {
                var rate = res.Price;
                if (res.DateFinish >= DateTime.Now || rate<model.Price)
                {

                    res.Id = model.Id;
                    res.Price = model.Price;
                    //res.Price = rate + 10;
                }
                else
                {
                    return BadRequest(new { message = "Лот закрыт" });
                }

            }
            _context.Products.Add(res);
            _context.SaveChanges();
            return Ok(new { message = "Add rate" });
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] ProductAddViewModel model)
        {
            try
            {
                List<string> fileNames = new List<string>();
                foreach (var item in model.Images)
                {
                    string fileName = "";
                    if (item != null)
                    {
                        var ext = Path.GetExtension(item.FileName);
                        fileName = Path.GetRandomFileName() + ext;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "uploads", fileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            item.CopyTo(stream);
                        }
                        fileNames.Add(fileName);
                    }
                }

                string startFoto = String.Empty;
                var product = _mapper.Map<Product>(model);

                if (model.StartPhoto != null)
                {
                    string randomFilename = Path.GetRandomFileName() +
                        Path.GetExtension(model.StartPhoto.FileName);

                    string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                    startFoto = Path.Combine(dirPath, randomFilename);
                    using (var file = System.IO.File.Create(startFoto))
                    {
                        model.StartPhoto.CopyTo(file);
                    }
                    product.StartPhoto = randomFilename;
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                foreach (var img in fileNames)
                {
                    ProductImage productImage = new ProductImage()
                    {
                        Name = img,
                        ProductId = product.Id
                    };
                    _context.ProductImages.Add(productImage);
                    _context.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }


       
    }
}
