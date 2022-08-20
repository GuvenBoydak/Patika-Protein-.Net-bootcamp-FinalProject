using FinalProject.Base;

namespace FinalProject.Test
{
    public class CheckPasswordTest
    {

        [Theory]
        [InlineData("test123","test123")]
        [InlineData("1234test", "1234test")]
        public void CheckingPassword_MetotExecute_IfNewPasswordWithConfirmPasswordIsCorrectReturnTrue(string newPassword,string confirmPassword)
        {
           bool result= CheckPassword.CheckingPassword(newPassword,confirmPassword);

            Assert.True(result);
        }

        [Theory]
        [InlineData("test12", "test123")]
        [InlineData("123test", "test123")]
        [InlineData("123test", "test13")]
        public void CheckingPassword_MetotExecute_IfNewPasswordWithConfirmPasswordIsCorrectReturnFalse(string newPassword, string confirmPassword)
        {
            bool result = CheckPassword.CheckingPassword(newPassword, confirmPassword);

            Assert.False(result);
        }
    }
}
