using System;

namespace mantis_tests
{
    public class ApiHelper : HelperBase
    {
        public ApiHelper(ApplicationManager applicationManager) : base(applicationManager)
        {
        }

        public void CreateNewIssue(AccountData account, ProjectData projectData, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = projectData.Id;

            client.mc_issue_add(account.Name, account.Password, issue);
        }
    }
}
