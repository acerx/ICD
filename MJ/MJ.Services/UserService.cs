using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Common.DTO;
using MJ.Common.Entities;
using MJ.Services.IServices;
using SimpleCrypto;

namespace MJ.Services
{
    public class UserService : IUserService
    {

        public bool AddUser(UserDto userDto)
        {
            bool result = false;

            try
            {
                using (var db = new MJEntities())
                {
                    SystemUser newUser = FillNewSystemUserEntity(db.SystemUsers.Create(), userDto);
                    db.SystemUsers.Add(newUser);

                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return result;
        }

        #region PRIVATE

        private SystemUser FillNewSystemUserEntity(SystemUser newUser, UserDto userDto)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            var encryptPass = crypto.Compute(userDto.Password);

            newUser.userId = userDto.UserId;
            newUser.email = userDto.Email;
            newUser.password = encryptPass;
            newUser.pwdSalt = crypto.Salt;
            newUser.userTypeId = userDto.UserTypeId;
            newUser.isActive = userDto.IsActive;
            newUser.needsPasswordReset = userDto.NeedsPasswordReset;
            newUser.datetimeCreated = userDto.DateTimeCreated;

            return newUser;
        }

        #endregion
    }
}
