using ITMS.Database.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMS.Business.Services.Model
{
   public class UserMasterModel 
    {
        public int UserId { get; set; }
       // public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        
    }
}
