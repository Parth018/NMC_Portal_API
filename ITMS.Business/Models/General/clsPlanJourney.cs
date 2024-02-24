using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ITMS.Business.Models.General
{
  public  class clsPlanJourney
    {
        public int srno { get; set; }
        public string sourcetime { get; set; }
        public string destinationtime { get; set; }
        public string totalminutes { get; set; }
        public double totaldistance { get; set; }
        public string halt { get; set; }
        public int fromstationid { get; set; }
        public string fromstationname { get; set; }
        public int tostationid { get; set; }
        public string tostationname { get; set; }
        public int totaltrips { get; set; }
        public int totaltrips1 { get; set; }
        public int fare { get; set; } = 0;
        public int tripstatus { get; set; }
    }
}
