using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using Dapper;
using DapperTest.Models;

namespace DapperTest.Service
{
    public class SqlClientService
    {
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
        private SqlConnection _conn;

        public SqlClientService()
        {
            this._conn = new SqlConnection(_connStr);
        }

        public List<CustomerInfo> TestSqlQuery(DapperSqlTestRequest req)
        {
            var result = new List<CustomerInfo>();
            var id = req.CustomerId;
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = _conn;
                cmd.CommandText =
                    $"Select CustomerId, Username, IsApi from [Customer] WITH (NOLOCK) Where customerId < {id}";
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CustomerInfo customerInfo = new CustomerInfo()
                        {
                            CustomerId = int.Parse(reader["customerid"].ToString()),
                            Username = reader["Username"].ToString(),
                            IsApi = bool.Parse(reader["IsApi"].ToString())
                        };
                        result.Add(customerInfo);
                    }
                }
            }

            _conn.Close();

            return result;
        }

        public List<AccountDetail> GetAccountList(GetAccountListRequest req)
        {
            var result = new List<AccountDetail>();

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "[Coloris_Common_GetAccountList_1.3]";

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@Webid", req.WebId));
            cmd.Parameters.Add(new SqlParameter("@Username", req.Username));
            cmd.Parameters.Add(new SqlParameter("@OperatorId", req.operatorId));

            // execute the command
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // iterate through results, printing each to console
                while (reader.Read())
                {
                    AccountDetail accountDetail = new AccountDetail()
                    {
                        CustomerId = int.Parse(reader["customerid"].ToString()),
                        Username = reader["Username"].ToString(),
                        LoginName = reader["LoginName"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        AccountType = int.Parse(reader["AccountType"].ToString()),
                        Phone = reader["Phone"].ToString(),
                        Mobile = reader["Mobile"].ToString(),
                        Currency = reader["Currency"].ToString(),
                        IsCashPlayer = int.Parse(reader["IsCashPlayer"].ToString()) == 1,
                        IsSuspended = int.Parse(reader["IsSuspended"].ToString()) == 1,
                        IsClosed = int.Parse(reader["IsClosed"].ToString()) == 1,
                        IsLocked = int.Parse(reader["IsLocked"].ToString()) == 1,
                        ParentName = reader["ParentName"].ToString(),
                        IsDeposited = int.Parse(reader["IsDeposited"].ToString()) == 1,
                        LastLoginIP = reader["LastLoginIP"].ToString(),
                        LastLoginOn = DateTime.Parse(reader["LastLoginOn"].ToString()),
                        AbleToEdit = int.Parse(reader["AbleToEdit"].ToString()) == 1,
                        CurrentLevel = reader["CurrentLevel"].ToString(),
                        CreatedOn = DateTime.Parse(reader["CreatedOn"].ToString()),
                        Referral = reader["Referral"].ToString(),
                    };
                    result.Add(accountDetail);
                }

                _conn.Close();
            }

            return result;
        }

        public GetStockAccountListResponse GetStockAccountList(GetStockAccountListRequest req)
        {
            var result = new GetStockAccountListResponse();

            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }

            using (SqlCommand cmd = new SqlCommand())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandText = "[dbo].[Coloris_Stock_GetStockAccountList_1.2.0]";
                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _conn;
                    // 3. add parameter to command, which will be passed to the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@customerId", req.CustomerId));
                    cmd.Parameters.Add(new SqlParameter("@operatorId", req.OperatorId));
                    cmd.Parameters.Add(new SqlParameter("@rowCountPerPage", req.RowCountPerPage));
                    cmd.Parameters.Add(new SqlParameter("@page", req.Page));

                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        var SpResult = ds.Tables[0];
                        var dt1 = ds.Tables[1];
                        var dt2 = ds.Tables[2];

                        var errerCode = int.Parse(SpResult.Rows[0]["ErrorCode"].ToString());
                        if (errerCode == 0)
                        {
                            var r = dt1.Rows[0];
                            result.Downlines = int.Parse(r["Downlines"].ToString());
                            result.FirstName = r["FirstName"].ToString();
                            result.LastUpdate = DateTime.Parse( r["LastUpdate"].ToString() );
                            result.RegisterDate = DateTime.Parse(r["RegisterDate"].ToString());
                            result.TotalPlayer = int.Parse(r["TotalPlayer"].ToString());
                            result.UpdateBy = r["UpdateBy"].ToString();
                            result.Username = r["Username"].ToString();
                            result.StockAccountList = new List<StockAccount>();

                            foreach (DataRow r2 in dt2.Rows)
                            {
                                var stockAccount = new StockAccount()
                                {
                                    RowNumber = int.Parse(r2["RowNumber"].ToString()),
                                    CustomerId = int.Parse(r2["CustomerId"].ToString()),
                                    Currency = r2["Currency"].ToString(),
                                    Username = r2["Username"].ToString(),
                                    FirstName = r["FirstName"].ToString(),
                                    RegisterDate = DateTime.Parse(r["RegisterDate"].ToString()),
                                    Status = r["Status"].ToString(),
                                    LastUpdate = DateTime.Parse(r["LastUpdate"].ToString()),
                                    UpdateBy = r["UpdateBy"].ToString(),
                                    AccountType = (AccountType)int.Parse(r2["AccountType"].ToString()),
                                    Balance = decimal.Parse(r2["Balance"].ToString()),
                                    Downlines = int.Parse(r2["Downlines"].ToString()),
                                    MaxPage = int.Parse(r2["MaxPage"].ToString()),
                                    TotalPlayer = int.Parse(r2["TotalPlayer"].ToString()),
                                };
                                result.StockAccountList.Add(stockAccount);
                            }
                        }
                    }
                }

            }

            _conn.Close();
            return result;
        }
    }
}