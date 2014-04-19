using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Atlassian.Jira;

namespace TrueFitProjectTracker.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(string key, Jira jira)
        {
            var currentProject = jira.GetProjects().First(); //for initialization
            var projects = jira.GetProjects();
            for (int i = 0; i < projects.Count(); ++i)
            {
                var tempProject = projects.ElementAt(i);
                if (tempProject.Key == key)
                {
                    currentProject = tempProject;
                    break;
                }
            }
            var issues = from i in jira.Issues
                         where i.Project == currentProject.Key
                         select i;
            int count = 0;
            for (int i = 0; i < issues.Count(); ++i)
            {
                var newIssue = issues.ElementAt(i);

                count++;
            }

            
            // initialize the properties from the model just... like... this!
            Title = currentProject.Name;

            // summing logic goes here.
            ProjectCompletion = count; // %, 0 through 100
            
            // TODO: write logic - for each (task in project) ...
            RecentTasksCompletedCount = 42;
            TasksCompletedCount = 392;
            RemainingTasksCount = 105;

            RecentBugsCompletedCount = 42;
            BugsCompletedCount = 267;
            RemainingBugsCount = 83;

            // tasks

            UpcomingTasks = new List<TaskEntryViewModel>();
            OneWeekTasks = new List<TaskEntryViewModel>();
            DistantTasks = new List<TaskEntryViewModel>();

            // for testing, in production, use only uncompleted tasks?

            for (int i = 4; i < 6; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    UpcomingTasks.Add(new TaskEntryViewModel("Task " + j + "!", new DateTime(2014, i, j * 3 + 1)));
                }
            }

            OneWeekTasks = UpcomingTasks.Where(task =>
                DateTime.Compare(task.CompletionDate, DateTime.Now.AddDays(7)) < 0
                && DateTime.Compare(task.CompletionDate, DateTime.Now) > 0
                ).OrderBy(task => task.CompletionDate).ToList();
            
            DistantTasks = UpcomingTasks.Where(task =>
                DateTime.Compare(task.CompletionDate, DateTime.Now.AddDays(7)) > 0
                ).OrderBy(task => task.CompletionDate).ToList();


            TaskBurndownChart = new List<double>();
            TaskRecentChart = new List<double>();

            BugBurndownChart = new List<double>();
            BugRecentChart = new List<double>();

        } // more obvious: End of Constructor


        
        public string Title { get; set; }
        public int ProjectCompletion { get; set; } // percentage

        public int RecentTasksCompletedCount { get; set; } // number
        public int TasksCompletedCount { get; set; } // number
        public int RemainingTasksCount { get; set; } // number

        public int RecentBugsCompletedCount { get; set; } // number
        public int BugsCompletedCount { get; set; } // number
        public int RemainingBugsCount { get; set; } // number

        // tasks
        public List<TaskEntryViewModel> UpcomingTasks { get; set; }
        public List<TaskEntryViewModel> OneWeekTasks { get; set; }
        public List<TaskEntryViewModel> DistantTasks { get; set; }


        // task and bug charts
        public double TaskProgress { get; set; } // percentage
        public Tuple<int, int> TaskBurndownStart { get; set; } // month, year, 0 for jan
        public Tuple<int, int> TaskBurndownEnd { get; set; } // month, year
        public List<double> TaskBurndownChart { get; set; } // filled one per month
        
        public List<double> TaskRecentChart { get; set; } // 7 items, one week before Now(), today is not charted

        public double BugProgress { get; set; }
        public Tuple<int, int> BugBurndownStart { get; set; }
        public Tuple<int, int> BugBurndownEnd { get; set; }
        public List<double> BugBurndownChart { get; set; } 
        
        public List<double> BugRecentChart { get; set; } 
       

    }

    //list classes

    public class TaskEntryViewModel
    {
        public TaskEntryViewModel(string title, DateTime completionDate){
            Title = title;
            CompletionDate = completionDate;
        }


        public string Title { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}