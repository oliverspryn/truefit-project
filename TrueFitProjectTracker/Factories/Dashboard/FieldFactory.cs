using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using TrueFitProjectTracker.Models.Cache;

namespace TrueFitProjectTracker.Factories.Dashboard {
/// <summary>
/// The <c>FieldFactory</c> class is a class which is used to provide
/// the necessary information to enabled this application to identify
/// what the field IDs of an Epic and Sprint fields are.
/// 
/// When an API call is made to a Jira server, the API returns a series
/// of field IDs, along with their associated values. The Epic and
/// Sprint fields are hidden inside of fields with the IDs of 
/// "customfield_xxxxx", where xxxxx is a number greater than 10000.
/// 
/// This class will associate the human-readable name with the Jira 
/// field ID so the data can be effectively processed after an API call.
/// 
/// The results are cached locally, since these values will not change,
/// and will significantly reduce page load time since the server is
/// spared from making another API request to the Jira server for this
/// information.
/// </summary>
	public class FieldFactory {
	/// <summary>
	/// The path in the Jira API to the list of all available fields
	/// which are present in for a particular task.
	/// </summary>
		private const string API_FIELD_LIST = "field";

	/// <summary>
	/// The name and path, relative to the project root, which will act
	/// as a flat-file database to hold the names and IDs of each of the
	/// necessary Jira fields.
	/// </summary>
		private const string DB_FILE = "App_Data/fields.dat";

	/// <summary>
	/// The name of the Jira Task field which associates a task with a 
	/// particular epic.
	/// </summary>
		private const string EPIC_FIELD_NAME = "Epic Link";

	/// <summary>
	/// A reference to a JiraInterface which will make the API call to the 
	/// server.
	/// </summary>
		private JiraInterface Jira;

	/// <summary>
	/// The List<> of desired Jira fields, along with their system-level
	/// ID and human-friendly name.
	/// </summary>
		public List<FieldsModel> List;

	/// <summary>
	/// The name of the Jira Task field which associates a task with a 
	/// particular sprint.
	/// </summary>
		private const string SPRINT_FIELD_NAME = "Sprint";

	/// <summary>
	/// A StreamReader to read from the flat-file database.
	/// </summary>
		private StreamReader SR;

	/// <summary>
	/// A StreamWriter to write to the flat-file database, when necessary.
	/// </summary>
		private StreamWriter SW;

	/// <summary>
	/// The constructor will bootstrap the functionality of this factory
	/// class. The cache will be consulted for the desired data and made
	/// available via C# models. If the cache does not exist, Jira will be
	/// queried for the necessary data, then cached locally.
	/// </summary>
	/// 
	/// <param name="jira">A reference to a JiraInterface object to make API calls to a Jira server</param>
		public FieldFactory(JiraInterface jira) {
			List = new List<FieldsModel>();
			Jira = jira;
			string path = HostingEnvironment.ApplicationPhysicalPath + DB_FILE;

		//Try reading from the database file, or create it if it doesn't exist
			try {
				SR = new StreamReader(path);

				if (!cacheUpdated()) {
					updateCache();
				} else {
					extractFields();
				}
			} catch (Exception) {
				FileStream stream = new FileStream(path, FileMode.Create);
				SW = new StreamWriter(stream);
				updateCache();
			}
		}

	/// <summary>
	/// The local cache will hold four values if the cache is up to date,
	/// two for the Sprints field, and another two for the Epic field. This
	/// function is used to check if this condition holds.
	/// </summary>
	/// 
	/// <returns>Whether or not the expected cache entries exist.</returns>
		private bool cacheUpdated() {
			int i;

		//Count the lines in the file
			for(i = 0; SR.ReadLine() != null; ++i) { }

		//Reset the "cursor" back to the beginning of the file
			SR.BaseStream.Position = 0;
			SR.DiscardBufferedData();

		//Four lines for two fields, where each field has two pieces of information
			return i == 4;
		}

	/// <summary>
	/// Fetch field data from the local cache file and make its results
	/// available to the application within a <c>List<FieldsModel></c> object.
	/// </summary>
		private void extractFields() {
		//First field
			FieldsModel fm1 = new FieldsModel();
			fm1.ID = SR.ReadLine() as string;
			fm1.Name = SR.ReadLine() as string;

		//Second field
			FieldsModel fm2 = new FieldsModel();
			fm2.ID = SR.ReadLine() as string;
			fm2.Name = SR.ReadLine() as string;

		//Make these values publicly available
			List.Add(fm1);
			List.Add(fm2);

		//Close the StreamReader
			SR.Close();
		}

	/// <summary>
	/// Make an API call to the Jira server, save the targeted information
	/// in a local cache, and make its results available to the application
	/// within a <c>List<FieldsModel></c> object.
	/// </summary>
		private void updateCache() {
		//Fetch the list of available fields
			Object fields = Jira.RPC(API_FIELD_LIST);
			IEnumerable<Object> fieldList = fields as IEnumerable<Object>;

		//Search for the desired field name in the list
			int found = 0;
			string name;

			foreach (Dictionary<String, Object> field in fieldList) {
				name = field["name"] as string;

			//Log the desired field
				if (name == EPIC_FIELD_NAME || name == SPRINT_FIELD_NAME) {
				//Collect the data
					FieldsModel fm = new FieldsModel();
					fm.ID = field["id"] as string;
					fm.Name = name;

				//Log the transaction
					List.Add(fm);

					++found;
				}

			//Don't over iterate!
				if (found == 2)
					break;
			}

		//Write these values to the database file
			SW.WriteLine(List[0].ID);
			SW.WriteLine(List[0].Name);
			SW.WriteLine(List[1].ID);
			SW.WriteLine(List[1].Name);

		//Close the StreamWriter
			SW.Close();
		}
	}
}