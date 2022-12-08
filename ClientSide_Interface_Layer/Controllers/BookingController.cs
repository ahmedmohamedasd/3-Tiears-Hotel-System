using ClientSide_Interface_Layer.Models;
using ClientSide_Interface_Layer.ViewModel;
using Data_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClientSide_Interface_Layer.Controllers
{
	public class BookingController : Controller
	{
        [HttpGet]
        public IActionResult BookRoom( int RoomId, DateTime date, string? Token)
        {
           // var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6InNhbWlyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoic2FtaXJAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsImV4cCI6MTY3MDQxOTAyNSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDkvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNDkvIn0.HlyWotedUovkXeooQBYDp8SgZ5LnBQfNjM84xBGvGdU";

          //  GlobalVariables.WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("BookingApi/" + RoomId).Result;
            if (response.IsSuccessStatusCode)
            {
                var room = response.Content.ReadAsAsync<RoomBookViewModel>().Result;
                room.CurrentDate = date;
                return View(room);
            }

            else
            {

                ViewBag.Date = date;
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult BookRoom(RoomBookViewModel model)
        {
           
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("BookingApi", model).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Home", "Branches" );
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult ViewMyBooking(string? UserName) {

            
            var user = UserName;
            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Booking> bookinDb = new List<Booking>();
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("BookingApi/BookingWithUserName/" + UserName).Result;
                if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return View("ViewMyBooking" , bookinDb);
                }
                var model = response.Content.ReadAsAsync<List<Booking>>().Result;
                return View("ViewMyBooking",model);
            }
            
        }
        [HttpGet]
        public IActionResult DeleteBooking(int id)
        {
            
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("BookingApi/DeleteBooking/" + id).Result;
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            else
            {
                var userName = response.Content.ReadAsAsync<String>().Result;
                return RedirectToAction("ViewMyBooking", new { UserName = userName });
            }

        }
    }
}
