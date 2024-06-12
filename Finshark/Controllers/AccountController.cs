using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using Finshark.DTO;
using Finshark.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finshark;
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    public AccountController(UserManager<AppUser> UserManager, ITokenService TokenService)
    {
        _userManager = UserManager;
        _tokenService = TokenService;
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
}
