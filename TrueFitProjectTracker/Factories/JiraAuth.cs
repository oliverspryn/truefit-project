using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TrueFitProjectTracker.Factories {
/// <summary>
/// The <c>JiraAuth</c> class is a wrapper for the Atlassian Jira
/// .NET SDK. This class provides all of the functionality of the
/// Atlassian <c>Jira</c> class, but has the ability to read text file
/// containing the Jira server's URL, username, and password and
/// authenticate automatically.
/// </summary>
	public class JiraAuth : Jira {
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
		private const string CONFIG_FILE = "App_Data/jira.dat";

	/// <summary>
	/// Hold the password, in case this class is called again, it will save
	/// the expense of reading from disk.
	/// </summary>
		private static string Password;

	/// <summary>
	/// Hold the username, in case this class is called again, it will save
	/// the expense of reading from disk.
	/// </summary>
		private static string UserName;

	/// <summary>
	/// Hold the server URL, in case this class is called again, it will save
	/// the expense of reading from disk.
	/// </summary>
		private static string URL;

	/// <summary>
	/// The constructor will simply call the parent <c>Jira</c> class's 
	/// constructor, with the credentials already provided.
	/// </summary>
		public JiraAuth() : base(JiraAuth.getURL(), JiraAuth.getUserName(), JiraAuth.getPassword()) {  }

	/// <summary>
	/// Open the configuration file, fetch, then return the password contained
	/// inside.
	/// </summary>
	/// 
	/// <returns>The password to authenticate with Jira.</returns>
        private static string getPassword() {
			if (Password == null) openFile();
			return Password;
		}

	/// <summary>
	/// Open the configuration file, fetch, then return the username contained
	/// inside.
	/// </summary>
	/// 
	/// <returns>The username to authenticate with Jira.</returns>
		private static string getUserName() {
			if (UserName == null)
				openFile();
			return UserName;
		}

	/// <summary>
	/// Open the configuration file, fetch, then return the server URL contained
	/// inside.
	/// </summary>
	/// 
	/// <returns>The URL to the Jira server.</returns>
        private static string getURL() {
			if (URL == null) openFile();
			return URL;
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
		private static void openFile() {
			string path = HostingEnvironment.ApplicationPhysicalPath + CONFIG_FILE;

			try {
				using (StreamReader sr = new StreamReader(path)) {
					URL = sr.ReadLine();
					UserName = sr.ReadLine();
					Password = sr.ReadLine();
				}
			} catch (FileNotFoundException e) {
				throw new FileNotFoundException("The system's configuration file was not found. Refer to the application documentation to setup and configure this file.");
			}
		}
	}
}