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
        [StringLength(50)]
        public string Title { get; set; }

        public bool isActive { get; set; }
    }
}