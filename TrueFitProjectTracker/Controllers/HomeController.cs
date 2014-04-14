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

        public ActionResult ProjectsList()
        {
            //check to see if we need to move the username/password elsewhere again.
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            //ProjectsListModel projectsList = new ProjectsListModel(jira);
            ProjectsListModel viewModel = new ProjectsListModel(jira);
            return View(viewModel);
        }

        public ActionResult Project(string name)
        {
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            
            //NOT NEEDED ANYMORE?
            //ProjectModel project = new ProjectModel(id);
            //follow 'Go To Definition' on these models to get the pattern.
            //demo:
            //project.Title = "Project X";
            //int id = 4; 

            project = new ProjectViewModel(name, jira);
			var stuff = new TasksFactory(name);
            
            return View(project);
        }

        public ActionResult ReportBug(string Title)
        {
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            BugViewModel model = new BugViewModel(project, jira);
            return View(model);
        }
    }
}
