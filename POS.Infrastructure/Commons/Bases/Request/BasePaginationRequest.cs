namespace POS.Infrastructure.Commons.Bases.Request
{
    //hacemos la paginacion q nos sirve para los listados de datos
    public class BasePaginationRequest
    {
        public int NumPage { get; set; } = 1;
        public int NumRecordsPage { get; set; } = 10;
        private readonly int NumMaxRecordsPAge = 50;
        public string Order { get; set; } = "asc";
        public string? Sort { get; set; } = null;

        public int Records
        {
            get => NumRecordsPage;
            set
            {
                NumRecordsPage = value > NumRecordsPage ? NumRecordsPage : value;
            }
        }
    }
}
