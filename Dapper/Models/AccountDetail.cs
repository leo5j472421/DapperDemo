using System;

namespace DapperTest.Models
{
    public class AccountDetail
    {
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountType { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Currency { get; set; }
        public bool IsCashPlayer { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsClosed { get; set; }
        public bool IsLocked { get; set; }
        public string ParentName { get; set; }
        public bool IsDeposited { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime LastLoginOn { get; set; }
        public bool AbleToEdit { get; set; }
        public string CurrentLevel { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Referral { get; set; }
    }
}