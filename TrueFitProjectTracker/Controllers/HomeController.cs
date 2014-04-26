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
			TasksFactory sprintTasks = new TasksFactory(name);


            // <jeff id="burndown-and-recent-chart">
            
            // aggregate the charts
            // both are handled the same way, only difference is where they are assigned
            // (hence the last if statement)
            foreach(string type in new string[] {"Task", "Bug"} ){ // to compare task.Issue to
				var tasksList = sprintTasks.List.Aggregate(new List<Models.Dashboard.TaskModel>(), (tasklist, value) =>
                    {
                        tasklist.AddRange(value.Tasks.Where(task => task.Issue == type));
                        return tasklist;
                    });
                var goodTasks = tasksList.Where(task => task.Created != new DateTime(1970,1,1) // no unbegun tasks please!
                    && (task.DueDate != new DateTime(1970,1,1) || task.ResolutionDate != new DateTime(1970,1,1))).ToList(); // need either or.


                List<double> taskData = new List<double>();
                var start = new Tuple<int, int>((int)DateTime.Now.Month, (int)DateTime.Now.Year);
                var end = new Tuple<int, int>((int)DateTime.Now.Month + 1, (int)DateTime.Now.Year);


                if (goodTasks.Count != 0)
                {

                    // month, year dates
                    var startDate = goodTasks
                        .Select(task => task.Created).Min().Date;
                    startDate = startDate.AddDays(-startDate.Day + 1);
                    start = new Tuple<int, int>(startDate.Month, startDate.Year);

                    // the last month with activities in it
                    var endDate = goodTasks
                        .Select(task => task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate)
                        .Max().Date;
                    endDate = endDate.AddDays(-endDate.Day + 1);
                    // on the x axis, include extra month
                    end = new Tuple<int, int>(endDate.AddMonths(1).Month, endDate.Year);

                    var datesList = Enumerable
                        .Range(0, 1 + (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month)
                        .Select(i => startDate.AddMonths(i))
                        .ToList();

                    // list of doubls to output to view model in one nice linq query
                    taskData = datesList
                        .Select(monthYear =>
                        {   // get work for each month.
                            // where this month has something to do with this task
                            return tasksList.Where(task => task.Created < monthYear.AddMonths(1)
                                && (task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate) >= monthYear)
                                // get man hours done in this month, final block.
                                .Select(task =>
                                {   // get ratio of % of task being done in this month
                                    double ratioInThisMonth = 0;
                                    if (task.Created < monthYear)
                                    {
                                        if ((task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate) < monthYear.AddMonths(1))
                                        {   // task  |-----|
                                            // month    |----|
                                            ratioInThisMonth = 1d
                                                * (task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate).Subtract(monthYear).Days
                                                / (task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate).Subtract(task.Created).Days;
                                        }
                                        else
                                        {   // task  |---------|
                                            // month    |----|
                                            ratioInThisMonth = 1d
                                                * monthYear.AddMonths(1).Subtract(monthYear).Days
                                                / (task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate).Subtract(task.Created).Days;
                                        }
                                    }
                                    else
                                    {
                                        if ((task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate) < monthYear.AddMonths(1))
                                        {   // task       |-|
                                            // month    |----|
                                            ratioInThisMonth = 1d; // 100%
                                        }
                                        else
                                        {   // task       |----|
                                            // month    |----|
                                            ratioInThisMonth = 1d
                                                * monthYear.Subtract(task.Created).Days
                                                / (task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate).Subtract(task.Created).Days;
                                        }
                                    }
                                    // get actual man hours from task effort and percentage... whoops, don't have em. 
                                    return ratioInThisMonth * monthYear.AddMonths(1).Subtract(monthYear).Days;
                                })
                                .Sum();
                        }).ToList();
                }


                // get recent tasks
                List<int> recentTasksData = Enumerable.Range(0, 7) // 0 ... 6
                    .Select(n => DateTime.Now.Date.AddDays(-7 + n))
                    .Select(day => tasksList.Count(task => task.ResolutionDate.Date.Equals(day))).ToList();

                // completion statistics
                int recentCompletedCount = tasksList.Count(task => task.ResolutionDate.Date.CompareTo(DateTime.Now.Date) <= 0
                    && task.ResolutionDate.Date.CompareTo(DateTime.Now.Date.AddDays(-7)) >= 0);
                int completedCount = tasksList.Count(task => task.ResolutionDate != new DateTime(1970,1,1));
                int remainingCount = tasksList.Count() - completedCount;

                int progress = (int)Math.Round((double)(completedCount) / (remainingCount + completedCount) * 100, 0); // percent

                // now handle our project model based on if we're doing tasks or bugs

                if (type == "Task")
                { // tasks
                    project.TaskProgress = progress;
                    project.TaskBurndownStart = start;
                    project.TaskBurndownEnd = end;
                    project.TaskBurndownChart = taskData;
                    project.TaskRecentChart = recentTasksData;

                    project.RecentTasksCompletedCount = recentCompletedCount;
                    project.TasksCompletedCount = completedCount;
                    project.RemainingTasksCount = remainingCount;
                }
                else if (type == "Bug")
                { // bugs
                    project.BugProgress = progress;
                    project.BugBurndownStart = start;
                    project.BugBurndownEnd = end;
                    project.BugBurndownChart = taskData;
                    project.BugRecentChart = recentTasksData;

                    project.RecentBugsCompletedCount = recentCompletedCount;
                    project.BugsCompletedCount = completedCount;
                    project.RemainingBugsCount = remainingCount;
                }
            }

            project.ProjectCompletion = (int)((double)(project.BugsCompletedCount + project.TasksCompletedCount)
                / (project.BugsCompletedCount + project.TasksCompletedCount + project.RemainingBugsCount + project.RemainingTasksCount)
                * 100);

            // </jeff>

			ViewBag.Tasks = sprintTasks;

            return View(project);
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
            var issue = jira.CreateIssue("Project Management Test-Lehman");
            issue.Description = model.Description;
            issue.Summary = model.Summary;
            issue.Reporter = model.Reporter;
            issue.SaveChanges();
            return RedirectToAction("Project");
        }
    }
}
