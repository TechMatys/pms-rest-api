﻿

namespace PMS.Core.Model
{
    public class Account
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public int? Mobile { get; set; }
        public string? DateOfBirth { get; set; }

        public int? Gender { get; set; }
         public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
