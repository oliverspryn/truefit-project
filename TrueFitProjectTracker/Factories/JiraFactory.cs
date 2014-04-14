using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TrueFitProjectTracker.Factories {
/// <summary>
/// The <c>JiraFactory</c> class is an abstract base class which is designed
/// to open the local crdentials configuration file and use the information 
/// contained inside to connect to a Jira server via its API.
/// 
/// Derived classes will be responsible for interacting with the Jira API
/// to query it for the desired information. This class is merely designed
/// to complete the preliminary actions which are required for later 
/// operations.
/// </summary>
	public abstract class JiraFactory {
	/// <summary>
	/// The name of the configuration file from which to read. This file must
	/// contain the following information, in the following order, each on
	/// their own lines:
	///  - Jira Server URL (with http(s)://)
	///  - User name
	///  - Password
	///  
	/// The path to the file will be relative to the project root directory.
	/// </summary>
		private const string CONFIG_FILE = "jira.dat";
		
	/// <summary>
	/// The Jira object which will interface with the Jira API.
	/// </summary>
		protected JiraInterface Jira;

	/// <summary>
	/// The password to an account on the Jira server, as defined by the 
	/// configuration file.
	/// </summary>
		private string Password;

	/// <summary>
	/// The URL to access a Jira server, as defined by the configuration file.
	/// </summary>
		private string URL;

	/// <summary>
	/// The user name to an account on the Jira server, as defined by the 
	/// configuration file.
	/// </summary>
		private string UserName;

	/// <summary>
	/// Bootstrap this functionality of this class by obtaining the Jira API
	/// connection credentials and connecting to the Jira server.
	/// </summary>
		public JiraFactory() {
			getCredentials();
			connect();
		}

	/// <summary>
	/// Connect to the Jira API using the credentials extracted from the
	/// configuration file.
	/// </summary>
		private void connect() {
			Jira = new JiraInterface(URL, UserName, Password);
		}

	/// <summary>
	/// Open the configuration file and store the contained Jira URL, username,
	/// and password to later connect to the Jira API.
	/// </summary>
	/// 
	/// <exception cref="System.ArgumentException"></exception>
	/// <exception cref="System.ArgumentNullException"></exception>
	/// <exception cref="System.IO.FileNotFounException"></exception>
	/// <exception cref="System.IO.DirectoryNotFoundException"></exception>
	/// <exception cref="System.IO.Exception"></exception>
		private void getCredentials() {
			string path = HostingEnvironment.ApplicationPhysicalPath + CONFIG_FILE;

			using (StreamReader sr = new StreamReader(path)) {
				URL = sr.ReadLine();
				UserName = sr.ReadLine();
				Password = sr.ReadLine();
			}
		}
	}
}