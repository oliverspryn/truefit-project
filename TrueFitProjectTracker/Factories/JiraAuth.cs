using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TrueFitProjectTracker.Factories {
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

		private static string Password;
		private static string UserName;
		private static string URL;

		public JiraAuth() : base(getURL(), getUserName(), getPassword()) { }

		private static string getPassword() { return Password; }

		private static string getURL() { return URL; }

		private static string getUserName() { return UserName; }

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
			private void openFile() {
				string path = HostingEnvironment.ApplicationPhysicalPath + CONFIG_FILE;

				using (StreamReader sr = new StreamReader(path)) {
					URL = sr.ReadLine();
					UserName = sr.ReadLine();
					Password = sr.ReadLine();
				}
			}
		}
	}
}