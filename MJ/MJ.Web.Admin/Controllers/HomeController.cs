using System;
using System.Web.Mvc;
using System.Web.Security;
using MJ.Common.DTO;
using MJ.Services.IServices;
using MJ.Services;
using MJ.Web.Admin.Models;

namespace MJ.Web.Admin.Controllers
{
    public class HomeController : Controller
    {
        #region SERVICES

        private IStaticService _staticService;
        public IStaticService StaticService
        {
            get { return _staticService ?? (_staticService = new StaticService()); }
        }

        private IUserService _userService;
        public IUserService UserService
        {
            get { return _userService ?? (_userService = new UserService()); }
        }

        #endregion

        #region ActResults

        public ActionResult Index()
        {
            ViewBag.result = TempData["Message"] as string;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserModel user)
        {
            if (IsValidCredential(user.Username, user.Password))
            {
                var userId = UserService.GetUserId(user.Username);

                var userInfo = UserService.GetUserInfo(userId);

                FormsAuthentication.SetAuthCookie(userInfo.Email, false);
                SetUserDataSession(userInfo);

                return RedirectToAction("Index", "Blog");
            }
            else
            {
                TempData["Message"] = "No such user found. Please try again.";
                return RedirectToAction("Index", "Blog");
            }

            return View(user);
        }

        // POST USING AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username, string password, string confirmpassword)
        {
            if (UserService.CheckEmailExists(username))
            {
                var result = "Exists";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                var result = "Failed";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            if (password != confirmpassword)
            {
                var result = "SamePassword";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            UserDto userDto = BuildUserDto(username, password);
            bool checker = UserService.AddUser(userDto);

            if (checker)
            {
                TempData["Message"] = "Successfully Created new account. Please login.";
                const string result = "Success";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region PRIVATE

        private UserDto BuildUserDto(string username, string password)
        {
            const string defaultUserType = "Admin";
            var userTypedId = StaticService.GetUserTypeId(defaultUserType);

            var userDto = new UserDto
            {
                UserId = Guid.NewGuid(),
                Email = username,
                Password = password,
                UserTypeId = userTypedId,
                IsActive = false,
                NeedsPasswordReset = false,
                DateTimeCreated = DateTime.Now
            };

            return userDto;
        }

        #endregion

        #region BUILD

        #endregion

        #region MISC

        private bool IsValidCredential(string username, string password)
        {
            return UserService.UserLogin(username, password);
        }

        private void SetUserDataSession(UserDto user)
        {
            try
            {
                var userData = new UserSessionData
                {
                    UserId = user.UserId,
                    Email = user.Email
                };

                Session["userData"] = userData;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}