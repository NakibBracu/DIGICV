using DigiCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.Training.Services
{
    public interface ICoverLetterService
    {
        Guid CreateCoverLetter(CoverLetter entity);
        CoverLetter GetCoverLetter(Guid id);
        void UpdateCoverLetter(CoverLetter entity);
        void DeleteCoverLetter(Guid id);
        public IList<CoverLetter> GetCoverLetters();
        public IList<(Guid Id, string Title)> GetCoverLettersByUserId(Guid userId);
        Task<(IList<CoverLetter> records, int total, int totalDisplay)>
            GetPagedCoverLettersAsync(int pageIndex, int pageSize, string searchText, string orderBy);
        int GetTotalCoverLetterCount();
    }
}
