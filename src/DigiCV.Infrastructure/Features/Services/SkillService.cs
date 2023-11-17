using DigiCV.Application;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure.Features.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigiCV.Infrastructure.Features.Services
{
    public class SkillService : ISkillService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public SkillService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateSkill(string name)
        {
            if (_unitOfWork.Skills.IsDuplicateName(name, null))
                throw new DuplicateNameException("Skill name is duplicate");
            Skill skill = new Skill()
            {
                Name = name
            };

            _unitOfWork.Skills.Add(skill);
            _unitOfWork.Save();
        }

        public void DeleteSkill(int id)
        {
            _unitOfWork.Skills.Remove(id);
            _unitOfWork.Save();
        }

        public async Task<(IList<Skill> records, int total, int totalDisplay)> GetPagedSkillsAsync(int pageIndex,
        int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.Skills.GetTableDataAsync(
                x => x.Name.Contains(searchText), orderBy, pageIndex, pageSize);
            return result;
        }

        public Skill GetSkill(int id)
        {
            return _unitOfWork.Skills.GetById(id);
        }

        public IList<Skill> GetSkills()
        {
            return _unitOfWork.Skills.GetAll();
        }

        public void UpdateSkill(int id, string name)
        {
            if (_unitOfWork.Skills.IsDuplicateName(name, id))
                throw new DuplicateNameException("Skill name is duplicate");
            Skill skill = _unitOfWork.Skills.GetById(id);
            skill.Name = name;
            _unitOfWork.Save();
        }
    }
}