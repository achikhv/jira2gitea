using System.Text.Json.Serialization;

namespace GenerateWhatsnew.Jira;

class IssueResponse
{
	public string? Expand { get; set; }

	public long StartAt { get; set; }

	public long MaxResults { get; set; }

	public long Total { get; set; }

	public List<Issue>? Issues { get; set; }
}

class Issue
{
	public string Key { get; set; }

	public IssueFields? Fields { get; set; }

	public string Self { get; set; }

	public override string? ToString() => Key;

	public Issue(string key, string self)
	{
		Key = key;
		Self = self;
	}
}

class IssueFields
{
	public string? Created { get; set; }
	public string? Updated { get; set; }

	public string? Description { get; set; }

	/// <summary>
	/// Release note comments.
	/// </summary>
	[JsonPropertyName("customfield_11100")]
	public string? ReleaseComment { get; set; }

	/// <summary>
	/// Тема на форуме.
	/// </summary>
	[JsonPropertyName("customfield_10100")]
	public string? Forum { get; set; }

	/// <summary>
	/// Шаги по воспроизведению.
	/// </summary>
	[JsonPropertyName("customfield_10035")]
	public string? ReproduceSteps { get; set; }

	public IssueComponent[]? Components { get; set; }

	public string[]? Labels { get; set; }

	public UserInfo? Creator { get; set; }

	public UserInfo? Reporter { get; set; }

	public UserInfo? Assignee { get; set; }

	public string? Summary { get; set; }

	public Resolution? Resolution { get; set; }

	public IssueLink[]? issuelinks { get; set; }

	public FixVersion[]? FixVersions { get; set; }

	public IssueType? issuetype { get; set; }
}


class Resolution
{
	public string? Name { get; set; }
}

class IssueType
{
	public string Name { get; set; }

	public IssueType(string name)
	{
		Name = name;
	}
}


class FixVersion
{
	public string Name { get; set; }

	public string? ReleaseDate { get; }

	public bool Released { get; set; }

	public string? Description { get; set; }

	public FixVersion(string name, string? releaseDate)
	{
		Name = name;
		ReleaseDate = releaseDate;
	}
}

class UserInfo
{
	public string Name { get; set; }

	public string EmailAddress { get; set; }

	public string DisplayName { get; set; }

	public UserInfo(string name, string emailAddress, string displayName)
	{
		Name = name;
		EmailAddress = emailAddress;
		DisplayName = displayName;
	}
}

class IssueComponent
{
	public string? Name { get; set; }
}

class IssueLink
{
	public InwardIssueInfo InwardIssue { get; set; }

	public IssueLink(InwardIssueInfo inwardissue)
	{
		InwardIssue = inwardissue;
	}
}


class InwardIssueInfo
{
	public string Key { get; set; }

	public InwardIssueInfo(string key)
	{
		Key = key;
	}
}

class IssueDetails
{
	public string Key { get; set; }

	public IssueDetails(string key, IssueDetailFields fields)
	{
		Key = key;
		Fields = fields;
	}

	public IssueDetailFields Fields { get; set; }
}

class IssueDetailFields
{
	public IssueCommentCollection? Comment { get; set; }
}


class IssueCommentCollection
{
	public IssueComment[]? Comments { get; set; }

}
class IssueComment
{
	public UserInfo Author { get; set; }

	public string Body { get; set; }

	public string Created { get; set; }

	public string Updated { get; set; }

	public IssueComment(UserInfo author, string body, string created, string updated)
	{
		Author = author;
		Body = body;
		Created = created;
		Updated = updated;
	}
}
