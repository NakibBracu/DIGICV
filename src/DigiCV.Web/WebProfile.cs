using AutoMapper;
using DigiCV.Domain.Entities;
using DigiCV.Web.Models.Builder;

namespace DigiCV.Web
{
    public class WebProfile:Profile
    {
        public WebProfile()
        {
            CreateMap<UserProfile, UserProfile>().ReverseMap();
            CreateMap<Reference, ReferenceUpdateModel>().ReverseMap();
            CreateMap<Education, EducationUpdateModel>().ReverseMap();
            CreateMap<Experience, ExperienceUpdateModel>().ReverseMap();
            CreateMap<Project, ProjectUpdateModel>().ReverseMap();
            CreateMap<Resume, BuilderUpdateModel>()
                .ForMember(x => x.ResumeId, ac => ac.MapFrom(x => x.Id))
                .ForMember(x => x.TemplateId, ac => ac.MapFrom(x => x.ResumeTemplateId))
                .ForMember(x => x.Title, ac => ac.MapFrom<string>(x => x.Title))
                .ForMember(x => x.FullName, ac => ac.MapFrom<string>(x => x.FullName))
                .ForMember(x => x.Email, ac => ac.MapFrom<string>(x => x.Email))
                .ForMember(x => x.PhoneNumber, ac => ac.MapFrom<string>(x => x.PhoneNumber))
                .ForMember(x => x.Skype, ac => ac.MapFrom<string>(x => x.Skype))
                .ForMember(x => x.LinkdIn, ac => ac.MapFrom<string>(x => x.LinkedIn))
                .ForMember(x => x.Address, ac => ac.MapFrom<string>(x => x.Address))
                .ForMember(x => x.Summary, ac => ac.MapFrom<string>(x => x.Summary))
                .ForMember(x=>x.ImageName, ac =>ac.MapFrom(x=>x.ImageName))
                .ForMember(x => x.SkillsId, ac => ac.MapFrom(x => x.Skills.Select(x => x.SkillId)))
                .ForMember(x => x.Trainings, ac => ac.MapFrom(x => x.Trainings))
                .ForMember(x => x.EducationUpdateModels, ac => ac.MapFrom(x => x.Educations))
                .ForMember(x => x.ExperienceUpdateModels, ac => ac.MapFrom(x => x.Experiences))
                .ForMember(x => x.ProjectUpdateModels, ac => ac.MapFrom(x => x.Projects))
                .ForMember(x => x.ReferenceUpdateModels, ac => ac.MapFrom(x => x.References));
            
        }
    }
}
