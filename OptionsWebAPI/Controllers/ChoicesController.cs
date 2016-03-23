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
            JObject jo = new JObject();
            var a = db.Choices.Select(c => c.FirstChoiceOptionId).ToArray();
            var i = 0;
            foreach (var n in a)
                jo.Add("Choice"+i,a[i++]);
            return jo;
        }
    }
}
