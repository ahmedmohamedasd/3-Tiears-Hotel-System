using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public partial class UserModel
    {
        public UserModel()
        {
            Booking = new HashSet<Booking>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
