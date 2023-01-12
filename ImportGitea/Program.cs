using System.Net.Http.Headers;
using System.Text;
using GenerateWhatsnew.Jira;
using ImportGitea;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
	.AddCommandLine(args);

var config = builder.Build();

string jiraServer = config["JiraServer"];
string jiraUser = config["JiraUser"];
string jiraPassword = config["JiraPassword"];
string jiraFilter = config["JiraFilter"];
string dbServer = config["DbServer"];
string dbName = config["DbName"];
string dbUser = config["DbUser"];
string dbUserPass = config["DbUserPass"];

if (!long.TryParse(config["RepoId"], out long repoId))
{
	repoId = 1;
}

using var client = new HttpClient
{
	BaseAddress = new Uri(jiraServer)
};

client.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Basic", Convert.ToBase64String(
			Encoding.ASCII.GetBytes(
			   $"{jiraUser}:{jiraPassword}")));

var csb = new SqlConnectionStringBuilder
{
	DataSource = dbServer,
	InitialCatalog = dbName,
	TrustServerCertificate = true,
	UserID = dbUser,
	Password = dbUserPass
};

using var sqlc = new SqlConnection(csb.ToString());
sqlc.Open();
var loader = new Loader(client);

var importer = new Importer(repoId, sqlc)
{
	RemoveExistingIssues = false
};

Console.WriteLine("Loading JIRA tasks...");
var issues = await loader.LoadIssues(jiraFilter);

long total = 0;

foreach (var issue in issues)
{
	importer.Add(issue);

	var details = await loader.LoadDetails(issue.Self);

	if (details != null)
	{
		importer.Add(details);
	}

	++total;

	Console.WriteLine($"Loaded {total} out of {issues.Count}");
}

Console.WriteLine($"Importing Jira tasks to gitea repoId = {repoId}...");

importer.Import();


return 0;
