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
        public int YearTermID { get; set; }

        public int Year { get; set; }

        // Winter           = 10
        // Spring / Summer  = 20
        // Fall             = 30
        public int Term { get; set; }

        public bool isDefault { get; set; }
    }
}