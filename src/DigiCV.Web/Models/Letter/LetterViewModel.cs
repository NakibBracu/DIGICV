using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure.Features.Services;
using DigiCV.Persistence.Features.Membership;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DigiCV.Web.Models.Letter
{
    public class LetterViewModel
    {
        private ICoverLetterService _coverLetterService;
        public IList<CoverLetter> Letters { get; set; }
        public CoverLetter Property { get; set; }
        public LetterViewModel()
        {
        }
        public LetterViewModel(ICoverLetterService coverLetterService)
        {
            _coverLetterService = coverLetterService;
        }
        internal void GetCoverLetter(Guid Id)
        {
            Property = _coverLetterService.GetCoverLetter(Id);
        }
        internal void DeleteCoverLetter(Guid id)
        {
            _coverLetterService.DeleteCoverLetter(id);
        }

    }
}
