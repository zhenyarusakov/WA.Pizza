﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WA.Pizza.Core.Entities;
using WA.Pizza.Core.Entities.IdentityModels;
using WA.Pizza.Infrastructure.Abstractions;

namespace WA.Pizza.Infrastructure.Data.Services;

public class AuthenticateService: IAuthenticateService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateService(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
    
    public async Task<Response> RegisterAsync(RegisterModel model)
    {
        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName
        };
        
        var userExists = await _userManager.FindByNameAsync(model.UserName);

        if (userExists == null)
        {
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            
            return new Response
            {
                Status = "Success", 
                Message = "User created successfully!"
            };
        }
        else
        {
            return  new Response
            {
                Status = "Error", 
                Message = "User creation failed! Please check user details and try again."
            };   
        }
    }
    
    public async Task<Response> RegisterAdminAsync(RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.UserName);

        if (userExists != null)
        {
            return new Response
            {
                Status = "Error",
                Message = "User already exists!"
            };
        }
        
        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return new Response
            {
                Status = "Error",
                Message = "User creation failed! Please check user details and try again."
            };
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.Administrator))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator));
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }
        
        if (!await _roleManager.RoleExistsAsync(UserRoles.Moderator))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator));
        }
        
        if (await _roleManager.RoleExistsAsync(UserRoles.Administrator))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Administrator);
        }
        
        return new Response
        {
            Status = "Success", 
            Message = "User created successfully!"
        };
    }
    
    public async Task<TokenModel> LoginAsync(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new TokenModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        return new TokenModel
        {
            Message = $"Wrong login - {model.UserName} or password - {model.Password} "
        };
    }

    public async Task AddToRoleAsync(AddToRoleModel addToRoleModel)
    {
        var user = await _userManager.FindByEmailAsync(addToRoleModel.Email);

        if (user == null)
        {
            throw new InvalidOperationException($"User with email {addToRoleModel.Email} is not found.");
        }

        if (!UserRoles.ListofRoles.Contains(addToRoleModel.Role))
        {
            throw new InvalidOperationException($"Unsupported user role {addToRoleModel.Role}");
        }

        await _userManager.AddToRoleAsync(user, addToRoleModel.Role);
    }
}