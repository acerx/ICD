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

        public bool AddUser(SystemUserDto userDto)
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

        public bool UserLogin(string username, string password)
        {
            var crypto = new PBKDF2();

            using (var db = new MJEntities())
            {
                var user = db.SystemUsers.FirstOrDefault(u=> u.email.Equals(username));

                if (user != null)
                {
                    var decryptedPassword = crypto.Compute(password, user.pwdSalt);

                    if (user.password.Equals(decryptedPassword))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public Guid GetUserId(string email)
        {
            using (var db = new MJEntities())
            {
                return db.SystemUsers.FirstOrDefault(u => u.email.Equals(email)).userId;
            }
        }

        public SystemUserDto GetUserInfo(Guid userId)
        {
            SystemUserDto userDto = null;

            using (var db = new MJEntities())
            {
                SystemUser user = db.SystemUsers.FirstOrDefault(u => u.userId == userId);

                if (user == null)
                {
                    userDto = null;
                }

                var uId = user.userId;

                userDto = BuildUserDto(user);
            }

            return userDto;
        }

        public bool CheckEmailExists(string email)
        {
            bool result = false;

            try
            {
                using (var db = new MJEntities())
                {
                    var query = (from a in db.SystemUsers
                                 where a.email == email
                                 select a).FirstOrDefault();

                    if (query != null)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

            return result;
        }

        #region PRIVATE

        private SystemUser FillNewSystemUserEntity(SystemUser newUser, SystemUserDto userDto)
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

        #region BUILD / AUTOMAP

        private SystemUserDto BuildUserDto(SystemUser user)
        {
            AutoMapper.Mapper.CreateMap<SystemUser, SystemUserDto>();
            SystemUserDto userDto = AutoMapper.Mapper.Map<SystemUser, SystemUserDto>(user);

            return userDto;
        }

        #endregion
    }
}
