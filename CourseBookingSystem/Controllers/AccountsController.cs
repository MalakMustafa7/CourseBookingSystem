using CouresBookingSystem.Core.Entities.Identity;
using CouresBookingSystem.Core.Services;
using CourseBookingSystem.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace CourseBookingSystem.Api.Controllers
{
     
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(UserManager<AppUser> userManager,
                                  ITokenService tokenService,
                                  SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            if(CheckEmailExists(model.Email).Result.Value) 
                return BadRequest("This Email Is Already Exist");
            var user = new AppUser() 
            { 
                Email = model.Email,
                FullName = model.FullName,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
                University = model.University,
            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if (!result.Succeeded) return BadRequest("SomeThing Went Wrong");
            var userDTO = new UserDTO()
            {
                FullName= user.FullName,
                University= user.University,
                Email= user.Email,
                Token = await _tokenService.GetTokenAsync(user, _userManager)
            };
            return Ok(userDTO);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user is null) return NotFound("The Email Is In Correct");
            var result =await _signInManager.CheckPasswordSignInAsync(user,model.Password,false);
            if(!result.Succeeded)  return Unauthorized();
            var userDTO = new UserDTO()
            {
                FullName = user.FullName,
                University = user.University,
                Email = user.Email,
                Token = await _tokenService.GetTokenAsync(user, _userManager)

            };
            return Ok(userDTO);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var userDTO = new UserDTO()
            {
                FullName = user.FullName,
                University = user.University,
                Email = user.Email,
                Token = await _tokenService.GetTokenAsync(user, _userManager)

            };
            return Ok(userDTO);

        }
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
