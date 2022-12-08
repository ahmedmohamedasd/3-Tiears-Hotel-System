using Data_Access_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi_Business_Layer.ViewModel;

namespace WebApi_Business_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly Data_Access_Layer.HotelSystemContext _dal;

        public BranchesController(HotelSystemContext dal)
        {
            _dal = dal;
        }
        //[HttpGet]
        //public List<Branches> getAllBranches()
        //{
        //    return _dal.Branches.ToList();
        //}

        [HttpGet("{id}/{date}")]
        public RoomsTypeViewModel GetRoomsWithBranch(int id , string date)
        {
            var currentDate = Convert.ToDateTime(date);
            var booking = _dal.Booking.Where(c => c.CurrentDate.Date == currentDate.Date && c.Room.BranchId == id).ToList();
           
                
            var singleRooms = _dal.Room.Where(c => c.BranchId == id && c.RoomTypeId == 1).Include(c => c.RoomType).Include(c=>c.Branch);
            var doubleRooms = _dal.Room.Where(c => c.BranchId == id && c.RoomTypeId == 2).Include(c => c.RoomType).Include(c => c.Branch);
            var suiteRooms = _dal.Room.Where(c => c.BranchId == id && c.RoomTypeId == 3).Include(c => c.RoomType).Include(c => c.Branch);
           List<AvailableRoomViewModel> availableSingleRooms = new List<AvailableRoomViewModel>();
            foreach (var room in singleRooms)
            {
                foreach(var book in booking)
                {
                    if(book.RoomId == room.Id)
                    {
                        room.Avilable = false;

                    }
                }
            }
            foreach (var room in doubleRooms)
            {
                foreach (var book in booking)
                {
                    if (book.RoomId == room.Id)
                    {
                        room.Avilable = false;

                    }
                }
            }
            foreach (var room in suiteRooms)
            {
                foreach (var book in booking)
                {
                    if (book.RoomId == room.Id)
                    {
                        room.Avilable = false;

                    }
                }
            }

            RoomsTypeViewModel model = new RoomsTypeViewModel
            {
                SingleRooms = singleRooms,
                DoubleRooms = doubleRooms,
                SuiteRooms = suiteRooms
            };
            return model;
        }
        //[HttpGet("{id}")]
        //public IActionResult GetBranch(int id)
        //{
        //    var branch = _dal.Branches.FirstOrDefault(c => c.BranchId == id);
        //    return Ok(branch);
        //}
        //[HttpPost]
        //public IActionResult AddBranch(Branches model)
        //{
        //    _dal.Branches.Add(model);
        //    _dal.SaveChanges();
        //    return Ok(model);
        //}
        //[HttpPut("{id}")]
        //public IActionResult PutBranch(int id , Branches model)
        //{
         
        //    if(id != model.BranchId)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        _dal.Entry(model).State = EntityState.Modified;
        //        _dal.SaveChanges();
        //        return NoContent();
        //    }
        //}
        //[HttpDelete("{id}")]
        //public IActionResult DeleteBranch(int id)
        //{
        //    var branch = _dal.Branches.FirstOrDefault(c => c.BranchId == id);
        //    if(branch == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        _dal.Branches.Remove(branch);
        //        _dal.SaveChanges();
        //        return NoContent();
        //    }
        //}
    }
}
