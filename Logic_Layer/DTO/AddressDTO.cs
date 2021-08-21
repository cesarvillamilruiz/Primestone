using Data_Access.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Layer.DTO
{
    public class AddressDTO
    {
        public int AddressId { get; set; }
        public string TextAddress { get; set; }
        public string AddressType { get; set; }
    }
}
