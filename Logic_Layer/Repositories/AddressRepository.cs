using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.DTO;
using Logic_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Logic_Layer.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddress
    {
        private readonly DataContext context;

        public AddressRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public List<Address> Get_Address_With_Student(int studentId)
        {
            List<Address> result = this.context.Address.Where(x => x.StudentId == studentId).
                                                        Include(t => t.AddressType).
                                                        ToList();
            
            return result;
        }
    }
}
