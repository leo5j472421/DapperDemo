using System;
using Newtonsoft.Json;

namespace DapperTest.Models
{
    public class StockAccount
    {
        // for downline account
        public int MaxPage { get; set; }
        public int RowNumber { get; set; }
        public int CustomerId { get; set; }
        public string Currency { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UpdateBy { get; set; }
        [JsonIgnore]
        public AccountType AccountType { get; set; }
        [JsonProperty("AccountType")]
        public string _accountType => AccountType.ToString();
        // for main account
        public int Downlines { get; set; }
        public int TotalPlayer { get; set; }
        public decimal Balance { get; set; }


    }
}