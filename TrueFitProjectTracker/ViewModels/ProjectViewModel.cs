using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel(Models.ProjectModel project)
        {
            // initialize the properties from the model just... like... this!
            Title = project.Title;

            // summing logic goes here.
            ProjectCompletion = 78; // %, 0 through 100
            
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


        TaskProgress = 79;
        TaskBurndownChart = new List<double> {100.0, 86.9, 73.5, 67.5, 55.2, 47.5, 40.2, 31.5, 27.3, 20.3, 13.9, 9.6};
        TaskRecentChart = new List<double> {5, 3, 4, 7, 2, 1, 6};

        BugProgress = 76;
        BugBurndownChart = new List<double> {100.0, 86.9, 73.5, 67.5, 55.2, 47.5, 40.2, 31.5, 27.3, 20.3, 13.9, 9.6};
        BugRecentChart = new List<double> { 5, 3, 4, 7, 2, 1, 6 };




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

        public double TaskProgress { get; set; }
        public List<double> TaskBurndownChart { get; set; } // looks like 12 numbers, 1 for each month
        public List<double> TaskRecentChart { get; set; } // looks like 1 week frame

        public double BugProgress { get; set; }
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