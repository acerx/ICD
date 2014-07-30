using System;
using System.Web.Mvc;
using MJ.Common.DTO;
using MJ.Services.IServices;
using MJ.Services;

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
            get
            {
                if (_userService == null)
                    _userService = new UserService();
                return _userService;
            }
        }

        #endregion

        public ActionResult Index()
        {

            ViewBag.result = TempData["Message"] as string;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // POST USING AJAX
        public ActionResult Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                var result = "Failed";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                UserDto userDto = BuildUserDto(username, password);
                bool checker = UserService.AddUser(userDto);

                if (checker)
                {
                    TempData["Message"] = "Successfully Created new account. Please login.";
                    var result = "Success";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(new { success = true });
        }

        #region PRIVATE

        private UserDto BuildUserDto(string username, string password)
        {
            var defaultUserType = "Admin";
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

    }
}