using ClientSide_Interface_Layer.ViewModel;
using Data_Access_Layer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_Business_Layer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingApiController : ControllerBase
	{
	 private readonly HotelSystemContext _context;

		public BookingApiController(HotelSystemContext context)
		{
			_context = context;
		}
		// GET: api/<BookingApiController>
		[HttpGet("{id}")]
		[Authorize]
		public IActionResult GetRoomWithId(int id)
		{
			var room = _context.Room.FirstOrDefault(c => c.Id == id);
			var BranchName = _context.Branches.FirstOrDefault(c => c.BranchId == room.BranchId).BranchName;
			var TypeName = _context.RoomType.FirstOrDefault(c => c.Id == room.RoomTypeId).Name;
			var discount = 0;
			RoomBookViewModel model = new RoomBookViewModel
            {
                Discount = discount,
                ImagePath = room.ImagePath,
                Price = (decimal)room.RoomPrice,
                RoomType = TypeName,
				RoomBranch = BranchName,
                RoomNumber = room.RoomNumber,
                RoomId = room.Id,
                TotalAmount = (decimal)room.RoomPrice * (1 - discount),
				BranchId = room.BranchId,
            };
            return Ok(model);
		}
		[HttpPost]
        [Authorize]
        public IActionResult PostBooking(RoomBookViewModel model)
		{
			decimal amount = 0;
			var bookInDb = _context.Booking.FirstOrDefault(c => c.UserName == model.Username);
			if(bookInDb != null)
			{
				amount = model.TotalAmount * (decimal).95;
			}
			else
			{
				amount = model.TotalAmount;
			}
            Booking book = new Booking
            {
                RoomId = model.RoomId,
                CurrentDate = model.CurrentDate,
                TotalAmount = amount,
                UserName = model.Username
            };
            _context.Booking.Add(book);
			_context.SaveChanges();
			return Ok();
		}
		[HttpGet("BookingWithUserName/{UserName}")]
		
		public IActionResult GetBookingWithName(string UserName)
		{
			var booking = _context.Booking.Where(c => c.UserName == UserName)
				         .Include(c => c.Room)
						 .Include(c => c.Room.Branch)
						 .Include(c=>c.Room.RoomType).ToList();


			if (!booking.Any())
			{
				return NotFound();
			}
			else
			{
				return Ok(booking);
			}

		}
		// GET api/<BookingApiController>/5
		[HttpDelete("DeleteBooking/{id}")]
		public IActionResult DeleteRoom( int Id)
		{
			
			var room = _context.Booking.FirstOrDefault(c=>c.Id == Id);
			string userName = room.UserName;

            if (room == null)
			{
				return NotFound();
			}
			else
			{
				_context.Booking.Remove(room);
				_context.SaveChanges();
				return Ok(userName);
			}	
		}
		
		
					
	}
}
