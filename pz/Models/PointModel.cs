using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pz.Models
{
    public class PointModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string QR_code { get; set; }
       // public int EventID { get; set; }
        ///public virtual  Event { get; set; }
    }
}