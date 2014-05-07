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
using System.IO;

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
        public ActionResult ReportBug(BugModel model, HttpPostedFileBase attachment)
        {
            
			JiraAuth jira = new JiraAuth();
            Issue issue = jira.CreateIssue(model.ProjectKey);
            issue.Description = model.Description;
            issue.Summary = model.Summary;
            issue.Type = "Bug";

            
            issue.SaveChanges();
            if(attachment != null){
                model.attachment = attachment;
            }
            if (model.attachment != null && model.attachment.ContentLength > 0){
                MemoryStream target = new MemoryStream();
                model.attachment.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                issue.AddAttachment(Uri.EscapeDataString(model.attachment.FileName), data);
            }
            return RedirectToAction("_ReportBug");
        }

        public ActionResult _ReportBug()
        {
            return View();
        }
    }
}