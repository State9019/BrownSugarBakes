using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;//added for Model Validation

namespace BrownSugarBakes.UI.MVC.Models
{
    public class ContactViewModel

    {
        [Required(ErrorMessage = "**Name is required**")]
        public string Name
        {

            get;
            set;
        }

        [Required(ErrorMessage = "**Email is required")]
        //[RegularExpression("ExpressonPatternHere",ErrorMessage ="** Must be a valid email address**")]
        //If you need to use a regular expression to match a pattern, you can use the syntax above
        [DataType(DataType.EmailAddress)]//uses a built-in regex pattern to validate
        public string Email
        {
            get;
            set;
        }

        [Required(ErrorMessage = "**Subject is required")]
        public string Subject
        {
            get;
            set;
        }

        [Required(ErrorMessage = "**Message is required")]
        [UIHint("MultilineText")]//Provides a textarea (multiline textbox) for this property

        public string Message
        {
            get;
            set;
        }

    }
}