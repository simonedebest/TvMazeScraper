namespace TvMazeScraper.Core.Model
{
    public class ErrorDetailsModel
    {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }
}