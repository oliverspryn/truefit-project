using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueFitProjectTracker.ViewModels;
using TrueFitProjectTracker.Models;
using Atlassian.Jira;
using TrueFitProjectTracker.Factories.Dashboard;
using TrueFitProjectTracker.Factories;

namespace TrueFitProjectTracker.Controllers {
    public class HomeController : Controller {
		public Atlassian.Jira.Issue newIssue;
		public ProjectViewModel project;

        public ActionResult ProjectsList()  {
			JiraAuth jira = new JiraAuth();
            ProjectsListModel list = new ProjectsListModel(jira);

			return View(list);
        }

        public ActionResult Project(string name) {
			return View(new ProjectFactory(name));
        }

        [HttpGet]
        public ActionResult ReportBug(string Title) {
			JiraAuth jira = new JiraAuth();
            BugModel model = new BugModel(project, jira);

            return View(model);

        }

        [HttpPost]
        public ActionResult ReportBug(BugModel model) {
			JiraAuth jira = new JiraAuth();
            Issue issue = jira.CreateIssue("Agile Scrum");
            issue.Description = model.Description;
            issue.Summary = model.Summary;
            issue.Reporter = model.Reporter;
            issue.SaveChanges();

            return RedirectToAction("Project");
        }
    }
}