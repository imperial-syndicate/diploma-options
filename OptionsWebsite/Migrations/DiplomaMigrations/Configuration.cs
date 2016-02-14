namespace OptionsWebsite.Migrations.DiplomaMigrations
{
    using DiplomaDataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.Diploma.DiplomasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\DiplomaMigrations";
        }

        protected override void Seed(DiplomaDataModel.Diploma.DiplomasContext context)
        {
            List<YearTerm> yearterms = new List<YearTerm>() {
              new YearTerm {Year=2015, Term = 20, isDefault=false},
              new YearTerm {Year=2015, Term = 30, isDefault=false},
              new YearTerm {Year=2016, Term = 10, isDefault=false},
              new YearTerm {Year=2016, Term = 30, isDefault=true},
            };
            context.YearTerms.AddOrUpdate(s => s.YearTermID, yearterms.ToArray());

            List<Option> options = new List<Option>() {
              new Option {Title="Data Communications", isActive=true},
              new Option {Title="Client Server", isActive=true},
              new Option {Title="Digital Processing", isActive=true},
              new Option {Title="Informations Systems", isActive=true},
              new Option {Title="Database", isActive=false},
              new Option {Title="Web and Mobile", isActive=true},
            };
            context.Options.AddOrUpdate(o => o.OptionID, options.ToArray());
        }
    }
}
