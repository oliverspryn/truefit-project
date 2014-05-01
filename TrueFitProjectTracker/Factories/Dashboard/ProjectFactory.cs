using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueFitProjectTracker.Models.Dashboard;

namespace TrueFitProjectTracker.Factories.Dashboard {
	public class ProjectFactory : TasksFactory {
	/// <summary>
	/// A high level summary of the bugs associated with a project.
	/// </summary>
		public SummaryModel Bugs { get; set; }

	/// <summary>
	/// The name of this project.
	/// </summary>
		public string Name { get; set; }

	/// <summary>
	/// The project's overall completion percentage.
	/// </summary>
		public int Percent { get; set; }

	/// <summary>
	/// The path in the Jira API to the listing of project details.
	/// </summary>
		private const string PROJECT_DETAILS = "project/";

	/// <summary>
	/// A high level summary of the tasks associated with a project.
	/// </summary>
		public SummaryModel Tasks { get; set; }

		public ProjectFactory(string key) : base(key) {
		//Set up the Bug and Task data structures
			Bugs = new SummaryModel();
			Tasks = new SummaryModel();

		//Fetch the project name
			Name = projectName(key);

			// <jeff id="burndown-and-recent-chart">

			// aggregate the charts
			// both are handled the same way, only difference is where they are assigned
			// (hence the last if statement)
			foreach (string type in new string[] { "Task", "Bug" }) { // to compare task.Issue to
				var tasksList = List.Aggregate(new List<Models.Dashboard.TaskModel>(), (tasklist, value) => {
						tasklist.AddRange(value.Tasks.Where(task => task.Issue == type));
						return tasklist;
					});
				var goodTasks = tasksList.Where(task => task.Created != new DateTime(1970, 1, 1) // no unbegun tasks please!
					&& (task.DueDate != new DateTime(1970, 1, 1) || task.ResolutionDate != new DateTime(1970, 1, 1))).ToList(); // need either or.


				List<double> taskData = new List<double>();
				var start = new Tuple<int, int>((int)DateTime.Now.Month, (int)DateTime.Now.Year);
				var end = new Tuple<int, int>((int)DateTime.Now.Month + 1, (int)DateTime.Now.Year);


				if (goodTasks.Count != 0) {

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
						.Select(monthYear => {   // get work for each month.
							// where this month has something to do with this task
							return Math.Round(tasksList.Where(task => task.Created < monthYear.AddMonths(1)
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
                                                * ((task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate).Subtract(monthYear).Days + 1)
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
                                                * (monthYear.AddMonths(1).Subtract(task.Created).Days - 1)
                                                / (task.ResolutionDate != new DateTime(1970, 1, 1) ? task.ResolutionDate : task.DueDate).Subtract(task.Created).Days;
                                        }
                                    }
                                    // get actual man hours from task effort and percentage. 
                                    return task.Progress.Percent < 100 ? ratioInThisMonth * task.Progress.Expected / 60 : ratioInThisMonth * task.Progress.Committed / 60;
                                })
                                .Sum(), 1);
                        }).ToList();
				}


				// get recent tasks
				List<int> recentTasksData = Enumerable.Range(0, 7) // 0 ... 6
					.Select(n => DateTime.Now.Date.AddDays(-7 + n))
					.Select(day => tasksList.Count(task => task.ResolutionDate.Date.Equals(day))).ToList();

				// completion statistics
				int recentCompletedCount = tasksList.Count(task => task.ResolutionDate.Date.CompareTo(DateTime.Now.Date) <= 0
					&& task.ResolutionDate.Date.CompareTo(DateTime.Now.Date.AddDays(-7)) >= 0);
				int completedCount = tasksList.Count(task => task.ResolutionDate != new DateTime(1970, 1, 1));
				int remainingCount = tasksList.Count() - completedCount;

                int progress = (int)Math.Round((double)(completedCount) / ((remainingCount + completedCount) == 0 ? (remainingCount + completedCount) * 100 : 1), 0); // percent

				// now handle our project model based on if we're doing tasks or bugs

				if (type == "Task") { // tasks
					Tasks.Burndown.Data = taskData;
					Tasks.Burndown.End = end;
					Tasks.Burndown.Start = start;
					Tasks.Completed = completedCount;
					Tasks.Percent = progress;
					Tasks.Recent = recentCompletedCount;
					Tasks.RecentComplete = recentTasksData;
					Tasks.Remaining = remainingCount;
				} else if (type == "Bug") { // bugs
					Bugs.Burndown.Data = taskData;
					Bugs.Burndown.End = end;
					Bugs.Burndown.Start = start;
					Bugs.Completed = completedCount;
					Bugs.Percent = progress;
					Bugs.Recent = recentCompletedCount;
					Bugs.RecentComplete = recentTasksData;
					Bugs.Remaining = remainingCount;
				}
			}

			Percent = (int)((double)(Bugs.Completed + Tasks.Completed) / (Bugs.Total + Tasks.Total) * 100);

			// </jeff>
			
		}

		private string projectName(string key) {
		//Fetching the list of issues will include the project name in each issue, don't ask for it again
			if (List.Count > 0) {
				Dictionary<String, Object> parent = Issues as Dictionary<String, Object>;
				IEnumerable<Object> issueList = parent["issues"] as IEnumerable<Object>;
				Dictionary<String, Object> issue = issueList.First() as Dictionary<String, Object>;
				Dictionary<String, Object> fields = issue["fields"] as Dictionary<String, Object>;
				Dictionary<String, Object> project = fields["project"] as Dictionary<String, Object>;

				return project["name"] as string;
		//No issues? Go fetch the project name
			} else {
				Object project = Jira.RPC(PROJECT_DETAILS + key);
				Dictionary<String, Object> details = project as Dictionary<String, Object>;

				return details["name"] as string;
			}
		}
	}
}