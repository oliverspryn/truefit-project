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
            this.projectsList.Add(new ProjectEntry(5, "Tree Project", "Reforesters United"));
            this.projectsList.Add(new ProjectEntry(7, "Tree Project", "Reforesters United"));
            this.projectsList.Add(new ProjectEntry(20, "Tree Project", "Reforesters United"));
            this.projectsList.Add(new ProjectEntry(21, "Tree Project", "Reforesters United"));
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


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }
        

        int id = 0;
        string name = "";
        string clientName;

    }
}
