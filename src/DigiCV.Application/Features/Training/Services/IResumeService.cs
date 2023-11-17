using DigiCV.Domain.Entities;

namespace DigiCV.Application.Features.Training.Services
{
    public interface IResumeService
    {
        Task<Guid> CreateResume(Resume entity);
        Task DeleteResume(Guid id);
        Task<Resume> GetResume(Guid id);
        Task<IList<Resume>> GetResumes();
        IList<(Guid Id, string Title)> GetResumesByUserId(Guid userId);
        Task<(IList<Resume> records, int total, int totalDisplay)> GetPagedResumesAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);
        Task UpdateResume(Resume resume);


        Task<string> GetTemplateNameAsync(Guid Id);

        Task<Resume> GetResume(Guid userId, string resumeTitle);

        int GetTotalResumeCount();

    }
}
