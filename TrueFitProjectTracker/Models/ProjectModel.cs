using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueFitProjectTracker.Models
{
    public class ProjectModel
    {

        ProjectModel()
        {

        }

        public ProjectModel(int id)
        {
            Title = "New Project";
        }

        public string Title { get; set; }
        
    }
}