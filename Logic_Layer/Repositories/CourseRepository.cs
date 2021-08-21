using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.Interfaces;

namespace Logic_Layer.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourse
    {
        private readonly DataContext context;

        public CourseRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
