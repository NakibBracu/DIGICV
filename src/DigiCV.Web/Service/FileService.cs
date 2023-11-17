namespace DigiCV.Web.Service;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHost;

    public FileService(IWebHostEnvironment webHost)
    {
        _webHost = webHost;
    }

    public string SaveFile(IFormFile file, string path)
    {
        string fileName = string.Empty;
        var allowedImageExtensions = new string[] { ".jpg", ".png", ".jpeg" };
        var allowedPdfExtension = ".pdf";

        try
        {
            var rootPath = _webHost.WebRootPath;
            var imagePath = Path.Combine(rootPath, path);
            var pdfPath = Path.Combine(rootPath, "Pdf");

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            if (!Directory.Exists(pdfPath))
            {
                Directory.CreateDirectory(pdfPath);
            }

            // Check the allowed extensions
            var ext = Path.GetExtension(file.FileName);
            if (allowedPdfExtension.Contains(ext))
            {
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(pdfPath, newFileName);

                using (var stream = new FileStream(fileWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                fileName = newFileName;
            }
            else if (allowedImageExtensions.Contains(ext) && file.Length <= 900 * 1000)
            {
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(imagePath, newFileName);

                using (var stream = new FileStream(fileWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                fileName = newFileName;
            }

            return fileName;
        }
        catch (Exception ex)
        {
            throw new Exception("File Upload Failed");
        }
    }

    public async Task<string> SaveFileAsync(IFormFile file, string path)
    {
        string fileName = string.Empty;
        var allowedImageExtensions = new string[] { ".jpg", ".png", ".jpeg" };
        var allowedPdfExtension = ".pdf";

        try
        {
            var rootPath = _webHost.WebRootPath;
            var imagePath = Path.Combine(rootPath, path);
            var pdfPath = Path.Combine(rootPath, "Pdf");

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            if (!Directory.Exists(pdfPath))
            {
                Directory.CreateDirectory(pdfPath);
            }

            // Check the allowed extensions

            var ext = Path.GetExtension(file.FileName);
            if (allowedPdfExtension.Contains(ext))
            {
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(pdfPath, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
                fileName = newFileName;
            }
            else if (allowedImageExtensions.Contains(ext) && file.Length <= 300 * 1000)
            {
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(imagePath, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();
                fileName = newFileName;
            }


            return fileName;
        }
        catch (Exception ex)
        {
            throw new Exception("File Upload Failed");
        }
    }

    public bool DeleteFile(string filepath)
    {

        var rootPath = _webHost.WebRootPath;
        try
        {
            var path = Path.Combine(rootPath, filepath);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("File Delete Failed");
        }

    }
}