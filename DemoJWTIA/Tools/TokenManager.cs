using Demo_ASP_MVC_06_Session.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoJWTIA.Tools
{
    public class TokenManager
    {
        private string _secret;

        public TokenManager(IConfiguration config)
        {
            _secret = config.GetSection("JwtInfo").GetSection("secret").Value;
        }
        public string GenerateToken(Member member)
        {
            // creer la signing key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            SigningCredentials credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Création du payload / claims
            Claim[] myClaims = new[]
            {
                new Claim("MemberId", member.MemberId.ToString()),
                new Claim(ClaimTypes.GivenName, member.Pseudo), // gestion de rôle, ajouter un role -> new claim
            };

            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims : myClaims,
                signingCredentials: credential,
                expires : DateTime.Now.AddDays(1)
                );
            // Génération du token

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(securityToken);
        }
    }
}
