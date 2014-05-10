TrueFit Project Tracker
===============

Installation or Transfer Instructions
---

1. Click on the [Releases][1] tab above
2. Download the latest installation package, currently `1.0.0`
3. Extract the `zip` or `tar.gz` file
4. Build the application from inside of Visual Studio. The requirements to build this application are mentioned in the section below.
5. Open a file explorer, and navigate to: `<application root folder>/App_Data`
6.	Create a new file and call it `jira.dat`.
7.	If the site is undergoing a transfer, then delete the existing `jira.dat` and replace it with a new, empty one.
8.	If the site is undergoing a transfer, and this folder has a file named `fields.dat`, delete it. The TrueFit Project Tracker will automatically rebuild it once the application has been moved
9.	Open `jira.dat`
10.	Enter only the following three pieces of information, separated by new lines:
    1.	The Jira installation URL, with a trailing slash: `http://example.jira.net/`
    2.	The username of an existing user in the Jira system
    3.	The password of an existing user
11.	Save and close the file
12.	Run or deploy the application, and it will connect and return information from the specified Jira instance

System Requirements to Build Application
---

- Microsoft Visual Studio 2012 or newer, with the C# and Web Development packages installed
- Microsoft .NET Framework version 4.5.0 or newer


[1]: https://github.com/ffiadmin/truefit-project/releases
