using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueFitProjectTracker.ViewModels;
using TrueFitProjectTracker.Models;

namespace TrueFitProjectTracker.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ProjectModel project;

        public ActionResult ProjectsList()
        {
            ProjectsListModel projectsList = new ProjectsListModel();
            ProjectsListViewModel viewModel = new ProjectsListViewModel(projectsList);

            return View(viewModel);
        }

        public ActionResult Project(int id)
        {

            // no viewbags allowed! (except for page title with layout)  :)  -Jefff
            ProjectModel project = new ProjectModel(id);

            //follow 'Go To Definition' on these models to get the pattern.

            //demo:
            project.Title = "Project X";

            ProjectViewModel viewModel = new ProjectViewModel(project);
            
            return View(viewModel);
        }

        public ActionResult ReportBug(int id)
        {
            BugModel model = new BugModel(project);
            return View(model);
        }
    }
}
