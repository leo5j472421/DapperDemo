using System;
using System.Collections.Generic;

namespace DapperTest.Models
{
    public class GetStockAccountListResponse
    {
        public List<StockAccount> StockAccountList { get; set; }
        public int Downlines { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public DateTime RegisterDate { get; set; }
        public int TotalPlayer { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UpdateBy { get; set; }

    }
}