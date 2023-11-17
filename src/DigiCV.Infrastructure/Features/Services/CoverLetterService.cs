using DigiCV.Application;
using DigiCV.Application.Features.Training.Services;
using DigiCV.Domain.Entities;
using DigiCV.Infrastructure.Features.Exceptions;
using System;
using System.Linq;

namespace DigiCV.Infrastructure.Features.Services
{
    public class CoverLetterService : ICoverLetterService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public CoverLetterService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Guid CreateCoverLetter(CoverLetter entity)
        {
            if (_unitOfWork.CoverLetters.IsDuplicateName(entity.Title, null))
            {

                throw new DuplicateNameException("You have a letter with this name.");
            }

            _unitOfWork.CoverLetters.Add(entity);
            _unitOfWork.Save();

            return entity.Id;
        }

        public void DeleteCoverLetter(Guid id)
        {
            _unitOfWork.CoverLetters.Remove(id);
            _unitOfWork.Save();
        }

        public async Task<(IList<CoverLetter> records, int total, int totalDisplay)>
            GetPagedCoverLettersAsync(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = await _unitOfWork.CoverLetters.GetTableDataAsync(x =>
            x.Title.Contains(searchText), orderBy, pageIndex, pageSize);

            return result;
        }

        public CoverLetter GetCoverLetter(Guid id)
        {
            return _unitOfWork.CoverLetters.GetById(id);
        }

        public IList<CoverLetter> GetCoverLetters()
        {
            return _unitOfWork.CoverLetters.GetAll();
        }

        public void UpdateCoverLetter(CoverLetter entity)
        {
            if (_unitOfWork.CoverLetters.IsDuplicateName(entity.Title, entity.Id))
            {
                throw new DuplicateNameException("You have a letter with this name.");
            }
            CoverLetter coverLetter = _unitOfWork.CoverLetters.GetById(entity.Id);
            coverLetter.Title = entity.Title;
            coverLetter.SendingDate = entity.SendingDate;
            coverLetter.SenderName = entity.SenderName;
            coverLetter.SenderEmail = entity.SenderEmail;
            coverLetter.SenderPhone = entity.SenderPhone;
            coverLetter.SenderAddress = entity.SenderAddress;
            coverLetter.SenderAddressEx = entity.SenderAddressEx;
            coverLetter.Recipient = entity.Recipient;
            coverLetter.CompanyName = entity.CompanyName;
            coverLetter.CompanyAddress = entity.CompanyAddress;
            coverLetter.Subject = entity.Subject;
            coverLetter.RecipientAddressing = entity.RecipientAddressing;
            coverLetter.Body = entity.Body;

            _unitOfWork.Save();
        }
        public IList<(Guid Id, string Title)> GetCoverLettersByUserId(Guid userId)
        {
            return _unitOfWork.CoverLetters.GetCoverLettersByUserId(userId);
        }

        public int GetTotalCoverLetterCount()
        {
            var totalCoverLetter = _unitOfWork.CoverLetters.GetCount();
            return totalCoverLetter;
        }
    }
}
