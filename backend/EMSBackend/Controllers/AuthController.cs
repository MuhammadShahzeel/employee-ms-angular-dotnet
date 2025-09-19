using EMSBackend.Dtos;
using EMSBackend.Interfaces;
using EMSBackend.Mappers;
using EMSBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [HttpPost("register")]


        public async Task<IActionResult> Login([FromBody] AuthDto authDto)
        {

            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = authDto.ToAppUser();
                var createdUser = await _userManager.CreateAsync(appUser, authDto.Password); // User create kiya



                if (createdUser.Succeeded)
                {
                    //var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); // "User" role assign kiya

                    //if (roleResult.Succeeded)
                    //{
                    var token = _tokenService.CreateToken(appUser); // JWT token banaya
                    var newUserDto = appUser.ToNewUserDto(token);   // DTO banaya response ke liye
                    return Ok(newUserDto);                          // 200 OK response with token
                                                                    //}
                                                                    //else
                                                                    //{
                                                                    //    return StatusCode(500, roleResult.Errors); // Role assign mein error
                                                                    //}
                }
                else
                {
                    return StatusCode(500, createdUser.Errors); // User create nahi ho saka
                }
            }

            catch (Exception e)
            {
                return StatusCode(500, e); // Unexpected error handle
            }

        }

    }
}

