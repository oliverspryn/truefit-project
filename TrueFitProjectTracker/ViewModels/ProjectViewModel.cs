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
        public TaskEntryViewModel(string title, DateTime completionDate){
            Title = title;
            CompletionDate = completionDate;
        }


        public string Title { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}