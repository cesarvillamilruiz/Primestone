using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interfaces
{
    public interface IAddress : IGenericRepository<Address>
    {
        List<Address> Get_Address_With_Student(int studentId);
    }
}
