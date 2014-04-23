using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueFitProjectTracker.Models.Cache;
using TrueFitProjectTracker.Models.Dashboard;
using TrueFitProjectTracker.Models.DataContext;

namespace TrueFitProjectTracker.Factories.Dashboard {
    public class TasksFactory : JiraFactory {
		private const string API_FIELD_LIST = "field";
		private const string API_ISSUES_LIST = "search?jql=project=";
		private FieldsDataContext db;
		private const string EPIC_FIELD_NAME = "Epic Link";
		public List<SprintModel> list {
			get {
				return sprints.Values.ToList<SprintModel>();
			}
		}
		private const string SPRINT_FIELD_NAME = "Sprint";
		private Dictionary<string, SprintModel> sprints;

		public TasksFactory(string projectKey) : base() {
			db = new FieldsDataContext();
			sprints = new Dictionary<string, SprintModel>();

		//Does the Jira fields cache need updated?
			if (!cacheUpdated()) {
				updateCache();
			}

		//Fetch the data from the Jira API
			fetchTasks(projectKey);
		}

		private void addToSprint(string sprintData, TaskModel tm) {
			string name = sprintData == null ? "Unassigned" : getSprintName(sprintData);

		//A new sprint may need to be created
			if (sprintData == null && !sprints.ContainsKey(name)) {
				DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Unix epoch

				sprints[name] = new SprintModel();
				sprints[name].CompleteDate = epoch;
				sprints[name].EndDate = epoch;
				sprints[name].Name = name;
				sprints[name].StartDate = epoch;
				sprints[name].State = "FUTURE";
				sprints[name].Tasks = new List<TaskModel>();
			} else if (!sprints.ContainsKey(name)) {
				sprints[name] = createSprint(sprintData);
			}

			sprints[name].Tasks.Add(tm);
		}

	/// <summary>
	/// The local cache will hold two values if the cache is up to date,
	/// one for the Sprints field, and another for the Epic field. This
	/// function is used to check if this condition holds.
	/// </summary>
	/// 
	/// <returns>Whether or not the expected cache entries exist.</returns>
		private bool cacheUpdated() {
			FieldsModel[] list = db.Fields.ToArray();
			
			return list.Length == 2;
		}

		private SprintModel createSprint(string sprintData) {
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Unix epoch
			SprintModel sm = new SprintModel();

		//This is a comma delimited list
			string[] parts = sprintData.Split(',');
			string[] small;

		//Completion date
			small = parts[5].Split('=');

			if (small[1] == "<null>") {
				sm.CompleteDate = epoch;
			} else {
				sm.CompleteDate = DateTime.Parse(small[1]);
			}

		//End date
			small = parts[4].Split('=');

			if (small[1] == "<null>") {
				sm.EndDate = epoch;
			} else {
				sm.EndDate = DateTime.Parse(small[1]);
			}

		//Name
			small = parts[2].Split('=');
			sm.Name = small[1];

		//Start date
			small = parts[3].Split('=');

			if (small[1] == "<null>") {
				sm.StartDate = epoch;
			} else {
				sm.StartDate = DateTime.Parse(small[1]);
			}

		//State
			small = parts[1].Split('=');
			sm.State = small[1];

		//Tasks
			sm.Tasks = new List<TaskModel>();

			return sm;
		}

		private void fetchTasks(string projectKey) {
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Unix epoch

		//Fetch all of the tasks for a particular project
			Object issues = Jira.RPC(API_ISSUES_LIST + HttpUtility.UrlEncode("\"" + projectKey + "\""));
			Dictionary<String, Object> parent = issues as Dictionary<String, Object>;

		//Fetch the field name holding the Epic
			FieldsModel epicDB = db.Fields.Find(EPIC_FIELD_NAME);
			string epicField = epicDB.ID;

		//Fetch the field name holding the Sprint
			FieldsModel sprintDB = db.Fields.Find(SPRINT_FIELD_NAME);
			string sprintField = sprintDB.ID;

		//Iterate over all of the issues
			IEnumerable<Object> issueList = parent["issues"] as IEnumerable<Object>;

			foreach(Dictionary<String, Object> issue in issueList) {
				TaskModel tm = new TaskModel();

			//Parse the data from each of the Jira fields, as supplied by the API
				Dictionary<String, Object> fields = issue["fields"] as Dictionary<String, Object>;

			//Creation date
				string created = fields["created"] as string;

				if (created == null) {
					tm.Created = epoch;
				} else {
					tm.Created = DateTime.Parse(fields["created"] as string);
				}				

			//Description
				tm.Description = fields["description"] as string;

			//Due date
				string due = fields["duedate"] as string;

				if (due == null) {
					tm.DueDate = epoch;
				} else {
					tm.DueDate = DateTime.Parse(fields["duedate"] as string);
				}

			//Epic
				tm.Epic = fields[epicField] as string;

			//Issue
				Dictionary<String, Object> issueDetails = fields["issuetype"] as Dictionary<String, Object>;
				tm.Issue = issueDetails["name"] as string;

			//Name
				tm.Name = fields["summary"] as string;

			//Percent
				Dictionary<String, Object> percentDetails = fields["progress"] as Dictionary<String, Object>;

				if (percentDetails.ContainsKey("percent")) {
					tm.Percent = (int)percentDetails["percent"];
				} else {
					tm.Percent = 0;
				}

			//Priority
				Dictionary<String, Object> priorityDetails = fields["priority"] as Dictionary<String, Object>;
				tm.Priority = priorityDetails["name"] as string;

			//Resolution
				if (fields["resolution"] == null) {
					fields["resolution"] = null;
				} else {
					Dictionary<String, Object> resolutionDetails = fields["resolution"] as Dictionary<String, Object>;
					tm.Resolution = resolutionDetails["name"] as string;
				}

			//Resolution date
				string resolution = fields["resolutiondate"] as string;

				if(resolution == null) {
					tm.ResolutionDate = epoch;
				} else {
					tm.ResolutionDate = DateTime.Parse(fields["resolutiondate"] as string);
				}

			//Status
				Dictionary<String, Object> statusDetails = fields["status"] as Dictionary<String, Object>;
				tm.Status = statusDetails["name"] as string;

			//Add this task to the appropriate sprint
				object[] sprint = fields[sprintField] as object[];

				if (sprint == null) {
					addToSprint(null, tm);
				} else {
					addToSprint(sprint[0] as string, tm);
				}
			}

		//Sort the tasks
			foreach (SprintModel sm in list) {
				sm.Tasks.Sort((x, y) => x.Created.CompareTo(y.Created));
			}
		}

		private string getSprintName(string sprintData) {
		//This is a comma delimited list
			string[] parts = sprintData.Split(',');
			string[] small;

		//Name
			small = parts[2].Split('=');
			return small[1];
		}

		private void updateCache() {
		//Fetch the list of available fields
			Object fields = Jira.RPC(API_FIELD_LIST);
			IEnumerable<Object> fieldList = fields as IEnumerable<Object>;

		//Search for the desired field name in the list
			int found = 0;
			string name;

			foreach(Dictionary<String, Object> field in fieldList) {
				name = field["name"] as string;

			//Log the desired field
				if (name == EPIC_FIELD_NAME || name == SPRINT_FIELD_NAME) {
				//Collect the data
					FieldsModel fm = new FieldsModel();
					fm.ID = field["id"] as string;
					fm.Name = name;

				//Log the transaction
					db.Fields.Add(fm);

					++found;
				}

			//Don't over iterate!
				if (found == 2) break;
			}

			db.SaveChanges();
		}
    }
}