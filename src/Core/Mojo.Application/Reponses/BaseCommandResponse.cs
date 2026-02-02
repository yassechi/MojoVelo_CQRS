namespace Mojo.Application.Reponses
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public string StrId { get; set; } = string.Empty;
    }
}