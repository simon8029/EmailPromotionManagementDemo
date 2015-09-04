using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Simon8029.EMPDemo.Model;

namespace Simon8029.EMPDemo.WebApp.Areas.EmailMarketing.Models
{
    public class LeadViewModel
    {
        public int LeadID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [DisplayName("Is Valid")]
        public bool IsValid { get; set; }
        [DisplayName("Unsubscribed")]
        public bool Unsubscribed { get; set; }

        public EM_Leads ToPOCO()
        {
            return new EM_Leads()
            {
                LeadID = LeadID,
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                IsValid = IsValid,
                Unsubscribed = Unsubscribed
            };
        }
    }
}