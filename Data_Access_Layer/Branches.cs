using System;
using System.Collections.Generic;

namespace Data_Access_Layer
{
    public partial class Branches
    {
        public Branches()
        {
            Room = new HashSet<Room>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchLocation { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
