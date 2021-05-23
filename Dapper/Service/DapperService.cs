using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperTest.Models;

namespace DapperTest.Service
{
    public class DapperService
    {
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
        private readonly SqlConnection _conn;

        public DapperService()
        {
            _conn = new SqlConnection(_connStr);
        }

        public List<CustomerInfo> TestSqlQuery(DapperSqlTestRequest req)
        {
            string strSql = "Select CustomerId, Username, IsApi from [Customer] WITH (NOLOCK) Where customerId < @id";
            var result = _conn.Query<CustomerInfo>(strSql, new { id = req.CustomerId }).ToList();
            return result;
        }

        public List<AccountDetail> GetAccountList(GetAccountListRequest req)
        {
            return _conn.Query<AccountDetail>("[Coloris_Common_GetAccountList_1.3]", req,
                commandType: CommandType.StoredProcedure).ToList();

        }


        public GetStockAccountListResponse GetStockAccountList(GetStockAccountListRequest req)
        {
            var result = _conn.QueryMultiple("[Coloris_Stock_GetStockAccountList_1.2.0]", new
                {
                    req.CustomerId,
                    req.OperatorId,
                    req.RowCountPerPage,
                    req.Page
                },
                commandType: CommandType.StoredProcedure);
            var errorCode = result.Read<Error>().ToList().First();
            var stockAccount = result.Read<StockAccount>().ToList().First();
            var stockAccountList = result.Read<StockAccount>().ToList();

            return new GetStockAccountListResponse()
            {
                Downlines = stockAccount.Downlines,
                LastUpdate = stockAccount.LastUpdate,
                FirstName = stockAccount.FirstName,
                RegisterDate = stockAccount.RegisterDate,
                StockAccountList = stockAccountList,
                TotalPlayer = stockAccount.TotalPlayer,
                UpdateBy = stockAccount.UpdateBy,
                Username = stockAccount.Username
            };
        }
    }
}