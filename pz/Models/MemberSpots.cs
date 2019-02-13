using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pz.Models
{
    public class MemberSpots
    {
        public int ID { get; set; }
        public int ProfileId { get; set; }
        public int SpotId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Spot VisitedPoint { get; set; }

    }
}