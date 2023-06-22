﻿using Gradebook.Entities;
using Gradebook.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Repositories
{
    public class GradebookRepository : IGradebookRepository
    {
        private readonly IGradebookDbContext _dbContext;

        public GradebookRepository(IGradebookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddGradebook(Entities.Gradebook gradebook)
        {
            _dbContext.Gradebooks.Add(gradebook);
            _dbContext.SaveChanges();
        }

        public void AddGrade(Entities.Gradebook gradebook, Grade grade)
        {
            gradebook.Grades.Add(grade);
            _dbContext.SaveChanges();
        }

        public Entities.Gradebook GetGradebook(string studentName)
        {
            var gradebook = _dbContext.Gradebooks.FirstOrDefault(x => x.Student.Name == studentName)!;

            return gradebook;
        }

        public List<Entities.Gradebook> GetAllGradebooks() =>
            _dbContext.Gradebooks.Include(x => x.Student).Include(x => x.Grades).ToList();

        public void DeleteGradebook(Entities.Gradebook gradebook)
        {
            _dbContext.Gradebooks.Remove(gradebook);
            _dbContext.SaveChanges();
        }

        public void DeleteGrade(Entities.Gradebook gradebook, Grade grade)
        {
            gradebook.Grades.Remove(grade);
            _dbContext.SaveChanges();
        }
    }
}