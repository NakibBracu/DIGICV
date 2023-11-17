using DigiCV.Application.Features.DTOs;
using DigiCV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Application.Features.Training.Services
{
    public interface IUserService
    {
        Task<(IList<UserDTO> records, int total, int totalDisplay)> GetPagedUsersAsync(int pageIndex,
            int pageSize, string searchText, string orderBy);

        UserProfile GetUser(Guid id);

        void UpdateUser(Guid id, string address, string education, string experience, string imageUrl, string designation, string githubUsername, string linkedInUsername, bool isActive);
    }
}