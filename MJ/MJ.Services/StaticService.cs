using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Common.DTO;
using MJ.Common.Entities;
using MJ.Services.IServices;

namespace MJ.Services
{
    public class StaticService : IStaticService
    {

        public Guid GetUserTypeId(string userTyped)
        {
            try
            {
                using (var db = new MJEntities())
                {
                    return db.UserTypes.FirstOrDefault(uType => uType.userTyped.Equals(userTyped)).userTypeId;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
