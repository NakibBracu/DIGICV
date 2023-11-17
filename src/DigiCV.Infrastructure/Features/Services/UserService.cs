using DigiCV.Application;
using DigiCV.Application.Features.DTOs;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DigiCV.Infrastructure.Features.Services
{
    public class UserService : IUserService
    {
        private readonly IAdoNetUtility _adoNetUtility;
        private readonly IApplicationUnitOfWork _unitOfWork;

        public UserService(IAdoNetUtility adoNetUtility, IApplicationUnitOfWork unitOfWork)
        {
            _adoNetUtility = adoNetUtility;
            _unitOfWork = unitOfWork;
        }

        public async Task<(IList<UserDTO> records, int total, int totalDisplay)>
            GetPagedUsersAsync(int pageIndex,
            int pageSize, string searchText, string orderBy)
        {
            var outParameters = new Dictionary<string, Type>()
            {
                { "Total", typeof(int) },
                { "TotalDisplay", typeof(int) }
            };

            var result = await _adoNetUtility.QueryWithStoredProcedureAsync<UserDTO>("GetPagedUserData",
                    new Dictionary<string, object>
                    {
                        { "PageIndex", pageIndex},
                        { "PageSize", pageSize },
                        { "SearchText", searchText},
                        { "OrderBy", orderBy }
                    },
                    outParameters);

            return (result.result, int.Parse(result.outValues.ElementAt(0).Value.ToString()), int.Parse(result.outValues.ElementAt(1).Value.ToString()));
        }

        public UserProfile GetUser(Guid id)
        {
            return _unitOfWork.UserProfiles.GetById(id);
        }

        public void UpdateUser(Guid id, string address, string education, string experience, string imageUrl, string designation, string githubUsername, string linkedInUsername, bool isActive)
        {
            UserProfile userProfile = _unitOfWork.UserProfiles.GetById(id);
            userProfile.Address = address;
            userProfile.Education = education;
            userProfile.Experience = experience;
            userProfile.ImageUrl = imageUrl;
            userProfile.Designation = designation;
            userProfile.GithubUsername = githubUsername;
            userProfile.LinkedInUsername = linkedInUsername;
            userProfile.IsActive = isActive;
            _unitOfWork.Save();
        }
    }
}
