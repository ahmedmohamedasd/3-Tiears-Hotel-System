using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime CurrentDate { get; set; }
        public string UserName { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual Room Room { get; set; }
        public virtual UserModel UserNameNavigation { get; set; }
    }
}
