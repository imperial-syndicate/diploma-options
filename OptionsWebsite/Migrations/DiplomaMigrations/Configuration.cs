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
              new YearTerm {YearTermID = 1, Year=2015, Term = 20, isDefault = false},
              new YearTerm {YearTermID = 2, Year=2015, Term = 30, isDefault = false},
              new YearTerm {YearTermID = 3, Year=2016, Term = 10, isDefault = false},
              new YearTerm {YearTermID = 4, Year=2016, Term = 30, isDefault = true},
            };
            context.YearTerms.AddOrUpdate(s => s.YearTermID, yearterms.ToArray());

            List<Option> options = new List<Option>() {
              new Option {OptionID = 1, Title = "Data Communications", isActive = true},
              new Option {OptionID = 2, Title = "Client Server", isActive = true},
              new Option {OptionID = 3, Title = "Digital Processing", isActive = true},
              new Option {OptionID = 4, Title = "Informations Systems", isActive = true},
              new Option {OptionID = 5, Title = "Database", isActive = false},
              new Option {OptionID = 6, Title = "Web and Mobile", isActive = true},
            };
            context.Options.AddOrUpdate(o => o.OptionID, options.ToArray());
        }
    }
}
