using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Layer.Repositories
{
    public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourse
    {
        private readonly DataContext context;

        public StudentCourseRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
