using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(UserManager<AppUser> userManeger, SignInManager<AppUser> signInManager,
            AppEFContext context,IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _userManager = userManeger;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _context = context;

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

        [HttpPost]
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
        }

        [HttpGet]
        //[Authorize]
        [Route("users")]
        public async Task<IActionResult> Users()
        {
            var list = _context.Users.Select(x => _mapper.Map<UserItemViewModel>(x)).ToList();

            return Ok(list);
        }
    }
}
