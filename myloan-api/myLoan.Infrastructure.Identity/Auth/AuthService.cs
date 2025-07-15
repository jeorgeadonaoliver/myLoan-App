using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using myLoan.Application.Interface.Auth;
using myLoan.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace myLoan.Infrastructure.Identity.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task<Result<(bool, ApplicationUser user)>> GetUserCredentials(string email)
    {
        var user = await _userManager.FindByEmailAsync(email ?? "");
        if(user is null)
            return Result.Ok((false, new ApplicationUser()));

        return Result.Ok((true, user));
    }

    private async Task<Result<bool>> IsPasswordValid(ApplicationUser user, string password)
    {
        var result = await _userManager.CheckPasswordAsync(user, password);
        return Result.Ok(result);
    }

    public async Task<Result<string>> LoginAsync(string email, string password)
    {
       
        var userCredentials = await GetUserCredentials(email ?? "");
        var passwordCheck = await IsPasswordValid(userCredentials.Value.user, password);

        if (userCredentials.Value.Item1 == false || passwordCheck.Value == false) 
        {
            return Result.Fail("Invalid email or password");
        }

        var token = GenerateJwtToken(userCredentials.Value.user);
        return Result.Ok(token);
    }

    public async Task<Result<string>> RegisterAsync(ApplicationUser cmd)
    {
        var result = await _userManager.CreateAsync(cmd, cmd.PasswordHash ?? "");

        if (!result.Succeeded) 
        {
            return Result.Fail($"Error on registerring user! {string.Join("; ", result.Errors.Select(e => e.Description))}");
        }

        var resultRole = await _userManager.AddToRoleAsync(cmd, "User");
        var token = GenerateJwtToken(cmd);
        return Result.Ok(token);
    }
    private string GenerateJwtToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"] ?? "");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Name, $"{user.LastName}, {user.FirstName}" ),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email?? ""),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("uid", user.Id)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _config["JwtSettings:Issuer"],
            Audience = _config["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
