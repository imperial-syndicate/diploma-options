using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel
{
    public class Option
    {
        // Primary Key
        [Key]
        public int OptionID { get; set; }

        // Max length: 50 characters
        [Display(Name = "Option")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Currently Active")]
        public bool isActive { get; set; }
    }
}