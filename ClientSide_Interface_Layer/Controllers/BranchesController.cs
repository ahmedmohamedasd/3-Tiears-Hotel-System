using ClientSide_Interface_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApi_Business_Layer.ViewModel;

namespace ClientSide_Interface_Layer.Controllers
{
    public class BranchesController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewRooms(int branchId )
        {
            ViewBag.BranchId = branchId;
           var date = DateTime.Now.ToString("yyyy'-'MM'-'dd");
            RoomsTypeViewModel model;
            HttpResponseMessage httpResponse = GlobalVariables.WebApiClient.GetAsync("Branches/"+ branchId +"/"+ date.ToString()).Result;
            model = httpResponse.Content.ReadAsAsync<RoomsTypeViewModel>().Result;
            model.CurrentDate = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public IActionResult SearchByDate(RoomsTypeViewModel room)
        {
            ViewBag.BranchId = room.BranchID;
            var date = room.CurrentDate.ToString("yyyy'-'MM'-'dd");
            RoomsTypeViewModel model;

            HttpResponseMessage httpResponse = GlobalVariables.WebApiClient.GetAsync("Branches/" + room.BranchID + "/" + date.ToString()).Result;
            model = httpResponse.Content.ReadAsAsync<RoomsTypeViewModel>().Result;
            model.CurrentDate = room.CurrentDate;
            return View("ViewRooms",model);
        }

      
        //public IActionResult Index()
        //{
        //    IEnumerable<MvcBranch> branchList;
        //    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Branches").Result;
        //    branchList = response.Content.ReadAsAsync<IEnumerable<MvcBranch>>().Result;
        //    return View(branchList);
        //}
        //[HttpGet]
        //public IActionResult AddOrEdit(int id=0)
        //{
        //    if (id == 0)
        //    {
        //        return View(new MvcBranch());
        //    }
        //    else
        //    {
        //        HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Branches/" + id).Result;
        //        return View(response.Content.ReadAsAsync<MvcBranch>().Result);
        //    }
        //}
        //[HttpPost]
        //public IActionResult AddOrEdit(MvcBranch model)
        //{
        //    if(model.BranchId == 0)
        //    {
        //        HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Branches", model).Result;
        //        TempData["SuccessMessage"] = "Branch Add Successfully";
        //    }
        //    else
        //    {
        //        HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Branches/"+model.BranchId.ToString(), model).Result;
        //        TempData["SuccessMessage"] = "Updated Successfully";

        //    }
        //    return RedirectToAction("Index");
        //}

        //public IActionResult Delete(int id)
        //{
        //    HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Branches/" + id).Result;
        //    TempData["SuccessMessage"] = "Deleted Successfully";
        //    return RedirectToAction("Index");

        //}
    }
}
