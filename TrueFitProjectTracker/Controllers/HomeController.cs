using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueFitProjectTracker.ViewModels;
using TrueFitProjectTracker.Models;
using Atlassian.Jira;

namespace TrueFitProjectTracker.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ProjectModel project;

        public ActionResult ProjectsList()
        {
            //check to see if we need to move the username/password elsewhere again.
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            ProjectsListModel projectsList = new ProjectsListModel(jira);
            ProjectsListViewModel viewModel = new ProjectsListViewModel(projectsList, jira);
            return View(viewModel);
        }

        public ActionResult Project(string key)
        {
            var jira = new Jira("https://gcctruefit.atlassian.net", "goehringmr1", "kronos5117");
            int id = 4; 
            ProjectModel project = new ProjectModel(id);

            //follow 'Go To Definition' on these models to get the pattern.

            //demo:
            project.Title = "Project X";
            ProjectViewModel viewModel = new ProjectViewModel(key, jira);
            
            return View(viewModel);
        }

        public ActionResult ReportBug(int id)
        {
            BugModel model = new BugModel(project);
            return View(model);
        }
    }
}
