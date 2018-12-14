using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DpWebAppData
{
    public class VisaApplication
    {
        [Required(ErrorMessage = "??")]
        public string Name { get; set; }

        [Required(ErrorMessage = "??")]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "??")]
        public DateTime PassportIssueDate { get; set; } 
    }
}