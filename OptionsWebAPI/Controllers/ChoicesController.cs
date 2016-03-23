using DiplomaDataModel.Diploma;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace OptionsWebAPI.Controllers
{
    public class ChoicesController : ApiController
    {
        DiplomasContext db = new DiplomasContext();

        public JObject Get()
        {
            JObject optionsForAChoice = new JObject();
            JObject allChoices = new JObject();
            JObject choiceNum = new JObject();
            int[] arr_choice;
            var options = db.Options.Select(o => o.OptionID).ToArray();

            //Choices for YearTermID 2 - 201530
            int?[] choice1 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.FirstChoiceOptionId).ToArray();
            int?[] choice2 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.SecondChoiceOptionId).ToArray();
            int?[] choice3 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.ThirdChoiceOptionId).ToArray();
            int?[] choice4 = db.Choices.Where(c => c.YearTermID == 2).Select(c => c.FourthChoiceOptionId).ToArray();

            //An array that contains arrays of choices
            int?[][] choices = new int?[][] { choice1, choice2, choice3, choice4 };

            //Iterate through each choices: 1-4
            for (int k = 0; k < choices.Length; k++)
            {
                optionsForAChoice = new JObject();
                arr_choice = new int[options.Length + 1];   //Array holder for current iteration of all choices
                for (int j = 0; j < choices[k].Length; j++) //Iterate through individual arrays of choices, 1st 2nd 3rd 4th choices
                    arr_choice[   (int)choices[k][j]     ]++;  //Increment any repeating choices
                for (int i = 1; i <= options.Length; i++)
                    optionsForAChoice.Add("C" + (k+1) +"_" + i.ToString(), arr_choice[i]);  //Add tags to identify each choice

                choiceNum.Add("Choice" + (k+1), optionsForAChoice);
            }
            allChoices.Add("201530", choiceNum);

            return allChoices;
        }
    }
}
