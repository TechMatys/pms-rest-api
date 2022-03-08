

namespace PMS.Core.Model
{
    public class Account
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public int? Mobile { get; set; }
        public int? DateOfBirth { get; set; }

        public int? GenderId { get; set; }
         public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
