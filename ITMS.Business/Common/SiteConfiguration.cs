using System;
using System.Collections.Generic;
using System.Text;

namespace ITMS.Business.Common
{
  public  class SiteConfiguration
    {
        public string ITMSWebAPIUrl { get; set; }
        public string ConnectionMaster { get; set; }

        public string SiteURLLive { get; set; }
        public string SiteURLLocal { get; set; }
        public string DocumentPath { get; set; }

        public string WebPortalLiveLink { get; set; }
        public string TriCityBusAppLink { get; set; }
        public string HelplineNumber { get; set; }
        public string LocomateLive { get; set; }

    }
}
