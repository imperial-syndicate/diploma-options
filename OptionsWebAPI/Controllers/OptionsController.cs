using DiplomaDataModel.Diploma;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OptionsWebAPI.Controllers
{
    //[Authorize(Roles = "Student")]
    public class OptionsController : ApiController
    {
        DiplomasContext db = new DiplomasContext();

        // GET: api/Options
        public JObject Get()
        {
            JObject output = new JObject();
            JArray ja_enabled_options = new JArray();
            JObject yearTerm = new JObject();

            // Get the option names
            var enabled_option_names = db.Options.Where(o => o.isActive == true).Select(o => o.Title).ToArray();
            ja_enabled_options.Add(enabled_option_names);

            // Get the users username
            // TODO: Get the username associated with the authenticated user

            // Get the current yearterm
            var yearTerm_query = db.YearTerms.Where(o => o.isDefault == true).First();
            yearTerm.Add("id", yearTerm_query.YearTermID);
            yearTerm.Add("year", yearTerm_query.Year);
            yearTerm.Add("term", yearTerm_query.Term);

            // Add all to the output object
            output.Add("options", ja_enabled_options);
            output.Add("yearterm", yearTerm);

            return output;
        }

        // GET: api/Options/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Options
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Options/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Options/5
        public void Delete(int id)
        {
        }
    }
}
