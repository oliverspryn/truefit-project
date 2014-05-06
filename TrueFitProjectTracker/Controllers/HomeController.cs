﻿using System;
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

        public ActionResult Project(string id) {
			try {
				return View(new ProjectFactory(id));
			} catch (InvalidOperationException e) {
				Response.Redirect("/");
				return View();
			}
        }

        [HttpGet]
        public ActionResult ReportBug(string Key) {
			JiraAuth jira = new JiraAuth();
            BugModel model = new BugModel(Key);
            return View(model);

        }

        [HttpPost]
        public ActionResult ReportBug(BugModel model) {
			JiraAuth jira = new JiraAuth();
            Issue issue = jira.CreateIssue(model.ProjectKey);
            issue.Description = model.Description;
            issue.Summary = model.Summary;
            issue.Type = "Bug";
            
            issue.SaveChanges();
            return RedirectToAction("_ReportBug");
        }

        public ActionResult _ReportBug()
        {
            return View();
        }
    }
}