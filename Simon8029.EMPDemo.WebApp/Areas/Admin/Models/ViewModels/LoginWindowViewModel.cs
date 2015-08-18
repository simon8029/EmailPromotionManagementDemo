using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simon8029.EMPDemo.WebApp.Areas.Admin.Models.ViewModels
{
    public class LoginWindowViewModel
    {
        [DisplayName("User Name:"),Required]
        public string LoginName { get; set; }
        [DisplayName("Password:"),Required]
        public string LoginPassword { get; set; }
        [DisplayName("Validate Code:"),Required]
        public string  LoginValidateCode { get; set; }
    }
}