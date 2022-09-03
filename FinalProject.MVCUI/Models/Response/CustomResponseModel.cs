namespace FinalProject.MVCUI
{
    public class CustomResponseModel<T>
    {
        public T Data { get; set; }

        public int StatusCode { get; set; }

        public List<string> Error { get; set; }

        public string Message { get; set; }
    }
}
