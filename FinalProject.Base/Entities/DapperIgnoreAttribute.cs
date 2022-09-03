namespace FinalProject.Base
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class DapperIgnoreAttribute : Attribute 
    {
    }
}
