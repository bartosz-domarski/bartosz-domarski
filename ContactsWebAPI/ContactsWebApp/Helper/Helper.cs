namespace ContactsWebApp.Helper
{
    public class ContactsAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5121/");
            return client;
        }
    }
}
