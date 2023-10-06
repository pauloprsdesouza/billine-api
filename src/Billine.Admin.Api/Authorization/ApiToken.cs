using Billine.Admin.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Billine.Admin.Api.Authorization
{
    public class ApiToken
    {
        private readonly JwtOptions _jwtOptions;

        public User User { get; set; }

        public ApiToken(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public override string ToString()
        {
            List<Claim> claims = new()
            {
                new Claim("UserId", User.Id.ToString()),
                new Claim("UserEmail", User.Email)
            };

            // foreach (var role in User.Roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, role));
            // }

            JwtSecurityToken token = new(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret)),
                    algorithm: SecurityAlgorithms.HmacSha256
                ),
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
