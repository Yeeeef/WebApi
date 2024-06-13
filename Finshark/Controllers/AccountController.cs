using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using Finshark.DTO;
using Finshark.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Finshark;
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> UserManager, ITokenService TokenService, SignInManager<AppUser> SignInManager)
    {
        _userManager = UserManager;
        _tokenService = TokenService;
        _signInManager = SignInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();

            var appUser = new AppUser 
            { UserName = registerDTO.UserName ,
             Email = registerDTO.EmailAddress
            };
            var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok
                    (
                        new NewUserDTO
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        }
                    );
                }
                else
                {
                return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }
        var user = await _userManager.Users.FirstOrDefaultAsync<AppUser>(x => x.UserName == loginDTO.UserName);
        if (user == null)
        {
            return Unauthorized("Invalid Account Information");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid Account Information");
        }
        return Ok(
            new NewUserDTO
            {
                UserName = loginDTO.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            }
        );
    }
}
