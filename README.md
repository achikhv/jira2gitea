# Jira2Gitea import tool

Imports jira issues to gitea SQL Server database.

Imports:

- Issues
- Reporters
- Assignees
- Labels
- Milestones
- Dependencies

## Running

```
dotnet run -- --JiraServer https://my.jira.server --JiraUser jiraUsername --JiraPassword jiraPass --JiraFilter jiraIssueFilterId --DbServer gitea.sql.server --DbName giteaDbName --DbUser sqlServerUser --DbPass sqlServerPass
```
