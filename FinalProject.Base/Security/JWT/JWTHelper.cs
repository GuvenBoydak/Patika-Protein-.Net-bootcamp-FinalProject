using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinalProject.Base
{
    public class JWTHelper : ITokenHelper
    {
        public IConfiguration Configuration { get;}

        private TokenOptions _tokenOptions;

        private DateTime _accessTokenExpiration;

        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            //Configuration.GetSection("TokenOptions") ile appsettings.json içerisindeki TokenOptions degerlerini Get<TokenOptions> sınıfındaki degerlere atıyoruz(Mapliyoruz).
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(string userName, int id)
        {
            //Token bitiş süresini 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            //_tokenOptions.SecurityKey ile  TokenOptions icerisindeki SecurityKey'i veriyoruz ve byte array olarak bir SecurityKey oluşturuluyor.
            SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);

            //bir üstte oluştrulan securtiyKey veriyoruz ve bize bir creadentials oluşturuyor.
            SigningCredentials signingCredentials = SigningCredentialHelper.CreateSigninCredentials(securityKey);

            JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions,userName,id, signingCredentials);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //jwt atadıgımız degerlerle bir jwt token oluşturuyoruz.
            string token = jwtSecurityTokenHandler.WriteToken(jwt);

            //AccessToken dönüyoruz.
            return new AccessToken { Token=token, Expiration=_accessTokenExpiration};
        }

        //JWT oluşturdugumuz method. Parametre olarak verdigimiz TokenOptins,userName,id,signingCredentials  vererek bir JWT oluşturuyoruz.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, string userName, int id, SigningCredentials signingCredentials)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //kulanıma başlıcagı sureyi veriyoruz.
                claims: GetClaim(userName,id),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        //UserName ve id parametresini claimslere ekliyoruz.
        private static Claim[] GetClaim(string userName, int id)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim("AppUserId", id.ToString()),
            };

            return claims;
        }
    }
}
