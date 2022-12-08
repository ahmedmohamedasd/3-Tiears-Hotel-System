using ClientSide_Interface_Layer.Models;
using Data_Access_Layer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using WebApi_Business_Layer.ViewModel;


namespace ClientSide_Interface_Layer.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(MvcUser user)
        {
            HttpResponseMessage httpResponse = GlobalVariables.WebApiClient.PostAsJsonAsync("Login",user ).Result;
            var token = httpResponse.Content.ReadAsAsync<string>().Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                HttpResponseMessage userResponse = GlobalVariables.WebApiClient.GetAsync("User/CurresntUser").Result;
                var ApiUser = userResponse.Content.ReadAsAsync<MvcUser>().Result;

                TempData["CurrentUser"] = ApiUser.Username;
                TempData["_Token"] = token;
                return RedirectToAction("Home", "Branches");
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "UserName or Password is Incorrect");
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                UserModel user = new UserModel
                {
                    EmailAddress = model.Email,
                    Password = model.Password,
                    Username = model.Username,
                    Role = "user"
                };
                HttpResponseMessage respnse = GlobalVariables.WebApiClient.PostAsJsonAsync("Login/Register", user).Result;
               
                if (respnse.IsSuccessStatusCode)
                {
                    var token = respnse.Content.ReadAsAsync<string>().Result;
                    GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage userResponse = GlobalVariables.WebApiClient.GetAsync("User/CurresntUser").Result;
                    var ApiUser = userResponse.Content.ReadAsAsync<MvcUser>().Result;

                    TempData["CurrentUser"] = ApiUser.Username;
                    TempData["_Token"] = token;
                    return RedirectToAction("ViewRooms", "Branches", new { branchId = 7});

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Name Exist Change User Name ");
                    return View(model);
                }
            }
        }




    }
}
