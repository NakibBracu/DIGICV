using Autofac;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Web.CustomAttributes;
using DigiCV.Web.Service;
using System.ComponentModel.DataAnnotations;

namespace DigiCV.Web.Areas.Admin.Models;

public class TemplateCreateModel
{
    
    [Required]
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
    [MaxFileSize(900 * 1024, ErrorMessage = "The image size must be under 900 KB.")]
    public IFormFile Image { get; set; }
    public string? ImageName { get; set; }
    private IFileService _fileService;
    private ITemplateService _templateService;
    public TemplateCreateModel()
    {

    }
    public TemplateCreateModel(IFileService fileService, ITemplateService templateService)
    {
        _fileService = fileService;
        _templateService = templateService;
    }

    public void CreateTemplateName()
    {
        ImageName = _fileService.SaveFile(Image, "TemplateImage");
    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _fileService = scope.Resolve<IFileService>();
        _templateService= scope.Resolve<ITemplateService>();
    }

    internal void CreateTemplate()
    {
        if(!string.IsNullOrEmpty(ImageName)
            && (IsActive == true || IsActive == false)
            && !string.IsNullOrEmpty(Name))
        {
            _templateService.CreateTemplate(Name, ImageName, IsActive);
        }
    }



}
