using Data_Access.Data.Entities;
using Logic_Layer.Bussiness;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudent studentRepository;
        private readonly ICourse courseRepository;
        private readonly IStudentCourse studentCourseRepository;
        public StudentController(IStudent studentRepository,  ICourse courseRepository, IStudentCourse studentCourseRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.studentCourseRepository = studentCourseRepository;
        }

        [HttpGet("Index")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var result = StudentExecution.Get_All_Students(studentRepository);

            return Ok(result);
        }

        [HttpGet("Get_Student_By_Id/{id}")]
        public async Task<IActionResult> Get_Student_By_Id(int id)
        {
            var result = await StudentExecution.Get_Student_By_Id(this.studentRepository, id);
            if (result.Item1)
            {
                return Ok(result.Item2);
            }

            return Ok(string.Format("{0} {1} {2}", 
                      ResponseStrings.Student, ResponseStrings.Not, ResponseStrings.Exist));
        }

        [HttpPost("Add_New_Student")]
        public async Task<IActionResult> Add_New_Student(StudentDTO model)
        { 
            if (ModelState.IsValid && model != null)
            {
                if (await StudentExecution.Insert_New_Student(this.studentRepository, model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Student,
                            ResponseStrings.Added,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpPut("Update_Student")]
        public async Task<IActionResult> Update_Student(Student model)
        {
            if (ModelState.IsValid)
            {
                if(await StudentExecution.Update_Student(this.studentRepository, model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Student,
                            ResponseStrings.Updated,
                            ResponseStrings.Success));
                }
                Log.Instance.Add(ResponseStrings.NoValid);
                return BadRequest(ResponseStrings.Error);
            }
            Log.Instance.Add(ResponseStrings.NoValid);
            return BadRequest(ResponseStrings.NoValid);
            
        }

        [HttpDelete("Delete_Student/{id}")]
        public async Task<IActionResult> Delete_Student(int id)
        {
            var result = await StudentExecution.Get_Student_By_Id(this.studentRepository, id);
            if (result.Item1)
            {
                if (await StudentExecution.Delete_Student(this.studentRepository, result.Item2))
                {
                    return Ok(string.Format("{0} {1} {2}",
                              ResponseStrings.Student,
                              ResponseStrings.Deleted,
                              ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }

            return BadRequest(string.Format("{0} {1} {2}",
                              ResponseStrings.Student,
                              ResponseStrings.Not,
                              ResponseStrings.Exist));
        }

        [HttpPost("Add_Student_Course")]
        public async Task<IActionResult> Add_Student_Course(StudentCourseDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await StudentExecution.Insert_Student_Course(this.studentRepository, this.studentCourseRepository, this.courseRepository, model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Record,
                            ResponseStrings.Added,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }
    }
}
