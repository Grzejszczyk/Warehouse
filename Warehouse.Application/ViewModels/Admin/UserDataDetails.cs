using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.ViewModels.Admin
{
    public class UserDataDetails
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}
