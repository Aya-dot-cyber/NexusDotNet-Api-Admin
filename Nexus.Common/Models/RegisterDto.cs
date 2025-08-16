using Nexus.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Nexus.Common.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Language))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "FullNameValidation", ErrorMessageResourceType = typeof(Language))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669a-zA-Z. ]+$", ErrorMessageResourceName = "OnlyLetterValidation", ErrorMessageResourceType = typeof(Language))]

        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Language))]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?=^.{7,50}$)\b[\w\.-]+@[\w\.-]+\.\w{2,20}\b", ErrorMessageResourceName = "EmailValidation", ErrorMessageResourceType = typeof(Language))]
        [Remote(action: "IsEmailExist", controller: "Account", HttpMethod = "POST", ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "EmailAlreadyExist")]
        [StringLength(50, MinimumLength = 7, ErrorMessageResourceName = "EmailValidationLength", ErrorMessageResourceType = typeof(Language))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Language))]
        [StringLength(50, MinimumLength = 10, ErrorMessageResourceName = "PasswordValidationLength", ErrorMessageResourceType = typeof(Language))]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[-_+=\[\]~\\\W_])\S{8,64}$", ErrorMessageResourceName = "PasswordValidation", ErrorMessageResourceType = typeof(Language))]
        public string Password { get; set; }


        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$", ErrorMessageResourceName = "PhoneNumberValidation", ErrorMessageResourceType = typeof(Language))]
        //[Remote(action: "IsPhoneExist", controller: "Account", HttpMethod = "POST", ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "PhoneNumberExist")]
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (value != null)
                    _phoneNumber = value.TrimStart('0');
            }
        }
        private string _phoneNumber;

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Language))]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(Language))]
        public string ConfirmPassword { get; set; }
        public int Type { get; set; }
        public int CountryId { get; set; }
    }
}
