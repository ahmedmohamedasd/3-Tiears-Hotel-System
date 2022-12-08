using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi_Business_Layer.ViewModel
{
	public class RoomsTypeViewModel
	{
		public IEnumerable<Room> SingleRooms { get; set; }
		public IEnumerable<Room> DoubleRooms { get; set; }
		public IEnumerable<Room> SuiteRooms { get; set; }
		[DataType(DataType.Date)]
		public DateTime	CurrentDate { get; set; }
		public int BranchID { get; set; }
	}
}
