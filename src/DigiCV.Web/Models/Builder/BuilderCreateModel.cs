using Autofac;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigiCV.Web.Service; 
using System.ComponentModel.DataAnnotations;
using DigiCV.Web.Service;
using Newtonsoft.Json;
using DigiCV.Web.Utilities;

namespace DigiCV.Web.Models.Builder;

public class BuilderCreateModel
{
    private IResumeService _resumeService;
    private ISkillService _skillService;
    private IFileService _fileService;

    public Guid? ResumeId { get; set; }
    public Guid UserId { get; set; }
    public Guid TemplateId { get; set; }
    public string Title { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "Name should contains atleast 3 characters")]
    [MaxLength(100, ErrorMessage = "Name can not have more than 100 characters")]
    [Display(Name ="Full Name")]
    public string FullName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage ="Enter valid email address")]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [MinLength(10, ErrorMessage = "Skype Length can not be less than 6 or greater than 32")]
    [MaxLength(37, ErrorMessage = "Skype Length can not be less than 6 or greater than 32")]
    [RegularExpression(@"^(live):\w+", ErrorMessage ="Enter valid Skype (live:username)")]
    public string Skype { get; set; }

    [MinLength(6, ErrorMessage = "LinkedIn url Min length 6 character")]
    [MaxLength(100, ErrorMessage = "LinkedIn Length can not be less than 6 or greater than 100")]
    [Url(ErrorMessage = "Enter LinkedIn url (https://www.linkedin.com/in/username)")]
    public string LinkdIn { get; set; }

    [Required]
    [MinLength(4, ErrorMessage = "Address Length can not be less than 4 or greater than 50")]
    [MaxLength(250, ErrorMessage = "Address Length can not be less than 6 or greater than 250")]
    public string Address { get; set; }

    [Required]
    [MinLength(20, ErrorMessage = "Summary Min length 20 character")]
    public string Summary { get; set; }
    public IFormFile Image { get; set; }
    public string? ImageName { get; set; }
    [Required]
    public IList<int> SkillsId { get; set; }
    [Required]
    public IList<string>? Trainings { get; set; }
    public IList<EducationModel> EducationModels { get; set; }
    public IList<ExperienceModel> ExperienceModels { get; set; }
    public IList<ProjectModel> ProjectModels { get; set; }
    public IList<ReferenceModel> ReferenceModels { get; set; }
    public IList<SelectListItem> SkillLitem = new List<SelectListItem>();

    public void ResolveDependency(ILifetimeScope scope)
    {
        _resumeService = scope.Resolve<IResumeService>();
        _skillService = scope.Resolve<ISkillService>();
        _fileService = scope.Resolve<IFileService>();
    }

    public async Task<Resume> CreateResume()
    {
        this.ImageName = await _fileService.SaveFileAsync(Image, "ResumeImage");

        Resume resume = new Resume()
        {
            UserId = this.UserId,
            ResumeTemplateId = this.TemplateId,
            Title = this.Title,
            FullName = this.FullName,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber,
            Skype = this.Skype,
            LinkedIn = this.LinkdIn,
            Address = this.Address,
            Summary = this.Summary,
            ImageName = this.ImageName,
            Trainings = this.Trainings
        };

        foreach (var experience in ExperienceModels)
        {
            var exe = new Experience()
            {
                Position = experience.Position,
                Companay = experience.Companay,
                JoiningDate = experience.JoiningDate,
                ResignationDate = experience.ResignationDate,
                Responsibilities = experience.Responsibilities
            };

            resume.Experiences.Add(exe);
        }

        foreach (var project in ProjectModels)
        {
            var pro = new Project()
            {
                Name = project.Name,
                Author = project.Author,
                Description = project.Description
            };

            resume.Projects.Add(pro);
        }

        foreach (var education in EducationModels)
        {
            var edu = new Education()
            {
                Level = education.Level,
                University = education.University,
                Grade = education.Grade,
                PassingYear = education.PassingYear
            };

            resume.Educations.Add(edu);
        }

        foreach (var reference in ReferenceModels)
        {
            var refe = new Reference()
            {
                Name = reference.Name,
                Designation = reference.Designation,
                Compnay = reference.Company,
                Address = reference.Address,
                Email = reference.Email,
                PhoneNumber = reference.PhoneNumber
            };

            resume.References.Add(refe);
        }

        foreach (var id in SkillsId)
        {
            var skill = _skillService.GetSkill(id);

            var resumeSkill = new ResumeSkill() { Skill = skill };

            resume.Skills.Add(resumeSkill);
        }

        ResumeId = resume.Id;
        
        return resume;
    }

    public async Task<Guid> InsertIntoDatabase()
    {
        Resume createdResume = await CreateResume();

        return await _resumeService.CreateResume(createdResume);
    }
}

public class EducationModel
{
    [Required]
    public string Level { get; set; }
    public string University { get; set; }
    [Required]
    [RegularExpression(@"[1-5]\.\d{1,2}$", ErrorMessage ="Enter a valid Grade point (like: 4.00)")]
    public string Grade { get; set; }
    public DateTime PassingYear { get; set; }
}

public class ExperienceModel
{
    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(150, ErrorMessage = "Max length 150 characters")]
    public string Position { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(150, ErrorMessage = "Max length 150 characters")]
    public string Companay { get; set; }
    [DateComparison(nameof(ResignationDate), ErrorMessage ="Joining Date must be greater than Resignation Date")]
    public DateTime JoiningDate { get; set; }

    public DateTime ResignationDate { get; set; }
    [Required]
    public IList<string>? Responsibilities { get; set; }
}

public class ProjectModel
{
    [Required]
    [MinLength(3, ErrorMessage = "Min length 3 character")]
    [MaxLength(150, ErrorMessage = "Max length 150 characters")]
    public string Name { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(100, ErrorMessage = "Max length 100 characters")]
    public string Author { get; set; }
    [Required]
    [MinLength(25, ErrorMessage = "Min length 25 character")]
    public string Description { get; set; }
}

public class ReferenceModel
{
    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(100, ErrorMessage = "Max length 100 characters")]
    public string Name { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(100, ErrorMessage = "Max length 100 characters")]
    public string Designation { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(150, ErrorMessage = "Max length 150 characters")]
    public string Company { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(250, ErrorMessage = "Max length 250 characters")]
    public string Address { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
}