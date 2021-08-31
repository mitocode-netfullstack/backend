namespace MitoCode.Dto.Request
{
    public class BaseRequest
    {
        public string Filter { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }

        public BaseRequest(string filter, int page, int rows)
        {
            Filter = filter;
            Page = page;
            Rows = rows;
        }
    }
}