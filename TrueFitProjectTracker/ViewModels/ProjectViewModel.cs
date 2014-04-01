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
            ProjectCompletion = 78;
            
            // TODO: write logic - for each (task in project) ...
            RecentTasksCompletedCount = 42;
            TasksCompletedCount = 392;
            RemainingTasksCount = 105;

            RecentBugsCompletedCount = 42;
            BugsCompletedCount = 267;
            RemainingBugsCount = 83;

            // tasks

            

        }
        
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


    }

    //list classes

    public class TaskEntryViewModel
    {
        public string Title { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}