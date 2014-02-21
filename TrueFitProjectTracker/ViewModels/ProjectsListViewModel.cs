using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrueFitProjectTracker.Models;

namespace TrueFitProjectTracker.ViewModels
{
    public class ProjectsListViewModel
    {

        public ProjectsListViewModel(ProjectsListModel projectsList)
        {
            // TODO: Load Data from JIRA

            this.projectsList.Add(new ProjectEntry(1, "Tree Project", "Reforesters United"));
        }

        public List<ProjectEntry> projectsList = new List<ProjectEntry>();
    }



    
    public class ProjectEntry
    {
        // represents one project item in a list
        public ProjectEntry() { }
        public ProjectEntry(int id, string name, string clientName)
        {
            this.id = id;
            this.name = name;
            this.clientName = clientName;
        }

        public int id = 0;
        public string name = "";
        public string clientName;

    }
}
