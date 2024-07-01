using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OutOfOffice.Context;
using OutOfOffice.DTO.Requests;
using OutOfOffice.DTO.Responses;
using OutOfOffice.Entities;
using OutOfOffice.Exceptions;

namespace OutOfOffice.Services;

public class AuthService : IAuthService
{

    private readonly ApplicationContext _appContext;
    private readonly IConfiguration _configuration;
    
    public AuthService(ApplicationContext appContext, IConfiguration configuration)
    {
        _appContext = appContext;
        _configuration = configuration;
    }
    
    public async Task RegisterUser(RegisterRequest request)
    {
        // var user = await _appContext.Employees
        //     .FirstOrDefaultAsync(u => u.FullName == request.FullName);
        //
        // if (user != null)
        // {
        //     throw new UserExitsException("User with this name already exists", 401);
        // }
        
        Employee userToSave = new()
        {
            FullName = request.FullName,
            OutOfOfficeBalance = request.OufOfOfficeBalance,
        };
        
        PasswordHasher<Employee> passwordHasher = new();
        var hashedPassword = passwordHasher.HashPassword(userToSave, request.Password);
        
        userToSave.Password = hashedPassword;
        
        var subdivision = await _appContext.Subdivisions
            .FirstOrDefaultAsync(s => s.Id == request.SubdivisionId)
                          ?? throw new NotFoundException($"Subdivision not found with id {request.SubdivisionId}", 404);
        
        var position = await _appContext.Positions
            .FirstOrDefaultAsync(p => p.Id == request.PositionId)
                       ?? throw new NotFoundException($"Position not found with id {request.PositionId}", 404);
        
        var status = await _appContext.EmployeeStatuses
            .FirstOrDefaultAsync(s => s.Id == request.StatusId)
                     ?? throw new NotFoundException($"Status not found with id {request.StatusId}", 404);
        Employee peoplePartner = null;
        if (request.PeoplePartnerId != null)
        {
            peoplePartner = await _appContext.Employees
                .FirstOrDefaultAsync(e => e.Id == request.PeoplePartnerId)
                            ?? throw new NotFoundException($"People partner not found with id {request.PeoplePartnerId}", 404);
        }
        
        var roles = await _appContext.Roles
            .Where(r => request.RolesId.Contains(r.Id))
            .ToListAsync();
        if (roles.Count != request.RolesId.Length)
        {
            var ids = "[" + String.Join(",", request.RolesId.Where(r => !roles.Select(r1 => r1.Id).Contains(r)).ToArray()) + "]";
            throw new NotFoundException($"Role not found with id {ids}", 404);
        }
        
        foreach (var role in roles)
        {
            userToSave.Roles.Add(new EmployeeRole
            {
                Employee = userToSave,
                Role = role
            });
        }
        
        userToSave.Subdivision = subdivision;
        userToSave.Position = position;
        userToSave.Status = status;
        userToSave.PeoplePartner = peoplePartner;

        await _appContext.Employees.AddAsync(userToSave);
        await _appContext.SaveChangesAsync();
    }

    public async Task<LoginResponse> LoginUser(LoginRequest request)
    {
        Employee? user = await  _appContext.Employees
            .Include(e => e.Roles)
            .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(u => u.FullName == request.Name);
        
        if(user == null)
        {
            throw new NotFoundException("Invalid password or login", 401);
        }
        if(!new PasswordHasher<Employee>().VerifyHashedPassword(user, user.Password, request.Password).Equals(PasswordVerificationResult.Success))
        {
            throw new BadCredentialsException("Invalid password or login", 401);
        }
        
        return new LoginResponse
        {
            Token = GenerateJwtToken(user),
            RefreshToken = GenerateRefreshToken(user)
        };
    }

    public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
    {
        if (!ValidateRefreshToken(request))
        {
            throw new InvalidTokenException("Invalid refresh token", 401);
        }
        
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(request.RefreshToken);
        
        string name = token.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        
        Employee? user = await  _appContext.Employees
            .Include(e => e.Roles)
                .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(u => u.FullName == name);
        
        return new LoginResponse
        {
            Token = GenerateJwtToken(user),
            RefreshToken = GenerateRefreshToken(user)
        };
    }
    
    
    public bool ValidateRefreshToken(RefreshTokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(request.RefreshToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWT:RefIssuer"],
                ValidAudience = _configuration["JWT:RefAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:RefKey"]!))
            }, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public string GenerateJwtToken(Employee user)
    {
        
        var roles = user.Roles.Select(re => re.Role.RoleName).ToArray();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        Claim[] userClaims = claims.ToArray();
        
     
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        
        JwtSecurityToken token = new(
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            userClaims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GenerateRefreshToken(Employee user)
    {
        Claim[] userClaims = new[]
        {
            new Claim(ClaimTypes.Name, user.FullName),
        };
        
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JWT:RefKey"]!));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        
        JwtSecurityToken token = new(
            _configuration["JWT:RefIssuer"],
            _configuration["JWT:RefAudience"],
            userClaims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}

public interface IAuthService
{
 
    public Task RegisterUser(RegisterRequest request);
    
    public Task<LoginResponse> LoginUser(LoginRequest request);
    
    public Task<LoginResponse> RefreshToken(RefreshTokenRequest request);
    
}