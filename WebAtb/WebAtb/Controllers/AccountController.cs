using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAtb.Data.Entities.Identity;
using WebAtb.Model;

namespace WebAtb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManeger;

        public AccountController(UserManager<AppUser> userManeger, IMapper mapper)
        {
            _userManeger = userManeger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {

            return Ok(new {token="Ok"});
        }
    }
}
