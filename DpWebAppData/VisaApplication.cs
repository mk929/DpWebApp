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
        [Required]
        public string Name { get; set; }
        [Required]
        public string PassportNumber { get; set; }
        [Required]
        public DateTime PassportIssueDate { get; set; } 
    }
}