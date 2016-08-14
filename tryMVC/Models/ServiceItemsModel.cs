using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryMVC.Models
{
    public class ServiceItemsModel
    {
        [Key]
        public int serviceItemID { get; set; }
        [Required(ErrorMessage ="Please enter the vehicle's type")]
        [Display(Name = "Item Name")]
        public string serviceItemName { get; set; }
        //public virtual List <ServicesModel> services { get; set; }
    }
}