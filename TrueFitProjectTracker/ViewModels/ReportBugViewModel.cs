
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atlassian.Jira;
namespace TrueFitProjectTracker.ViewModels
{
    public class BugViewModel
    {


        public BugViewModel(ProjectViewModel project, Jira jira)
        {
            title = "Agile Scrum";
        }

        public string title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Reporter { get; set; } 


    }
}
