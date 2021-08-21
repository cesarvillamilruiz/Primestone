using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Bussiness
{
    public class StudentExecution
    {
        public static List<StudentDTO> Get_All_Students(IStudent studentRepository)
        {
            List<StudentDTO> StudentsDTO = new List<StudentDTO>();
            try
            {
                List<Student> students = studentRepository.GetAll().ToList();
                students.ForEach(x => {
                    StudentDTO st = new StudentDTO
                    {
                        Names = x.Names,
                        LastNames = x.LastNames,
                        BirthDate = x.BirthDate,
                        Gender = x.GenderId
                    };
                    StudentsDTO.Add(st);
                });
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Get_All_Students {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }
            
            return StudentsDTO;
        }

        public static async Task<(bool,Student)> Get_Student_By_Id(IStudent studentRepository, int id)
        {
            try
            {
                Student student = await studentRepository.GetByIdAsync(id);
                if (student == null)
                {
                    return (false, null);
                }
                return (true, student);
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Get_Student_By_Id {0} {1}",
                                 ResponseStrings.Error, ex.Message));
                return (false, null);
            }
        }
        public static async Task<bool> Insert_New_Student(IStudent studentRepository,StudentDTO model)
        {
            try
            {
                await studentRepository.CreateAsync(new Student
                {
                    Names = model.Names,
                    LastNames = model.LastNames,
                    BirthDate = model.BirthDate,
                    GenderId = model.Gender
                });

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Insert_New_Student {0} {1}", 
                                 ResponseStrings.Error,  ex.Message));
                return false;
            }
        }

        public static async Task<bool> Update_Student(IStudent studentRepository, Student model)
        {
            try
            {
                await studentRepository.UpdateAsync(model);

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Update_Student {0} {1}",
                                 ResponseStrings.Error, ex.Message));
                return false;
            }
        }

        public static async Task<bool> Delete_Student(IStudent studentRepository, Student model)
        {
            try
            {
                await studentRepository.DeleteAsync(model);

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Delete_Student {0} {1}",
                                 ResponseStrings.Error, ex.Message));
                return false;
            }
        }

        public static async Task<bool> Insert_Student_Course(IStudent studentRepository, 
            IStudentCourse studentCourseRepository, 
            ICourse courseRepository, 
            StudentCourseDTO model)
        {
            try
            {
                var student = await studentRepository.GetByIdAsync(model.StudentId);
                var course  = await courseRepository.GetByIdAsync(model.StudentId);
                if (student != null && course != null)
                {
                    StudentCourse sc = new StudentCourse();
                    sc.CourseId = course.Id;
                    sc.StudentId = student.Id;
                    await studentCourseRepository.CreateAsync(sc);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.Insert_Student_Course {0} {1}",
                                 ResponseStrings.Error, ex.Message));
                return false;
            }
        }
    }
}
