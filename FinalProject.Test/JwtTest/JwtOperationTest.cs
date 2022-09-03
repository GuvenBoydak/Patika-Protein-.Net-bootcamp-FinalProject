using FinalProject.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Test
{
    public class JwtOperationTest
    {
        private readonly IConfiguration _configuration;
        private readonly JWTHelper _jwtHelper;

        public JwtOperationTest()
        {
            _configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            _jwtHelper = new JWTHelper(_configuration);
        }


        [Fact]
        public void CreateToken_MetotExecute_ReturnAccessToken()
        {
            List<Role> roles = new List<Role>()
            {
                new Role{ID=1 ,Name="Admin"}
            };
            AppUser appUser = new AppUser()
            {
                ID = 1,
                UserName = "guven",
                Email = "test@gmail.com"
            };
            //Token yaratıyoruz.
            AccessToken accessToken = _jwtHelper.CreateToken(appUser,roles);

            //Gelen deger AccessToken mi kontrol ediyoruz.
            Assert.IsAssignableFrom<AccessToken>(accessToken);
        }

        [Theory]
        [InlineData("12345test")]
        public void CreatePasswordHashAndVerifyPasswordHash_MetotExecute_IfPasswordIsCorrectReturnTrue(string password)
        {
            //Girdigimiz password ile passwordHash ve passwordSalt oluşturduk.
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //Gelen Degerleri Tipleri byte[] mi kontrol ettik
            Assert.IsAssignableFrom<byte[]>(passwordSalt);
            Assert.IsAssignableFrom<byte[]>(passwordHash);

            //Oluşturulan passwordHash ve passwordSalt vererek girdigimiz passwordu hashleyıp sonucu kontrol ettık.
            bool result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            Assert.True(result);
        }

        [Theory]
        [InlineData("12345test", "test12345")]
        public void CreatePasswordHashAndVerifyPasswordHash_MetotExecute_IfPasswordIsWrongReturnFalse(string truePassword, string falsePassword)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(truePassword, out passwordHash, out passwordSalt);

            Assert.IsAssignableFrom<byte[]>(passwordSalt);
            Assert.IsAssignableFrom<byte[]>(passwordHash);

            bool result = HashingHelper.VerifyPasswordHash(falsePassword, passwordHash, passwordSalt);
            Assert.False(result);
        }


        [Fact]
        public void CreateSecurityKey_MetotExecute_SouldBeReturnSymmetricSecurityKey()
        {
            //appsetting.json dan SecurtyKey degerini okuduk.
            string value = _configuration.GetSection("TokenOptions:SecurityKey").Value;


            SecurityKey key = SecurityKeyHelper.CreateSecurityKey(value);

            //Gelen degeri kontrol etik.
            Assert.IsAssignableFrom<SymmetricSecurityKey>(key);//SymmetricSecurityKey  SecurityKey den kalıtım aldıgı için tipler uyuşuyor.
        }

        [Fact]
        public void CreateSigninCredentials_MetotExecute_SouldBeReturnSigningCredentials()
        {
            string value = _configuration.GetSection("TokenOptions:SecurityKey").Value;

            SecurityKey key = SecurityKeyHelper.CreateSecurityKey(value);

            SigningCredentials result= SigningCredentialHelper.CreateSigninCredentials(key);

            Assert.IsAssignableFrom<SigningCredentials>(result);
        }


    }
}
