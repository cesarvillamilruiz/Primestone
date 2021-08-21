using Data_Access.Data.Entities;
using Logic_Layer.Bussiness;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController:ControllerBase
    {
        private readonly IStudent studentRepository;
        private readonly IAddress addressRepository;
        private readonly IAddressType AddressTypeRepository;
        public AddressController(IStudent studentRepository, IAddress addressRepository, IAddressType AddressTypeRepository)
        {
            this.studentRepository = studentRepository;
            this.addressRepository = addressRepository;
            this.AddressTypeRepository = AddressTypeRepository;
        }

        [HttpGet("Get_Address_From_Student/{studentId}")]
        public IActionResult Get_Address_From_Student(int studentId)
        {
            var result = AddressExecution.Get_Address_From_Student(this.addressRepository, studentId);

            return Ok(result);
        }

        [HttpPost("Add_New_Address")]
        public async Task<IActionResult> Add_New_Address(AddAddressDTO model)
        {
            if (ModelState.IsValid && model != null)
            {
                if (await AddressExecution.Insert_New_Address(this.addressRepository, 
                                                              this.studentRepository, 
                                                              this.AddressTypeRepository, 
                                                              model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Address,
                            ResponseStrings.Added,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpPut("Update_Address")]
        public async Task<IActionResult> Update_Address(UpdateAddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await AddressExecution.Update_Address(this.addressRepository, model))
                {
                    return Ok(string.Format("{0} {1} {2}",
                            ResponseStrings.Address,
                            ResponseStrings.Updated,
                            ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }
            return BadRequest(ResponseStrings.NoValid);
        }

        [HttpDelete("Delete_Address/{id}")]
        public async Task<IActionResult> Delete_Address(int id)
        {
            var result = await AddressExecution.Get_Address_By_Id(this.addressRepository, id);
            if (result.Item1)
            {
                if (await AddressExecution.Delete_Address(this.addressRepository, result.Item2))
                {
                    return Ok(string.Format("{0} {1} {2}",
                              ResponseStrings.Address,
                              ResponseStrings.Deleted,
                              ResponseStrings.Success));
                }
                return BadRequest(ResponseStrings.Error);
            }

            return BadRequest(string.Format("{0} {1} {2}",
                              ResponseStrings.Address,
                              ResponseStrings.Not,
                              ResponseStrings.Exist));
        }
    }
}
