using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models
{
    public class UsersModel
    {
        public int pkiUserID { get; set; }
        public string? userName { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? userEmail { get; set; }
        public int? fkiRoleID { get; set; }
        public int? genderID { get; set; }
        public int? nationalityID { get; set; }
        public string? hashPassword { get; set; }
        public bool? isActive { get; set; }
        public bool? isBlocked { get; set; }
        public DateTime? dateCreated { get; set; }
        public DateTime? lastLogin { get; set; }
    }
}
