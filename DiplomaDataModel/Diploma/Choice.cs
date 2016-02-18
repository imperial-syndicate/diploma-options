using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel
{
    public class Choice
    {
        // Primary Key
        [Key]
        public int ChoiceID { get; set; }

        // Foreign Key for YearTerm
        public int YearTermID { get; set; }
        [ForeignKey("YearTermID")]

        public YearTerm YearTerm { get; set; }


        // Student A00... Number
        // Max Length 9 Characters
        [Display(Name = "Student ID")]
        [StringLength(9)]
        [Required]
        [Editable(false)]
        public string StudentID { get; set; }

        // Max Length 40 Characters
        [Display(Name = "First Name")]
        [StringLength(40)]
        [Required]
        public string StudentFirstName { get; set; }

        // Max Length 40 Characters
        [Display(Name = "Last Name")]
        [StringLength(40)]
        [Required]
        public string StudentLastName { get; set; }

        // Foreign Key for FirstOption
        [Display(Name = "First Choice")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }

        [ForeignKey("FirstChoiceOptionId")]
        public Option FirstOption { get; set; }


        // Foreign Key for SecondOption
        [Display(Name = "Second Choice")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }

        [ForeignKey("SecondChoiceOptionId")]
        public Option SecondOption { get; set; }


        // Foreign Key for ThirdOption
        [Display(Name = "Third Choice")]
        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }

        [ForeignKey("ThirdChoiceOptionId")]
        public Option ThirdOption { get; set; }


        // Foreign Key for FourthOption
        [Display(Name = "Fourth Choice")]
        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }

        [ForeignKey("FourthChoiceOptionId")]
        public Option FourthOption { get; set; }


        // Always the current Date-Time
        [Display(Name = "Selection Date")]
        public DateTime SelectionDate { get; set; }
    }
}