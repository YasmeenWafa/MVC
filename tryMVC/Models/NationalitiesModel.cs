using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryMVC.Models
{
    public class NationalitiesModel
    {
        [Key]
        public int nationalityID { get; set; }
        [Required(ErrorMessage = "Nationality name is required ")]
        [Display(Name = "Nationality Name")]
        
        public string nationalityName { get; set; }
    }
}