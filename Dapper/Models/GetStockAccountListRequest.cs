namespace DapperTest.Models
{
    public class GetStockAccountListRequest
    {
        public int CustomerId { get; set; }
        public string SearchUsername { get; set; }
        public string Status { get; set; }
        public int RowCountPerPage { get; set; }
        public int Page { get; set; }
        public int OperatorId { get; set; }
        public int SimulateId { get; set; }
    }
}