namespace AuthAPI.Models
{
    public class RegisterRequest
    {
        public string userName { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
