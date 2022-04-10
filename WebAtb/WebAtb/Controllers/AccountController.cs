using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
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

        public AccountController(UserManager<AppUser> userManeger, IMapper mapper, IJwtTokenService jwtTokenService)
        {
            _userManager = userManeger;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            

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
    }
}
