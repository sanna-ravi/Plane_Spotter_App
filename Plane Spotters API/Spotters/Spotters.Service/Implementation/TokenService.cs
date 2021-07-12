using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Spotters.Service.Interface;
using Spotters.Service.ServiceModels;
using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Service.Implementation
{
    public class TokenService : ITokenService
    {
        protected IOptionsSnapshot<BearerTokensOptions> configuration;
        public TokenService(IOptionsSnapshot<BearerTokensOptions> configuration)
        {
            this.configuration = configuration;
            if (this.configuration == null)
            {
                throw new ArgumentNullException();
            }

        }

        public async Task<TokenViewModel> GenerateToken(String userId, String name, String role, String IPAddress)
        {
            String privateXml = Path.Combine(Directory.GetCurrentDirectory(), configuration.Value.RSAPrivateKey);

            try
            {
                using (RSA privateRsa = RSA.Create())
                {
                    var privateKeyXml = File.ReadAllText(privateXml);
                    privateRsa.FromXmlString(privateKeyXml);
                    var privateKey = new RsaSecurityKey(privateRsa);
                    SigningCredentials signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);

                    string issuer = configuration.Value.Issuer;

                    var utcNow = DateTime.UtcNow;

                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, issuer),
                        new Claim(JwtRegisteredClaimNames.Iss, issuer, ClaimValueTypes.String, issuer),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64, issuer),
                        new Claim(ClaimTypes.NameIdentifier, userId, ClaimValueTypes.String, issuer),
                        new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, issuer),
                        new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, issuer),
                        new Claim("ipaddress", IPAddress, ClaimValueTypes.String, issuer)
                    };

                    DateTime expireTime = utcNow.AddMinutes(this.configuration.Value.TokenExpirationMinutes);

                    var token = new JwtSecurityToken(issuer,
                      issuer, claims, utcNow,
                      expires: expireTime,
                      signingCredentials: signingCredentials);

                    String tokenVal = await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));

                    return new TokenViewModel()
                    {
                        TKD = userId,
                        Et = expireTime,
                        CdRe = tokenVal,
                        Rel = role
                    };
                }
            }
            catch
            {

            }
            return null;
        }
    }
}
