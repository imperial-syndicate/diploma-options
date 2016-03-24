using DiplomaDataModel.Diploma;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace OptionsWebAPI.Controllers
{
    [EnableCors("*" , "*", "*")]
    public class ChoicesController : ApiController
    {
        DiplomasContext db = new DiplomasContext();
        
        public JObject GetGraphData()
        {
            JObject allChoices = new JObject();
            JObject choiceNum = new JObject();
            int[] arr_choice;
            var options = db.Options.Select(o => o.OptionID).ToArray();
            int[] arr_choices;

            var option_names = db.Options.Select(o => o.Title).ToArray();
            JArray ja_options = new JArray();
            ja_options.Add(option_names);
            int?[] choice1;
            int?[] choice2;
            int?[] choice3;
            int?[] choice4;
            int?[][] choices;
            JArray c1;
            int[] allTermIDs = db.YearTerms.Select(t => t.YearTermID).ToArray();
            int[] years      = db.YearTerms.Select(t => t.Year).ToArray();
            int[] terms      = db.YearTerms.Select(t => t.Term).ToArray();
            int yearterm_num = 0;

            for (int b = 0; b < allTermIDs.Length; b++){
                yearterm_num = allTermIDs[b];
                choice1 = db.Choices.Where(c => c.YearTermID == yearterm_num).Select(c => c.FirstChoiceOptionId).ToArray();
                choice2 = db.Choices.Where(c => c.YearTermID == yearterm_num).Select(c => c.SecondChoiceOptionId).ToArray();
                choice3 = db.Choices.Where(c => c.YearTermID == yearterm_num).Select(c => c.ThirdChoiceOptionId).ToArray();
                choice4 = db.Choices.Where(c => c.YearTermID == yearterm_num).Select(c => c.FourthChoiceOptionId).ToArray();

                //An array that contains arrays of choices
                choices = new int?[][] { choice1, choice2, choice3, choice4 };
                choiceNum = new JObject();
                //Iterate through each choices: 1-4
                for (int k = 0; k < choices.Length; k++)
                {
                    c1 = new JArray();
                    arr_choices = new int[options.Length];
                    arr_choice = new int[options.Length + 1];   //Array holder for current iteration of all choices
                    for (int j = 0; j < choices[k].Length; j++) //Iterate through individual arrays of choices, 1st 2nd 3rd 4th choices
                        arr_choice[(int)choices[k][j]]++;  //Increment any repeating choices

                    Array.Copy(arr_choice, 1, arr_choices, 0, options.Length);
                    c1.Add(arr_choices);

                    choiceNum.Add("Choice" + (k + 1), c1);
                }
                allChoices.Add(years[b] + "" + terms[b], choiceNum);
            }

            allChoices.Add("Options", ja_options);

            return allChoices;
        }
        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
