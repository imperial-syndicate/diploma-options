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
              new Option {OptionID = 7, Title = "Tech Pro", isActive = false }
            };
            context.Options.AddOrUpdate(o => o.OptionID, options.ToArray());

            List<Choice> choices = new List<Choice>()
            {
                // Year Term 201530
                new Choice {
                        ChoiceID = 1, YearTermID = 2,
                        StudentID = "A00000001",
                        StudentFirstName = "Alex", StudentLastName = "Xela",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 3, FourthChoiceOptionId = 4,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 2, YearTermID = 2,
                        StudentID = "A00000002",
                        StudentFirstName = "Bradley", StudentLastName = "Yeldarb",
                        FirstChoiceOptionId = 4, SecondChoiceOptionId = 1,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 3, YearTermID = 2,
                        StudentID = "A00000003",
                        StudentFirstName = "Chad", StudentLastName = "Dach",
                        FirstChoiceOptionId = 3, SecondChoiceOptionId = 4,
                        ThirdChoiceOptionId = 1, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 4, YearTermID = 2,
                        StudentID = "A00000004",
                        StudentFirstName = "Dillan", StudentLastName = "Nallid",
                        FirstChoiceOptionId = 2, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 4, FourthChoiceOptionId = 1,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 5, YearTermID = 2,
                        StudentID = "A00000005",
                        StudentFirstName = "Emily", StudentLastName = "Ylime",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 3, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 6, YearTermID = 2,
                        StudentID = "A00000006",
                        StudentFirstName = "Fred", StudentLastName = "Derf",
                        FirstChoiceOptionId = 6, SecondChoiceOptionId = 1,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 7, YearTermID = 2,
                        StudentID = "A00000007",
                        StudentFirstName = "George", StudentLastName = "Egroeg",
                        FirstChoiceOptionId = 3, SecondChoiceOptionId = 6,
                        ThirdChoiceOptionId = 1, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 8, YearTermID = 2,
                        StudentID = "A00000008",
                        StudentFirstName = "Hank", StudentLastName = "Knah",
                        FirstChoiceOptionId = 2, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 6, FourthChoiceOptionId = 1,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 9, YearTermID = 2,
                        StudentID = "A00000009",
                        StudentFirstName = "Isabel", StudentLastName = "Lebasi",
                        FirstChoiceOptionId = 2, SecondChoiceOptionId = 4,
                        ThirdChoiceOptionId = 6, FourthChoiceOptionId = 7,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 10, YearTermID = 2,
                        StudentID = "A00000010",
                        StudentFirstName = "James", StudentLastName = "Semaj",
                        FirstChoiceOptionId = 7, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 4, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                }
                //Yea Term 201610 Below
                ,
                new Choice {
                        ChoiceID = 11, YearTermID = 3,
                        StudentID = "A00000011",
                        StudentFirstName = "Kim", StudentLastName = "Mik",
                        FirstChoiceOptionId = 6, SecondChoiceOptionId = 7,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 4,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 12, YearTermID = 3,
                        StudentID = "A00000012",
                        StudentFirstName = "Louis", StudentLastName = "Siuol",
                        FirstChoiceOptionId = 4, SecondChoiceOptionId = 6,
                        ThirdChoiceOptionId = 7, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 13, YearTermID = 3,
                        StudentID = "A00000013",
                        StudentFirstName = "Mikey", StudentLastName = "Yekim",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 6, FourthChoiceOptionId = 7,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 14, YearTermID = 3,
                        StudentID = "A00000014",
                        StudentFirstName = "Nico", StudentLastName = "Ocin",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 5,
                        ThirdChoiceOptionId = 3, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 15, YearTermID = 3,
                        StudentID = "A00000015",
                        StudentFirstName = "Omar", StudentLastName = "Ramo",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 6, FourthChoiceOptionId = 2,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 16, YearTermID = 3,
                        StudentID = "A00000016",
                        StudentFirstName = "Patty", StudentLastName = "Yttap",
                        FirstChoiceOptionId = 3, SecondChoiceOptionId = 5,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 7,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 17, YearTermID = 3,
                        StudentID = "A00000017",
                        StudentFirstName = "Quagmire", StudentLastName = "Erimqauq",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 7,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 18, YearTermID = 3,
                        StudentID = "A00000018",
                        StudentFirstName = "Raymond", StudentLastName = "Dnomyar",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 3,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 6,
                        SelectionDate = DateTime.Now
                },
                new Choice {
                        ChoiceID = 19, YearTermID = 3,
                        StudentID = "A00000019",
                        StudentFirstName = "Sam", StudentLastName = "Mas",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 7,
                        ThirdChoiceOptionId = 2, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                }
                ,
                new Choice {
                        ChoiceID = 20, YearTermID = 3,
                        StudentID = "A00000020",
                        StudentFirstName = "Theodore", StudentLastName = "Erodoeht",
                        FirstChoiceOptionId = 1, SecondChoiceOptionId = 2,
                        ThirdChoiceOptionId = 7, FourthChoiceOptionId = 3,
                        SelectionDate = DateTime.Now
                }
            };
            context.Choices.AddOrUpdate(c => c.ChoiceID, choices.ToArray());

        }
    }
}
