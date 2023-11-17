using Autofac;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigiCV.Web.Service; 
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using DigiCV.Web.Utilities;
using AutoMapper;
using Humanizer;

namespace DigiCV.Web.Models.Builder;

public class BuilderUpdateModel
{
    private IResumeService _resumeService;
    private ISkillService _skillService;
    private IFileService _fileService;
    private ITemplateService _templateService;
    private IMapper _mapper;

    public Guid ResumeId { get; set; }
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
    [Url(ErrorMessage = "Enter LinkedIn url")]
    public string LinkdIn { get; set; }

    [Required]
    [MinLength(4, ErrorMessage = "Address Length can not be less than 4 or greater than 50")]
    [MaxLength(250, ErrorMessage = "Address Length can not be less than 6 or greater than 250")]
    public string Address { get; set; }

    [Required]
    [MinLength(20, ErrorMessage = "Summary Min length 20 character")]
    public string Summary { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImageName { get; set; }
    [Required]
    public IList<int> SkillsId { get; set; }
    [Required]
    public IList<string>? Trainings { get; set; }
    public IList<EducationUpdateModel> EducationUpdateModels { get; set; }
    public IList<ExperienceUpdateModel> ExperienceUpdateModels { get; set; }
    public IList<ProjectUpdateModel> ProjectUpdateModels { get; set; }
    public IList<ReferenceUpdateModel> ReferenceUpdateModels { get; set; }
    public IList<SelectListItem> SkillItems = new List<SelectListItem>();
    public BuilderUpdateModel()
    {

    }
    public BuilderUpdateModel(
        IResumeService resumeService,
        ISkillService skillService,
        IFileService fileService,
        IMapper mapper, 
        ITemplateService templateService)
    {
        _resumeService = resumeService;
        _skillService = skillService;
        _fileService = fileService;
        _mapper = mapper;
        _templateService = templateService;
    }
    public async Task LoadData(Guid resumeId)
    {
        var resume = await _resumeService.GetResume(resumeId);
        _mapper.Map(resume, this);
    }
    
    public void ResolveDependency(ILifetimeScope scope)
    {
        _resumeService = scope.Resolve<IResumeService>();
        _skillService = scope.Resolve<ISkillService>();
        _fileService = scope.Resolve<IFileService>();
        _mapper = scope.Resolve<IMapper>();
        _templateService = scope.Resolve<ITemplateService>();
    }
    public async Task<bool> ChangeResumeTemplate()
    {
        var resume =await _resumeService.GetResume(ResumeId);
        var template = _templateService.GetTemplateById(TemplateId);
        if(resume!=null && resume.UserId == UserId && template !=null)
        {
            resume.ResumeTemplateId = template.Id;
            resume.ResumeTemplate = template;
            await _resumeService.UpdateResume(resume);
            return true;

        }else
        {
            return false;
        }

    }
    public async Task<bool> UpdateResume()
    {

        
        Resume resume = await _resumeService.GetResume(ResumeId);

        if(resume == null|| resume.UserId != UserId) 
        {
            return false;

        }
        if (Image != null)
        {
            this.ImageName = await _fileService.SaveFileAsync(Image, "ResumeImage");
            if (resume.ImageName != null)
            _fileService.DeleteFile(Path.Combine("ResumeImage", resume.ImageName));
        }

        resume.ResumeTemplateId = this.TemplateId;
        resume.Title = this.Title;
        resume.FullName = this.FullName;
        resume.Email = this.Email;
        resume.PhoneNumber = this.PhoneNumber;
        resume.Skype = this.Skype;
        resume.LinkedIn = this.LinkdIn;
        resume.Address = this.Address;
        resume.Summary = this.Summary;
        resume.ImageName = this.ImageName;
        resume.Trainings = this.Trainings;

        
        foreach (var experience in ExperienceUpdateModels)
        {
            var existingExperience = resume.Experiences.FirstOrDefault(x=>x.Id==experience.Id);
            if (existingExperience != null)
            {
                _mapper.Map(experience, existingExperience);
            }
            else
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
            
        }
        var experiencesToRemove = resume.Experiences.Where(existingItem => !ExperienceUpdateModels.Any(newItem => Convert.ToInt32(newItem.Id) == existingItem.Id)).ToList();
        foreach (var experienceToRemove in experiencesToRemove)
        {
            resume.Experiences.Remove(experienceToRemove);
        }
        foreach (var project in ProjectUpdateModels)
        {
            var existingProject = resume.Projects.FirstOrDefault(x=>x.Id == project.Id);
            if (existingProject != null)
            {
                _mapper.Map(project, existingProject);
            }
            else
            {
                var pro = new Project()
                {
                    Name = project.Name,
                    Author = project.Author,
                    Description = project.Description
                };
                resume.Projects.Add(pro);
            }
        }
        var projectsToRemove = resume.Projects.Where(existingItem => !ProjectUpdateModels.Any(newItem => Convert.ToInt32(newItem.Id) == existingItem.Id)).ToList();
        foreach (var projectToRemove in projectsToRemove)
        {
            resume.Projects.Remove(projectToRemove);
        }
        foreach (var education in EducationUpdateModels)
        {
            var existingEducation = resume.Educations.FirstOrDefault(x=>x.Id==education.Id);
            if (existingEducation != null)
            {
                _mapper.Map(education, existingEducation);
            }
            else
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
            

        }
        var educationsToRemove = resume.Educations.Where(existingItem => !EducationUpdateModels.Any(newItem => Convert.ToInt32(newItem.Id) == existingItem.Id)).ToList();
        foreach (var educationToRemove in educationsToRemove)
        {
            resume.Educations.Remove(educationToRemove);
        }
        foreach (var reference in ReferenceUpdateModels)
        {
            var existingRefernce = resume.References.FirstOrDefault(x => x.Id == reference.Id);
            if(existingRefernce != null)
            {
                _mapper.Map(reference, existingRefernce);
            }
            else
            {
                var refe = new Reference()
                {
                    Name = reference.Name,
                    Designation = reference.Designation,
                    Compnay = reference.Compnay,
                    Address = reference.Address,
                    Email = reference.Email,
                    PhoneNumber = reference.PhoneNumber
                };
                resume.References.Add(refe);
            }
        }
        var referencesToRemove = resume.References.Where(existingItem=>!ReferenceUpdateModels.Any(newItem=>Convert.ToInt32(newItem.Id) ==existingItem.Id)).ToList();
        foreach(var referenceToRemove in referencesToRemove)
        {
            resume.References.Remove(referenceToRemove);
        }

        foreach (var id in SkillsId)
        {

            var skill = _skillService.GetSkill(id);

            var resumeSkill = new ResumeSkill() { Skill = skill };
            var oldSkill = resume.Skills?.FirstOrDefault(x=>x.SkillId == id);
            if (oldSkill == null)
            {
                resume.Skills.Add(resumeSkill);
            }          
        }
        var skillsToRemove = resume.Skills.Where(existingItem => !SkillsId.Any(newItem => newItem == existingItem.Skill.Id));
        foreach(var skillToRemove in skillsToRemove.ToList())
        {
            resume.Skills.Remove(skillToRemove);
        }
        //ResumeId = resume.Id;

        await _resumeService.UpdateResume(resume);
        return true;
    }
}


public class EducationUpdateModel
{
    public int? Id { get; set; }
    [Required]
    public string Level { get; set; }
    public string University { get; set; }
    [Required]
    [RegularExpression(@"[1-5]\.\d{1,2}$", ErrorMessage = "Enter a valid Grade point (like: 4.00)")]
    public string Grade { get; set; }
    public DateTime PassingYear { get; set; }
}

public class ExperienceUpdateModel
{
    public int? Id { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(150, ErrorMessage = "Max length 150 characters")]
    public string Position { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(150, ErrorMessage = "Max length 150 characters")]
    [Display(Name ="Company")]
    public string Companay { get; set; }
    [DateComparison(nameof(ResignationDate), ErrorMessage = "Joining Date must be greater than Resignation Date")]
    public DateTime JoiningDate { get; set; }

    public DateTime ResignationDate { get; set; }
    [Required]
    public IList<string>? Responsibilities { get; set; }
}

public class ProjectUpdateModel
{
    public int? Id { get; set; }
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

public class ReferenceUpdateModel
{
    public int? Id { get; set; }
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
    [Display(Name = "Company")]
    public string Compnay { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Min length 2 character")]
    [MaxLength(250, ErrorMessage = "Max length 250 characters")]
    public string Address { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
}