using DigiCV.Web.Service;
using DinkToPdf;
using DinkToPdf.Contracts;
using HtmlAgilityPack;

namespace DigiCV.Web.Models.PDF
{
    public class PdfGenerationHelper : IPdfGenerationHelper
    {
        private readonly IViewRenderer _viewRenderer;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConverter _pdfConverter;
        // Add any other dependencies needed

        public PdfGenerationHelper(IViewRenderer viewRenderer, IWebHostEnvironment hostingEnvironment,
            IConverter pdfConverter
            )
        {
            _viewRenderer = viewRenderer;
            _hostingEnvironment = hostingEnvironment;
            _pdfConverter = pdfConverter;
        }

        public byte[] GeneratePdf(PdfCreateModel createModel)
        {
            var htmlContent = _viewRenderer.RenderViewToStringAsync(createModel.Controller, createModel.ViewName, createModel.Model).Result;
            // Modify the htmlContent as needed
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Find all elements with src attribute starting with '/'
            var elementsWithSrc = htmlDocument.DocumentNode.SelectNodes("//*[@src[starts-with(., '/')]]");
            if (elementsWithSrc != null)
            {
                foreach (var element in elementsWithSrc)
                {
                    var oldSrc = element.GetAttributeValue("src", "");
                    var newSrc = _hostingEnvironment.WebRootPath + oldSrc;

                    // Update the src attribute with the new path
                    element.SetAttributeValue("src", newSrc);
                }

                // Get the updated HTML content
                htmlContent = htmlDocument.DocumentNode.OuterHtml;
            }

            var globalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.A4, // Change to A3
                Orientation = Orientation.Portrait,
                Margins = new MarginSettings
                {
                    Top = 8,
                    Bottom = 8,
                    Left = 8,
                    Right = 8
                }
            };

            var pdf = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8"},
            };

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = { pdf }
            };

            return _pdfConverter.Convert(doc);
        }
    }

}
