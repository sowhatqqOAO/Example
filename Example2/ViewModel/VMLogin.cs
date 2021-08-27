using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Example2.ViewModel
{
    public class VMLogin
    {
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Display(Name = "密碼")]
        public string Password { get; set; }
    }
}