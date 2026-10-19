namespace AuthAPI.ViewModel
{
    public class vw_ActiveUsers
    {
        public int pkiUserID { get; set; }
        public int? fkiRoleID { get; set; }
        public string? roleName { get; set; } = string.Empty;
        public string? firstName { get; set; } = string.Empty;
        public string? lastName { get; set; } = string.Empty;
        public string? userName { get; set; } = string.Empty;
        public string? userEmail { get; set; } = string.Empty;
        public string? genderName { get; set; } = string.Empty;
        public string? nationalityName { get; set; } = string.Empty;
        public string? phoneNumber { get; set; } = string.Empty;
        public string? tellNumber { get; set; }
        public string? physicalAddress { get; set; }
        public string? postalCode { get; set; }
        public bool? isActive { get; set; }
        public bool? isBlocked { get; set; }
        public DateTime? dateCreated { get; set; }
    }
}
