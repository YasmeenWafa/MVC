using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryMVC.Models
{
    public class WorkModel
    {
        [Key]
        public int workID { get; set; }

        [Required(ErrorMessage ="Please enter the price")]
        [Display (Name = "Price")]
        public double price;

        //service foreign
        [Required(ErrorMessage = "Please enter service's name")]
        [Display(Name = "Service")]
        public virtual string serviceName { get; set; }


        //service item foreign
        [Required(ErrorMessage = "Please enter item's name")]
        [Display(Name = "Item Name")]
        public virtual string itemName { get; set; }


        //customer name foreign
        [Required(ErrorMessage = "Please enter customer's name")]
        [Display(Name = "Customer Name")]
        public virtual string customerName { get; set; } 


    }
}