namespace VaquinhaOnline.Application.Contracts;

public interface IJwtService
{
    public JwtSecurityToken Generate(IEnumerable<Claim> claims, IConfiguration configuration);
    public List<Claim> GenerateAuthClaims(UserTokenDto user, IList<string> userRoles);
}