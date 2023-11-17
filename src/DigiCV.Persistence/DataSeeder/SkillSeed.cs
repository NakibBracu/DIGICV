using DigiCV.Domain.Entities;
using DigiCV.Persistence.Features.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCV.Persistence.DataSeeder
{
    public static class SkillSeed
    {
        public static IList<Skill> SkillList()
        {
            var skills = new List<Skill>()
            {
                new Skill()
                {
                    Id = 1,
                    Name= ".Net Core, Web API, NodeJs"
                },
                new Skill()
                {
                    Id = 2,
                    Name = "Asp.net, .Net MVC"
                },
                new Skill()
                {
                    Id = 3,
                    Name = "C#, VB.Net"
                },
                new Skill()
                {
                    Id = 4,
                    Name= "LINQ, SQL, Entity Framework and MongoDB"
                },
                new Skill()
                {
                    Id = 5,
                    Name = "Angular 6, Angular 1 and MEAN Stack"
                },
                new Skill()
                {
                    Id = 6,
                    Name= "Javascript, JQuery and Ajax"
                },
                new Skill()
                {
                    Id = 7,
                    Name= "Bootstrap CSS, HTML"
                },
                new Skill()
                {
                    Id = 8,
                    Name = "Crystal and RDLC Report"
                },
                new Skill()
                {
                    Id = 9,
                    Name= "Design Pattern & Principles, OOP Software Design& Architecture"
                },
                new Skill()
                {
                    Id = 10,
                    Name = "TFS, GIT, V. SourceSafe, Trello, Agile, SCRUM"
                }
            };
            return skills;
        }
    }
}
