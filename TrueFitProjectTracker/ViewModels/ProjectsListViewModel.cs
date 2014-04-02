using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrueFitProjectTracker.Models;
using Atlassian.Jira;

namespace TrueFitProjectTracker.ViewModels
{
    public class ProjectsListViewModel
    {

        public ProjectsListViewModel(ProjectsListModel projectsList, Jira jira)
        {
            // TODO: Load Data from JIRA
            var projects = jira.GetProjects();
            for (int i = 0; i < projects.Count(); ++i)
            {
                var project1 = projects.ElementAt(i);
                string currentProject = project1.Name;
                int projID = Convert.ToInt32(project1.Id);
                this.projectsList.Add(new ProjectEntry(projID, project1.Name, project1.Lead, 56));
            }


        }

        private int toUInt32(string p)
        {
            throw new NotImplementedException();
        }

        private int toInt32(string p)
        {
            throw new NotImplementedException();
        }

        public List<ProjectEntry> projectsList = new List<ProjectEntry>();
    }



    
    public class ProjectEntry
    {
        // represents one project item in a list
        public ProjectEntry() { }
        public ProjectEntry(int id, string name, string clientName, int totalProgress)
        {
            this.id = id;
            this.name = name;
            this.clientName = clientName;
            this.totalProgress = totalProgress;
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
        public int TotalProgress
        {
            get { return totalProgress; }
            set { totalProgress = value; }
        }   

        int id = 0;
        int totalProgress = 0;
        string name = "";
        string clientName;
        
    }
}
