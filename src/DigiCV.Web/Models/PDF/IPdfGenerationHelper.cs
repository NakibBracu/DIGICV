namespace DigiCV.Web.Models.PDF
{
    public interface IPdfGenerationHelper
    {
        byte[] GeneratePdf(PdfCreateModel createModel);
    }

}
