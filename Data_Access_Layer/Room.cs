using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public partial class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int BranchId { get; set; }
        public int RoomTypeId { get; set; }
        public decimal? RoomPrice { get; set; }
        public string ImagePath { get; set; }
        public bool? Avilable { get; set; }

        public virtual Branches Branch { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
