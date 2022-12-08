using Data_Access_Layer;
using System.Collections.Generic;

namespace WebApi_Business_Layer.ViewModel
{
	public class AvailableRoomViewModel
	{
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int BranchId { get; set; }
        public int RoomTypeId { get; set; }
        public decimal? RoomPrice { get; set; }
        public string ImagePath { get; set; }

        public virtual Branches Branch { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
        public bool Available { get; set; } = true;
        public AvailableRoomViewModel(Room model)
        {
            Id = model.Id;
            RoomNumber = model.RoomNumber;
            BranchId = model.BranchId;
            RoomTypeId = model.RoomTypeId;
            RoomPrice = model.RoomPrice;
            ImagePath = model.ImagePath;
            Available = false;
            
        }
    }
}
