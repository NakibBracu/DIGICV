using DigiCV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Persistence
{
    public interface IApplicationDbContext
    {
        //DbSet<Cv> CVs { get; set; }
        //DbSet<Education> Educations { get; set; }
        //DbSet<Experience> Experiences { get; set; }
        //DbSet<Project> Projects { get; set; }
        //DbSet<Reference> References { get; set; }
        //DbSet<Skill> Skills { get; set; }
        public DbSet<CoverLetter> CoverLetters { get; set; }

    }
}
