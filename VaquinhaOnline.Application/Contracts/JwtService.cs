using Microsoft.Extensions.Configuration;

namespace VaquinhaOnline.Application.Contracts;

public class JwtService : IJwtService
{
    public JwtSecurityToken Generate(IEnumerable<Claim> claims, IConfiguration config)
    {
        var jwtConfig = config.GetSection("JWTSettings");
        var key = jwtConfig["SecurityKey"] ?? throw new InvalidOperationException("Invalid security key");
        var privateKey = Encoding.UTF8.GetBytes(key);
        var signingCredentials = new SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["TokenValidityInMinutes"] ?? "0")),
            Audience = jwtConfig["ValidAudience"],
            Issuer = jwtConfig["ValidIssuer"],
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return token;
    }

    public List<Claim> GenerateAuthClaims(UserTokenDto user, IList<string> userRoles)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.PhoneNumber),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        authClaims.AddRange(userRoles.Select(userRoles => new Claim(ClaimTypes.Role, userRoles)));
        return authClaims;
    }
}
