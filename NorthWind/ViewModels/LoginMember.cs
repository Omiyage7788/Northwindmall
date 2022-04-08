using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NorthWind.ViewModels
{
    public class LoginMember
    {
        
        [DisplayName("帳號")]
        [Required(ErrorMessage = "帳號為必填")]
        [RegularExpression("[A-Z]{5}", ErrorMessage = "帳號格式有誤")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼為必填")]
        [RegularExpression("[A-Z]{5}", ErrorMessage = "密碼格式有誤")]
        public string Password { get; set; }
    }
}