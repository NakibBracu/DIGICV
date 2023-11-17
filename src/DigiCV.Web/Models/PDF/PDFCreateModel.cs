using Microsoft.AspNetCore.Mvc;

namespace DigiCV.Web.Models.PDF
{
    public class PdfCreateModel
    {
        public string ViewName { get; set; }
        public object Model { get; set; }

        public Controller Controller { get; set; }

        public PdfCreateModel() { }
    }

}
