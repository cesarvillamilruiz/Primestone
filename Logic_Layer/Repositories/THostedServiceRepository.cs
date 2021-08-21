using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic_Layer.Repositories
{
    public class THostedServiceRepository : GenericRepository<DoWork>, ITHostedService
    {
        private readonly DataContext context;

        public THostedServiceRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
