using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pz.Models
{
    public class EventModel
    {
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Nazwa nie może być dluższa niz 50 znaków")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SRC { get; set; }
        public virtual IList<PointModel> Points { get; set; }
        public virtual IList<MemberEvent> Members { get; set; }


    }
}