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

            this.projectsList.Add(new ProjectEntry(10001, "Tree Project", "Reforesters United", 27));
            this.projectsList.Add(new ProjectEntry(10005, "Mortal Wombats", "Capricornicopia", 78));
            this.projectsList.Add(new ProjectEntry(10007, "The Chunky Spaghetti Crisis", "Mr. Meatball Maniac", 12));
            this.projectsList.Add(new ProjectEntry(10020, "The 3/4 Done Club", "QuarterMain Studios",76));
            this.projectsList.Add(new ProjectEntry(20221, "Whale Nuker/Backpack Problem Solver", "Eli Houston IV",100));
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
