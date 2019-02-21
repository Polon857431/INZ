using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pz.Models
{
    public class Profile
    {
        public int ID { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public Guid Token { get; set; }

        public virtual IList<MemberEvent> MemberOfEvents { get; set; }

        public virtual IList<MemberSpots> VistitedPoints { get; set; }

    }
}