using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel;
using DiplomaDataModel.Diploma;

namespace OptionsWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChoicesController : Controller
    {
        private DiplomasContext db = new DiplomasContext();

        // GET: Choices
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);

            return View(choices.ToList());
        }

        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            Choice choice = choices.Where(c => c.ChoiceID == id).First();
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // GET: Choices/Create
        [OverrideAuthorization()]
        [Authorize(Roles = "Student,Admin")]
        public ActionResult Create()
        {
            // There are new YearTerms available, so error out
            if (db.YearTerms.Count() <= 0)
            {
                ViewBag.ErrorDetails = "There are no terms available";
                ViewBag.ReturnUrl = "Index";
                return View("Error");
            }

            // Retrieve only the active choices
            var activeOptions = getOptions();

            ViewBag.FirstChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title");

            // Figure out what the name of the currently selected YearTerm is
            Dictionary<String, Object> yearTermValues = getYearTermInfo();
            ViewBag.yearTermID = yearTermValues["yearTermID"];
            int yt = (int)yearTermValues["yearTermID"];
            ViewBag.yearTermName = yearTermValues["yearTermName"];

            if (User.IsInRole("Student"))
            {
                var student = db.Choices.Where(c => c.StudentID == User.Identity.Name && c.YearTermID == yt).Count();

                if (student > 0)
                {
                    ViewBag.ErrorDetails = "You have already submitted a choice for this term.";
                    ViewBag.ReturnUrl = "Index";
                    return View("Error");
                }
            } 
       
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [OverrideAuthorization()]
        [Authorize(Roles = "Student,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceID,YearTermID,StudentID,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId")] Choice choice)
        {
            // Set the date of selection on the server side
            choice.SelectionDate = DateTime.Now;

            // Check to see if the user has made unique choices
            bool isValid = true;
            if (!validChoices(choice))
            {
                ModelState.AddModelError("", "The options you chose must be all different");
                isValid = false;
            }

            // Retrieve only the active choices
            var activeOptions = getOptions();

            ViewBag.FirstChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(activeOptions, "OptionID", "Title", choice.ThirdChoiceOptionId);

            // Figure out what the name of the currently selected YearTerm is
            Dictionary<String, Object> yearTermValues = getYearTermInfo();
            ViewBag.yearTermID = yearTermValues["yearTermID"];
            int yt = (int)yearTermValues["yearTermID"];
            ViewBag.yearTermName = yearTermValues["yearTermName"];

            if (User.IsInRole("Admin"))
            {
                var student = db.Choices.Where(c => c.StudentID == choice.StudentID && c.YearTermID == yt).Count();

                if (student > 0)
                {
                    ViewBag.ErrorDetails = "A Student has already been submitted for this term";
                    ViewBag.ReturnUrl = "Index";
                    return View("Error");
                }
            }

            if (ModelState.IsValid && isValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                return View("Thanks");
            }

            return View(choice);
        }

        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }

            // Retrieve only the active choices
            var activeOptions = getOptions();

            ViewBag.FirstChoiceOptionId = new SelectList(getSpecificOption((int)choice.FirstChoiceOptionId), "OptionID", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(getSpecificOption((int)choice.SecondChoiceOptionId), "OptionID", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(getSpecificOption((int)choice.ThirdChoiceOptionId), "OptionID", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(getSpecificOption((int)choice.FourthChoiceOptionId), "OptionID", "Title", choice.ThirdChoiceOptionId);

            // Display the YearTerm that the choice is associated with
            Dictionary<String, Object> yearTermValues = getYearTermInfo();
            ViewBag.yearTermID = yearTermValues["yearTermID"];
            ViewBag.yearTermName = yearTermValues["yearTermName"];

            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceID,YearTermID,StudentID,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            // Check to see if the user has made unique choices
            bool isValid = true;
            if (!validChoices(choice))
            {
                ModelState.AddModelError("", "The options you chose must be all different");
                isValid = false;
            }

            if (ModelState.IsValid && isValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Retrieve only the active choices
            var activeOptions = getOptions();

            ViewBag.FirstChoiceOptionId = new SelectList(getSpecificOption((int)choice.FirstChoiceOptionId), "OptionID", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(getSpecificOption((int)choice.SecondChoiceOptionId), "OptionID", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(getSpecificOption((int)choice.ThirdChoiceOptionId), "OptionID", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(getSpecificOption((int)choice.FourthChoiceOptionId), "OptionID", "Title", choice.ThirdChoiceOptionId);

            // Display the YearTerm that the choice is associated with
            Dictionary<String, Object> yearTermValues = getYearTermInfo();
            ViewBag.yearTermID = yearTermValues["yearTermID"];
            ViewBag.yearTermName = yearTermValues["yearTermName"];

            return View(choice);
        }

        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            Choice choice = choices.Where(c => c.ChoiceID == id).First();
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Check to make sure that the user has entered unique choices
        private bool validChoices(Choice choice)
        {
            // Make sure the values aren't null
            if (choice.FirstChoiceOptionId == null
                || choice.SecondChoiceOptionId == null
                || choice.ThirdChoiceOptionId == null
                || choice.FourthChoiceOptionId == null)
            {
                return false;
            }

            // Check for non-duplicate options
            var list = new List<int>();
            list.Add((int)choice.FirstChoiceOptionId);
            list.Add((int)choice.SecondChoiceOptionId);
            list.Add((int)choice.ThirdChoiceOptionId);
            list.Add((int)choice.FourthChoiceOptionId);

            if (list.Count != list.Distinct().Count())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Returns a DB object of the diploma options that are Active
        private IQueryable<Option> getOptions()
        {
            return db.Options.Where(c => c.isActive == true);
        }

        // Returns a DB object of the diploma options that are Active
        private IQueryable<Option> getSpecificOption(int optionId)
        {
            return db.Options.Where(c => c.isActive == true || c.OptionID == optionId);
        }

        // Returns a readable string of the passed in YearTerm
        // eg. Passing in 30, 2015, returns "Fall 2015"
        private String getReadableYearTerm(int termNum, int termYear)
        {
            String value = "";

            switch (termNum)
            {
                case 10:
                    value = "Winter " + termYear;
                    break;
                case 20:
                    value = "Spring/Summer " + termYear;
                    break;
                case 30:
                    value = "Fall " + termYear;
                    break;
            }

            return value;
        }

        private Dictionary<String, Object> getYearTermInfo()
        {
            // Figure out what the name of the currently selected YearTerm is
            var currentYearTerm = db.YearTerms.Where(c => c.isDefault == true).First();
            var yearTermID = currentYearTerm.YearTermID;
            var yearTermNum = currentYearTerm.Term;
            var yearTermYear = currentYearTerm.Year;
            var yearTermName = "";

            yearTermName = getReadableYearTerm(yearTermNum, yearTermYear);

            Dictionary<String, Object> dict = new Dictionary<string, object>();
            dict.Add("yearTermID", yearTermID);
            dict.Add("yearTermName", yearTermName);

            return dict;
        }
 
    }
}
