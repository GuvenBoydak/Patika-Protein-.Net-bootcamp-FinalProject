using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinalProject.Base
{
    public class JWTHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }

        private TokenOptions _tokenOptions;

        private DateTime _accessTokenExpiration;

        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            //Configuration.GetSection("TokenOptions") ile appsettings.json içerisindeki TokenOptions degerlerini Get<TokenOptions> sınıfındaki degerlere atıyoruz(Mapliyoruz).
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(AppUser appUser ,List<Role> roles)
        {
            //Token bitiş süresini 
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            //_tokenOptions.SecurityKey ile  TokenOptions icerisindeki SecurityKey'i veriyoruz ve byte array olarak bir SecurityKey oluşturuluyor.
            SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);

            //bir üstte oluştrulan securtiyKey veriyoruz ve bize bir creadentials oluşturuyor.
            SigningCredentials signingCredentials = SigningCredentialHelper.CreateSigninCredentials(securityKey);

            JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, appUser, signingCredentials,roles);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //jwt atadıgımız degerlerle bir jwt token oluşturuyoruz.
            string token = jwtSecurityTokenHandler.WriteToken(jwt);

            //AccessToken dönüyoruz.
            return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
        }

        //JWT oluşturdugumuz method. Parametre olarak verdigimiz TokenOptins,userName,id,signingCredentials  vererek bir JWT oluşturuyoruz.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, AppUser appUser , SigningCredentials signingCredentials, List<Role> roles)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //kulanıma başlıyacagı sureyi veriyoruz.
                claims: SetClaims(appUser,roles),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        //UserName, id ve kullanıcının rollerini claimslere ekliyoruz.
        private IEnumerable<Claim> SetClaims(AppUser appUser, List<Role> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.ID.ToString()),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim("AppUserId",appUser.ID.ToString())
            };

            foreach (Role item in roles)//Kulanıcının rollerini claimlere ekliyoruz.
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Name));
            }
            return claims;
        }
    }
}
