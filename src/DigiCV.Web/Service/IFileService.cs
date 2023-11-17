namespace DigiCV.Web.Service;

public interface IFileService
{
    string SaveFile(IFormFile file, string path);
    Task<string> SaveFileAsync(IFormFile file, string path);
    bool DeleteFile(string filepath);
}