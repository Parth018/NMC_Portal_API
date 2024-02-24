using System;
using System.ComponentModel.DataAnnotations;

namespace ITMS.Business.Services.Model
{
    public class AgeGroupMasterModel
    {
        public Int32 Id { get; set; }
        //[RegularExpression(@"^[0-9- ]+$", ErrorMessage = "Invalid Age Group")]
        [Required(ErrorMessage = "Enter Age Group")]
        [Display(Name = "Age Group")]
        public string AgeGroup { get; set; }

    }
}
