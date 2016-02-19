using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel
{
    public class YearTerm
    {
        // Primary Key
        [Key]
        [Display(Name = "Term")]

        public int YearTermID { get; set; }

        public int Year { get; set; }

        // Winter           = 10
        // Spring / Summer  = 20
        // Fall             = 30
        [RegularExpression("^(10|20|30)$", ErrorMessage = "Must be 10, 20 or 30")]
        public int Term { get; set; }

        [Display(Name = "Default")]
        public bool isDefault { get; set; }
    }
}