using DigiCV.Application.Features.Training.Repositories;
using DigiCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Persistence.Training.Repositories
{
    public class TemplateRepository : Repository<Template, Guid>, ITemplateRepository
    {
        public TemplateRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
