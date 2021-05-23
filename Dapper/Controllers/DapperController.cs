using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using DapperTest.Models;
using DapperTest.Service;

namespace DapperTest.Controllers
{
    [System.Web.Mvc.RoutePrefix("api/DapperController")]
    public class DapperController : ApiController
    {
        private DapperService dapper = new DapperService();
        private SqlClientService sqlClient = new SqlClientService();
        // GET: Dapper
        [HttpPost]
        [Route("Dapper/Test")]

        public ApiResponse<List<CustomerInfo>> DapperTest(DapperSqlTestRequest req)
        {
            return new ApiResponse<List<CustomerInfo>>(dapper.TestSqlQuery(req));
        }

        [HttpPost]
        [Route("Dapper/GetAccountList")]

        public ApiResponse<List<AccountDetail>> DapperSpCall(GetAccountListRequest req)
        {
            return new ApiResponse<List<AccountDetail>>(dapper.GetAccountList(req));
        }

        [HttpPost]
        [Route("Dapper/GetStockAccountList")]

        public ApiResponse<GetStockAccountListResponse> DapperGetStockAccount(GetStockAccountListRequest req)
        {
            return new ApiResponse<GetStockAccountListResponse>(dapper.GetStockAccountList(req));
        }


        [HttpPost]
        [Route("SqlClient/Test")]
        public ApiResponse<List<CustomerInfo>> SqlClientTest(DapperSqlTestRequest req)
        {
            return new ApiResponse<List<CustomerInfo>>(sqlClient.TestSqlQuery(req));
        }

        [HttpPost]
        [Route("SqlClient/GetAccountList")]

        public ApiResponse<List<AccountDetail>> SqlClientSpCall(GetAccountListRequest req)
        {
            return new ApiResponse<List<AccountDetail>>(sqlClient.GetAccountList(req));
        }

        [HttpPost]
        [Route("SqlClient/GetStockAccountList")]

        public ApiResponse<GetStockAccountListResponse> SqlGetStockAccount(GetStockAccountListRequest req)
        {
            return new ApiResponse<GetStockAccountListResponse>(sqlClient.GetStockAccountList(req));
        }


    }
}