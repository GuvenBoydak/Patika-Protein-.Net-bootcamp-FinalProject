using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinalProject.Base
{
    public class SecurityKeyHelper
    {
        //Kullandıgımız securityKey'i Asp Net jwt servislerinin anliyacagı hale getirmemiz gerekiyor bu yüzden string ifadeyi byte array formatına çeviriyoruz buradaki method sayesinde.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            //SymmetricSecurityKey ile Security  Key'in simetriğini alıyoruz.
            //Encoding.UTF8.GetBytes(securityKey) ile string ifadeyi byte array'e dönüştürür.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
