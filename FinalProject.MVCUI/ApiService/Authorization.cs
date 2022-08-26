using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public static class Authorization
    {
        public static HttpClient AuthorizationWithToken(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            return client;
        }
    }
}
