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
       //service item foreign
       //customer name foreign


    }
}