using Data_Access.Data.Entities;
using Data_Access.DataAccess;
using Logic_Layer.Helpers;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Bussiness
{
    public class HostedServiceExecution
    {
        public static bool Insert_New_DoWork(DataContext context)
        {
            try
            {
                context.DoWork.Add(new DoWork
                {
                    EstaBorrado = false,
                    Evento = "Continue...",
                    Fecha = DateTime.Now
                });
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Add(string.Format("Logic_Layer.Bussiness.HostedServiceExecution.Insert_New_DoWork {0} {1}",
                                 ResponseStrings.Error, ex.Message));
                return false;
            }
        }
    }
}
