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

            

            //Choices for YearTermID 2 - 201530
            int?[] choice1 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.FirstChoiceOptionId).ToArray();
            int?[] choice2 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.SecondChoiceOptionId).ToArray();
            int?[] choice3 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.ThirdChoiceOptionId).ToArray();
            int?[] choice4 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.FourthChoiceOptionId).ToArray();

            JArray c1 = new JArray();
            //An array that contains arrays of choices
            int?[][] choices = new int?[][] { choice1, choice2, choice3, choice4 };

            //Iterate through each choices: 1-4
            for (int k = 0; k < choices.Length; k++)
            {
                c1 = new JArray();
                arr_choices = new int[options.Length];
                arr_choice = new int[options.Length + 1];   //Array holder for current iteration of all choices
                for (int j = 0; j < choices[k].Length; j++) //Iterate through individual arrays of choices, 1st 2nd 3rd 4th choices
                    arr_choice[   (int)choices[k][j]     ]++;  //Increment any repeating choices
                
                Array.Copy(arr_choice,1, arr_choices, 0, options.Length);
                c1.Add(arr_choices);

                choiceNum.Add("Choice" + (k+1), c1);
            }
            allChoices.Add("201530", choiceNum);
            allChoices.Add("Options", ja_options);

            return allChoices;
        }
    }
}
