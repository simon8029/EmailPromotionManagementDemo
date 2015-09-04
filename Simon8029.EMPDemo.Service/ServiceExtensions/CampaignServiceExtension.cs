using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon8029.EMPDemo.Service
{
    public partial class EM_CampaignsService
    {
        public void UpdateCampaignInstanceEvent(string guid, int eventId, string eventStatus, DateTime eventDate)
        {
            var query = DbSession.EM_CampaignInstancesRepository.Get(c => c.Guid == new Guid(guid));
            if (query.Count() > 0)
            {
                var i = query.First();
                i.EventID = eventId;
                i.EventStatus = eventStatus;
                i.EventDate = eventDate;
                DbSession.SaveChanges();
            }
        }

    }
}
