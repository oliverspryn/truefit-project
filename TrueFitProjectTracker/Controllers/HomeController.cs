using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueFitProjectTracker.ViewModels;
using TrueFitProjectTracker.Models;
using Atlassian.Jira;
using TrueFitProjectTracker.Factories.Dashboard;

namespace TrueFitProjectTracker.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ProjectViewModel project;
        public Atlassian.Jira.Issue newIssue;
        public ActionResult ProjectsList()
        {
            //check to see if we need to move the username/password elsewhere again.
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            //ProjectsListModel projectsList = new ProjectsListModel(jira);
            ProjectsListModel viewModel = new ProjectsListModel(jira);
            return View(viewModel);
        }

        public ActionResult Project(string name) {
			return View(new ProjectFactory(name));
        }

        [HttpGet]
        public ActionResult ReportBug(string Title)
        {
            string test = Title;
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            BugModel model = new BugModel(project, jira);
            return View(model);

        }
        [HttpPost]
        public ActionResult ReportBug(BugModel model)
        {
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            var issue = jira.CreateIssue("Agile Scrum");
            issue.Description = model.Description;
            issue.Summary = model.Summary;
            issue.Reporter = model.Reporter;
            issue.SaveChanges();
            return RedirectToAction("Project");
        }
    }
}
