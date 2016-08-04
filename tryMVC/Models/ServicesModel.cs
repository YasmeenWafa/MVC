using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryMVC.Models
{
    public class ServicesModel
    {
        [Key]
        public int serviceID;
        [Required(ErrorMessage ="Please enter the Service Name")]
        [Display(Name = "Service Name")]
        public string serviceName;
    }
}