using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Example.BusinessLogic.Infrastructure;
using Example.Models;
using Example.Web.Models;

namespace Example.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// The page with all Users.
        /// </summary>
        /// <returns></returns>
        [Route("Users")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync().ConfigureAwait(false);
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// The page for adding new User.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View("AddEdit", new User());
        }

        /// <summary>
        /// Save the new User and redirect to the Index page.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            if (user == null)
            {
                TempData["ErrorMessage"] = $"The user is null";
                return RedirectToAction("Error");
            }

            try
            {
                await _userService.AddUserAsync(user).ConfigureAwait(false);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Page for editing the User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{id}/Edit")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = $"Value of the {nameof(id)} parameter should be more than 0";
                return RedirectToAction("Error");
            }

            try
            {
                var user = await _userService.GetUserByIdAsync(id).ConfigureAwait(false);
                if (user == null)
                {
                    TempData["ErrorMessage"] = $"The user with id {id} couldn't be found";
                    return RedirectToAction("Error");
                }


                return View("AddEdit", user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Update the User and redirect to the Index page.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("User/Edit")]
        public async Task<IActionResult> Edit(User user)
        {
            if (user == null)
            {
                TempData["ErrorMessage"] = $"The user is null";
                return RedirectToAction("Error");
            }

            if (user.Id <= 0)
            {
                TempData["ErrorMessage"] = $"The Id of the user should be more than 0";
                return RedirectToAction("Error");
            }

            try
            {
                await _userService.UpdateUserAsync(user).ConfigureAwait(false);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Delete the User and redirect to the Index page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("User/{id}/Delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = $"Value of the {nameof(id)} parameter should be more than 0";
                return RedirectToAction("Error");
            }

            try
            {
                await _userService.DeleteUserAsync(id).ConfigureAwait(false);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// The page with the error info.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = TempData["ErrorMessage"]?.ToString()
        });
        }
    }
}