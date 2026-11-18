
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.DTOs.Auth;
using StudentManagement.API.Models;
using StudentManagement.API.Services;

namespace StudentManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly JwtService _jwtService;

		public AuthController(
			UserManager<ApplicationUser> userManager,
			JwtService jwtService)
		{
			_userManager = userManager;
			_jwtService = jwtService;
		}

		// Admin-only user registration
		[HttpPost("register")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Register(RegisterRequestDto request)
		{
			var existingUser = await _userManager.FindByEmailAsync(request.Email);

			if (existingUser != null)
			{
				return BadRequest(new
				{
					message = "Email is already registered."
				});
			}

			var user = new ApplicationUser
			{
				Name = request.Name,
				UserName = request.Email,
				Email = request.Email
			};

			var result = await _userManager.CreateAsync(
				user,
				request.Password
			);

			if (!result.Succeeded)
			{
				return BadRequest(new
				{
					errors = result.Errors.Select(error => error.Description)
				});
			}

			var roleResult = await _userManager.AddToRoleAsync(user, "User");

			if (!roleResult.Succeeded)
			{
				await _userManager.DeleteAsync(user);

				return BadRequest(new
				{
					errors = roleResult.Errors.Select(error => error.Description)
				});
			}

			return Ok(new
			{
				message = "User registered successfully."
			});
		}

		// User login
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginRequestDto request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user == null)
			{
				return Unauthorized(new
				{
					message = "Invalid email or password."
				});
			}

			var isPasswordValid = await _userManager.CheckPasswordAsync(
				user,
				request.Password
			);

			if (!isPasswordValid)
			{
				return Unauthorized(new
				{
					message = "Invalid email or password."
				});
			}

			var roles = await _userManager.GetRolesAsync(user);

			var role = roles.FirstOrDefault();

			if (role == null)
			{
				return Unauthorized(new
				{
					message = "User role is not assigned."
				});
			}

			var token = _jwtService.GenerateToken(user, role);

			var response = new AuthResponseDto
			{
				Token = token,
				UserId = user.Id,
				Name = user.Name,
				Email = user.Email ?? "",
				Role = role
			};

			return Ok(response);
		}
	}
}
