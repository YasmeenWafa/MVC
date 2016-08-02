using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tryMVC.Models
{
    public class CustomersModel
    {
        [Key]
        public int customerID { get; set; }
        [Required(ErrorMessage ="Customer Name is required")]
        [MaxLength(50, ErrorMessage = "Customer name has to be no more than 50 charachters")]
        [Display(Name = " Name")]
        public string customerName { get; set; }
        [Display(Name = "Address")]

        public string customerAddress { get; set; }
        [Display(Name = "Age")]

        public int customerAge { get; set; }
        [Required(ErrorMessage = "Please select gender type")]
        [Display(Name = "Gender")]

        public Gender gender { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        [Display(Name = "Email Address")]

        public string customerEmail { get; set; }
        [Display(Name = "Phone Number")]

        public Int32 phoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your country's name")]
        [Display(Name = "Country")]

        public  string nationalityName { get; set; }

    }
}