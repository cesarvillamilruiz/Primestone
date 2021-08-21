using Data_Access.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DataAccess
{
    public class SeedDb
    {
        private readonly DataContext context;
        public SeedDb(DataContext _context)
        {
            context = _context;
        }
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            

            if (!this.context.AddressType.Any())
            {
                var addressTypes = new List<AddressType>();
                addressTypes.Add(new AddressType { Name = "Domicilio" });
                addressTypes.Add(new AddressType { Name = "Laboral" });
                addressTypes.Add(new AddressType { Name = "Temporal" });
                
                this.context.AddressType.AddRange(addressTypes);

                await this.context.SaveChangesAsync();
            }

            if (!this.context.Gender.Any())
            {
                var genders = new List<Gender>();
                genders.Add(new Gender { Name = "Femenino" });
                genders.Add(new Gender { Name = "Masculino" });
                genders.Add(new Gender { Name = "No Definido" });

                this.context.Gender.AddRange(genders);
                await this.context.SaveChangesAsync();
            }

            if (!this.context.Student.Any())
            {
                var gender = this.context.Gender.FirstOrDefault();
                var student = new Student
                {
                    Names = "Admin",
                    LastNames = "Admin",
                    BirthDate = DateTime.Now,
                    GenderId = gender.Id
                };

                this.context.Student.Add(student);
                await this.context.SaveChangesAsync();
            }

            if (!this.context.Course.Any())
            {
                var courses = new List<Course>();
                courses.Add(new Course { CodigoCurso = "A001", NombreCurso = "Maths", FechaInicio = new DateTime(2021,01,15), FechaFin = new DateTime(2021, 06, 15) });
                courses.Add(new Course { CodigoCurso = "A002", NombreCurso = "Spanish", FechaInicio = new DateTime(2021, 03, 15), FechaFin = new DateTime(2021, 06, 25) });
                courses.Add(new Course { CodigoCurso = "A003", NombreCurso = "Chemistry", FechaInicio = new DateTime(2020, 01, 15), FechaFin = new DateTime(2020, 06, 15) });

                this.context.Course.AddRange(courses);
                await this.context.SaveChangesAsync();
            }

            if (!this.context.Address.Any())
            {
                var addresses = new List<Address>();
                var addressType = this.context.AddressType.FirstOrDefault();
                var student = this.context.Student.FirstOrDefault();
                addresses.Add(new Address { TextAddress = "Calle 4 # 5", AddressType = addressType, StudentId = student.Id });
                addresses.Add(new Address { TextAddress = "Calle 64 # 77", AddressType = addressType, StudentId = student.Id });

                this.context.Address.AddRange(addresses);
                await this.context.SaveChangesAsync();
            }

            if (!this.context.AuthUser.Any())
            {
                var student = this.context.Student.FirstOrDefault();
                var authUser = new AuthUser
                {
                    User = "Admin",
                    Password = "Admin",
                    StudentId = student.Id
                };

                this.context.AuthUser.Add(authUser);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
