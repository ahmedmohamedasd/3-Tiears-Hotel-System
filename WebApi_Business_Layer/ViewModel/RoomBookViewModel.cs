using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClientSide_Interface_Layer.ViewModel
{
	public class RoomBookViewModel
	{
		public int RoomId { get; set; }
		public int RoomNumber { get; set; }
		public string RoomType { get; set; }
		public string RoomBranch { get; set; }
		public int BranchId { get; set; }
		public decimal Price { get; set; }
		[DataType(DataType.Date)]
		public DateTime CurrentDate  { get; set; }
		public string ImagePath { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal Discount { get; set; }
		public string Username { get; set; }
		public string Token { get; set; }
	}
}
