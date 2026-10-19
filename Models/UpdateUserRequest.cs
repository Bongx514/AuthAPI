namespace AuthAPI.Models
{
    public class UpdateUserRequest
    {
        public string? userName { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? userEmail { get; set; }
        public int? fkiRoleID { get; set; }
    }
}
