
using DigiCV.Application.Features.Training.Services;
using DigiCV.Infrastructure;
using System.Web;

namespace DigiCV.Web.Areas.Admin.Models
{
    public class UserListModel
    {

        private readonly IUserService _userService;
        public UserListModel()
        {
        }
        public UserListModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<object> GetPagedUsersAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _userService.GetPagedUsersAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.SearchText,
                dataTablesUtility.GetSortText(new string[] {"Id", "ImageUrl", "FullName", "ClaimType", "IsActive" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Id.ToString(),
                                record.ImageUrl,
                                HttpUtility.HtmlEncode(record.FullName),
                                record.ClaimType,
                                record.IsActive.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}