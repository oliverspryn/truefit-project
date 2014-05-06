using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atlassian.Jira;
using TrueFitProjectTracker.ViewModels;
using System.Web.Mvc;

namespace TrueFitProjectTracker.Models
{
    public class BugModel
    {
        public BugModel()
        {
        }

        public BugModel(String key)
        {
            ProjectKey = key;
        }

        public string title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ProjectKey { get; set; }


    }
}
