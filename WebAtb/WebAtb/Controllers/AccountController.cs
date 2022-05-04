using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using WebAtb.Data;
using WebAtb.Data.Entities.Identity;
using WebAtb.Helpers;
using WebAtb.Model;
using WebAtb.Servise;

namespace WebAtb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService  _jwtTokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppEFContext _context;
        private IHostEnvironment _host;

        public AccountController(UserManager<AppUser> userManeger, SignInManager<AppUser> signInManager,
            AppEFContext context,IMapper mapper, IJwtTokenService jwtTokenService, IHostEnvironment host)
        {
            _userManager = userManeger;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _context = context;
            _host = host;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var img = ImageWorker.FromBase64StringToImage(model.Photo);
            string randomFilename = Path.GetRandomFileName() + ".jpeg";
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
            img.Save(dir, ImageFormat.Jpeg);
            var user = _mapper.Map<AppUser>(model);
            user.Photo = randomFilename;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });


            return Ok(new { token = _jwtTokenService.CreateToken(user) });


        }

        /*[HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            var user=await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new { message = "User isn't find" });
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Incorrect password!" });
            }


            return Ok(new { token = _jwtTokenService.CreateToken(user) });
        }*/

        [HttpGet]
        //[Authorize]
        [Route("users")]
        public async Task<IActionResult> Users()
        {
            Thread.Sleep(2000);
            var list = await _context.Users.Select(x => _mapper.Map<UserItemViewModel>(x))
                .AsQueryable().ToListAsync();

            return Ok(list);
        }

        /// <summary>
        /// Вхід на сайт
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає токен авторизації</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Login user</response>
        /// <response code="400">Login has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your login right now</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponceViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        return Ok(new TokenResponceViewModel { token = _jwtTokenService.CreateToken(user) });
                    }
                }
                return BadRequest(new { error = "Користувача не знайдено" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Помилка на сервері. " + ex.Message });
            }
        }

       /* [HttpPut]
        //[Authorize]
        [Route("updateuser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserEditViewModel model)
        {
            
            Thread.Sleep(1000);
            var user = _context.Users
                .FirstOrDefault(x => x.Id == model.Id);
            if (user == null)
                return NotFound();
            if (!string.IsNullOrEmpty(model.Photo))
            {
                var img = ImageWorker.FromBase64StringToImage(model.Photo);
                string randomFilename = Path.GetRandomFileName() + ".jpeg";
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
                img.Save(dir, ImageFormat.Jpeg);
                user.Photo = randomFilename;
            }
            user.PhoneNumber = model.Phone;
            user.FirstName = model.FirstName;
            user.SecondName = model.SecondName;
            
            _context.SaveChanges();
            return Ok(_mapper.Map<UserItemViewModel>(user));
        }*/

        [HttpPost]
        [Route("delete")]

        public IActionResult Delete([FromBody] UserDelViewModel model)
        {

            var res = _context.Users.FirstOrDefault(x => x.Id == model.Id);
            if (res == null)
            {
                return BadRequest(new { message = "Check id!" });
            }

            _context.Users.Remove(res);
            _context.SaveChanges();
            return Ok(new { message = "User deleted" });
        }

        //[HttpPut]
        //[Authorize]
        [HttpPost]
        [Route("edit")]
        public IActionResult EditUser([FromForm] RegisterViewModel model)
        {
            var res = _context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (model == null)
            {
                return BadRequest(new { message = "Не зашла инфа" });
            }

            res.PhoneNumber = model.Phone;
            res.FirstName = model.FirstName;
            res.SecondName = model.SecondName;


            string fileName = string.Empty;

            if (model.Photo != null)
            {
                var img = ImageWorker.FromBase64StringToImage(model.Photo);
                string randomFilename = Path.GetRandomFileName() + ".jpeg";
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads", randomFilename);
                img.Save(dir, ImageFormat.Jpeg);
                var oldImage = res.Photo;
                

                
                string fol = "\\uploads\\";
                string contentRootPath = _host.ContentRootPath + fol + oldImage;

                if (System.IO.File.Exists(contentRootPath))
                {
                    System.IO.File.Delete(contentRootPath);
                }
                    res.Photo = randomFilename;
                
            }
            _context.SaveChanges();

            //return Ok(new { message = "ok" });
            return Ok(new TokenResponceViewModel { token = _jwtTokenService.CreateToken(res) });
        }
    }
}
