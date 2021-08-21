using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic_Layer.Bussiness
{
    public class AddressExecution
    {
        public static List<AddressDTO> Get_Address_From_Student(IAddress addressRepository, int studentId)
        {
            List<Address> addresses = new List<Address>();
            List<AddressDTO> addressesDTO = new List<AddressDTO>();
            try
            {
                addresses = addressRepository.Get_Address_With_Student(studentId);
                addresses.ForEach(x =>
                {
                    addressesDTO.Add(new AddressDTO
                    {
                        AddressId = x.Id,
                        TextAddress = x.TextAddress,
                        AddressType = x.AddressType.Name
                    });
                });
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AddressExecution.Get_Address_From_Student {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }
            return addressesDTO;
        }

        public static async Task<bool> Insert_New_Address(IAddress addressRepository,
                                                          IStudent studentRepository,
                                                          IAddressType AddressTypeRepository,
                                                          AddAddressDTO model)
        {
            try
            {
                var addressType = await AddressTypeRepository.GetByIdAsync(model.AddressTypeId);
                var student = await studentRepository.GetByIdAsync(model.StudentId);
                if (addressType != null && student != null)
                {
                    await addressRepository.CreateAsync(new Address
                    {
                        TextAddress = model.TextAddress,
                        AddressTypeId = model.AddressTypeId,
                        StudentId = model.StudentId
                    });
                    return true;
                }                
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AddressExecution.Insert_New_Address {0} {1}",
                                 ResponseStrings.Error, ex.Message));
            }
            return false;
        }

        public static async Task<bool> Update_Address(IAddress addressRepository, UpdateAddressDTO model)
        {
            try
            {
                Address address = await addressRepository.GetByIdAsync(model.Id);
                if (address != null)
                {
                    address.TextAddress = model.TextAddress;
                    address.AddressTypeId = model.AddressTypeId;
                    address.StudentId = model.StudentId;
                    await addressRepository.UpdateAsync(address);

                    return true;
                }                
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AddressExecution.Update_Address {0} {1}",
                                 ResponseStrings.Error, ex.Message));                
            }
            return false;
        }


        public static async Task<(bool, Address)> Get_Address_By_Id(IAddress addressRepository, int id)
        {
            try
            {
                Address address = await addressRepository.GetByIdAsync(id);
                if (address != null)
                {
                    return (true, address);
                }                
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AddressExecution.Get_Addresst_By_Id {0} {1}",
                                 ResponseStrings.Error, ex.Message));                
            }
            return (false, null);
        }

        public static async Task<bool> Delete_Address(IAddress addressRepository, Address model)
        {
            try
            {
                await addressRepository.DeleteAsync(model);

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.AddressExecution.Delete_Address {0} {1}",
                                 ResponseStrings.Error, ex.Message));
                return false;
            }
        }
    }
}
