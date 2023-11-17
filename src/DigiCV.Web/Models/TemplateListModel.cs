using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure;
using DigiCV.Infrastructure.Features.Services;

namespace DigiCV.Web.Models
{
    public class TemplateListModel
    {
        private readonly ITemplateService _templateService;
        public Guid? ResumeId { get; set; }
        public Guid? SelectedTemplateId { get; set; }
        public TemplateListModel()
        {

        }
        public TemplateListModel(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public IList<ResumeTemplate> GetPopularTemplates()
        {
            return _templateService.GetTemplates();
        }
        public IList<ResumeTemplate> GetTemplates()
        {
            return _templateService.GetTemplates();
        }

        public async Task<object> GetPagedResumeTemplatesAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _templateService.GetPagedResumeTemplatesAsync(
            dataTablesUtility.PageIndex,
            dataTablesUtility.PageSize,
            dataTablesUtility.SearchText,
            dataTablesUtility.GetSortText(new string[] { "Name", "ImageName", "IsActive" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.ImageName,
                                record.IsActive.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(Guid id)
        {
            _templateService.DeleteTemplate(id);
        }
    }
}
