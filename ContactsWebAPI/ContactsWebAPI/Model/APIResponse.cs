namespace ContactsWebAPI.Model
{
    public class APIResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object? ResponseData { get; set; }
    }

    public enum ResponseType
    {
        Success,
        NotFound,
        Failure
    }
}
