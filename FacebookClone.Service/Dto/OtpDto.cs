namespace FacebookClone.Service.Dto
{
    public class OtpDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
