using System;
using System.ComponentModel.DataAnnotations;

namespace Pools.Models
{
    public class Reservations
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid Email Address")]
        public String Email { get; set; }

       // [Required(ErrorMessage = "Mobile no. is required")]
        [Phone]
       // [RegularExpression(@"/([0-9\s\-]{7,})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/)", ErrorMessage = "Please enter a valid Phone number")]
        
        public String MobilePhone { get; set; }


    }
}
