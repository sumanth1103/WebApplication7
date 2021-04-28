using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class SingleEmp
    {
        [Display(Name ="id")]
        public int emp { get; set; }
    }
}