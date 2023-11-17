using DigiCV.Application;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;

namespace DigiCV.Infrastructure.Features.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public ResumeService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateResume(Resume entity)
        {
            await _unitOfWork.Resumes.AddAsync(entity);
            await _unitOfWork.SaveAsync();

            // Retrieve the generated Guid from the entity
            Guid newResumeId = entity.Id; // Assuming the Id property of the entity is named "Id" and is of type Guid

            return newResumeId;
        }

        public async Task DeleteResume(Guid id)
        {
            await _unitOfWork.Resumes.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Resume> GetResume(Guid id)
        {
            return await _unitOfWork.Resumes.GetByIdAsync(id);
        }

        public async Task<IList<Resume>> GetResumes()
        {
            return await _unitOfWork.Resumes.GetAllAsync();
        }
        public async Task<(IList<Resume> records, int total, int totalDisplay)> GetPagedResumesAsync(int pageIndex,
            int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.Resumes.GetTableDataAsync(
                x => x.FullName.Contains(searchText), orderBy, pageIndex, pageSize);
            return result;
        }

        public async Task UpdateResume(Resume resume)
        {
            _unitOfWork.Resumes.Edit(resume);
            await _unitOfWork.SaveAsync();
        }


        public async Task<string> GetTemplateNameAsync(Guid Id)
        {
            var template = await _unitOfWork.ResumeTemplates.GetByIdAsync(Id);
            return template.Name;
        }
        public async Task<Resume> GetResume(Guid userId, string resumeTitle)
        {
            var title = resumeTitle.Replace('-', ' ').ToLower();

            return await _unitOfWork.Resumes.GetByResumeTitleAsync(userId, title);
        }

        public IList<(Guid Id, string Title)> GetResumesByUserId(Guid userId)
        {
            return  _unitOfWork.Resumes.GetResumeByUserId(userId);
        }

        public int GetTotalResumeCount()
        {
            var totalResume = _unitOfWork.Resumes.GetCount();
            return totalResume;
        }
    }
}
