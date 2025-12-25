namespace FacebookClone.Core.Bases
{
    public class ErrorResponse
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public string? Data { get; set; }
    }
}
