namespace DigiCV.Web.Models
{
    public enum ResponseTypes
    {
        Success,
        Danger,
        Warning
    }
    public class ResponseModel
    {
        public string? Message { get; set; }
        public ResponseTypes Type { get; set; }
    }
}
