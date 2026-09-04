using LibraryApp.Model;
using LibraryApp.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LibraryApp.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterVM request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Conflict(new { error = "Email is already registered." });
        if (await _userManager.FindByNameAsync(request.UserName) is not null)
            return Conflict(new { error = "Username is already registered." });
        if (await _userManager.Users.AnyAsync(user => user.PhoneNumber == request.Phone))
            return Conflict(new { error = "Phone number is already registered." });

        var user = new AppUser
        {
            UserName = request.UserName.Trim(),
            Email = request.Email.Trim(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            PhoneNumber = request.Phone.Trim()
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(error => error.Description) });

        await _signInManager.SignInAsync(user, isPersistent: false);
        return CreatedAtAction(nameof(Me), new { }, new { user.Id, user.UserName, user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVm request)
    {
        var value = request.Login.Trim();
        var user = await _userManager.FindByEmailAsync(value)
            ?? await _userManager.FindByNameAsync(value)
            ?? await _userManager.Users.FirstOrDefaultAsync(item => item.PhoneNumber == value);
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized(new { error = "Invalid credentials." });

        await _signInManager.SignInAsync(user, request.RememberMe);
        return Ok(new { user.Id, user.UserName, user.Email, user.FirstName, user.LastName });
    }

    [Authorize, HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }

    [Authorize, HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var user = await _userManager.GetUserAsync(User);
        return user is null
            ? Unauthorized()
            : Ok(new { user.Id, user.UserName, user.Email, user.FirstName, user.LastName, user.PhoneNumber });
    }
}
