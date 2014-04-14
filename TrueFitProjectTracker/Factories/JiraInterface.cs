using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace TrueFitProjectTracker.Factories {
/// <summary>
/// The <c>JiraInterface</c> class is designed to communicate with a Jira
/// server via its REST API. The Atlassian .NET SDK is not capable of making
/// custom calls to the Jira REST API. This class allows any API URL to be 
/// queried and automatically return a C# <c>Object</c> based on the JSON
/// which was recieved from the API.
/// </summary>
	public class JiraInterface {
	/// <summary>
	/// Specify the base URL to the REST API on the Jira server.
	/// </summary>
		private const string API_BASE = "rest/api";

	/// <summary>
	/// As older versions of the Jira REST API are replaced with newer versions,
	/// the API version will change. Specify that version here. Use "latest" to 
	/// always force the usage of the latest REST API version. Using "latest" is
	/// not recommended, since the interface or returned data may change with
	/// later API versions, leaving the application unable to account for these
	/// sudden changes.
	/// </summary>
		private const string API_VERSION = "2";

	/// <summary>
	/// The reference to the REST API client.
	/// </summary>
		RestClient client;

	/// <summary>
	/// The URL to access a Jira server.
	/// </summary>
		private string URL;
		
		public JiraInterface(string URL, string username, string password) {
		//Save the parameters
			this.URL = URL;

		//Does the URL have a trailing slash? It should!
			if(!this.URL.EndsWith("/")) {
				this.URL += "/";
			}

		//Log into the Jira server
			authenticate(username, password);
		}

	/// <summary>
	/// All API calls to the Jira server must be authenticated. This function
	/// will attempt to authenticate using the given credentials.
	/// </summary>
	/// 
	/// <param name="username">The user name to an account on the Jira server.</param>
	/// <param name="password">The password to an account on the Jira server.</param>
		private void authenticate(string username, string password) {
			client = new RestClient(URL);
			client.Authenticator = new HttpBasicAuthenticator(username, password);
		}

	/// <summary>
	/// API requests must be translated to a fully qualified URL before being
	/// sent to a Jira server. This function will construct the known information
	/// about a particular Jira server and API version, and return the path name
	/// to the API call, without the domain name, as this information is assembled
	/// elseware.
	/// </summary>
	/// 
	/// <param name="pathURL">The path to the API request, without the domain name or API version.</param>
	/// 
	/// <returns>The path to the API call, without the domain name.</returns>
		protected string constructRequest(string pathURL) {
			return API_BASE + "/" + API_VERSION + "/" + pathURL;
		}

	/// <summary>
	/// Once the local server has been authenticated with Jira, subsequent calls can 
	/// be made to the REST API.
	/// 
	/// This function is designed to make a synchronous request to the Jira REST
	/// API, and return the results. The JSON will be deserialized and made available
	/// in the form of an <c>Object</c>. Default requests will use the GET method, but
	/// POST requests are also supported. A <c>NameValueCollection</c> can be used 
	/// to send GET or POST data with a request.
	/// </summary>
	/// 
	/// <param name="pathURL">The path to the API request, without the domain name or API version.</param>
	/// <param name="method">Either <c>Method.GET</c> (default) or <c>Method.POST</c> for the request type.</param>
	/// <param name="data">An optional key, value pair of parameters to send with the request.</param>
	/// 
	/// <returns>An Object from a deserialized JSON string from the REST API.</returns>
		public Object RPC(string pathURL, Method method = Method.GET, NameValueCollection data = null) {
			string path = constructRequest(pathURL);

		//Create the request to the Jira server
			RestRequest request = new RestRequest(path, Method.GET);

		//Assemble the request parameters
			if (data != null) {
				foreach(string key in data) {
					request.AddParameter(key, data[key]);
				}
			} else if (data == null && method == Method.POST) {
				throw new Exception("The RPC data value cannot be null when issuing a POST request.");
			}

			IRestResponse rResponse = client.Execute(request);
			string response = rResponse.Content;

		//Deserialize the JSON string
			JavaScriptSerializer jss = new JavaScriptSerializer();
			Object JSON = jss.Deserialize<Object>(response);
			return JSON;
		}
	}
}