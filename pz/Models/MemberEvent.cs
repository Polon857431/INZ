using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pz.Models
{
    public class MemberEvent
    {

        public int ID { get; set; }
        public int ProfileId { get; set; }
        public int EventId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual EventModel Event { get; set;}
    }
}