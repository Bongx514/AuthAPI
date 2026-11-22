namespace AuthAPI.ViewModel
{
    public class vw_ActiveUsers
    {
        public int pkiUserID { get; set; }
        public string? userName { get; set; } = string.Empty;
        public string? firstName { get; set; } = string.Empty;
        public string? lastName { get; set; } = string.Empty;
        public string? userEmail { get; set; } = string.Empty;
        public int? fkiRoleID { get; set; }
        public string? txtRoleName { get; set; } = string.Empty;
        public int? genderID { get; set; }
        public string? txtGender { get; set; } = string.Empty;
        public bool? isActive { get; set; }
        public bool? isBlocked { get; set; }
        public DateTime? dateCreated { get; set; }
    }
}
