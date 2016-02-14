using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Diploma
{
    public class DiplomasContext : DbContext
    {
        public DiplomasContext() : base("DefaultConnection") { }
        public DbSet <YearTerm> YearTerms { get; set; }
        public DbSet <Choice> Choices { get; set; }
        public DbSet <Option> Options { get; set; }
    }
}
