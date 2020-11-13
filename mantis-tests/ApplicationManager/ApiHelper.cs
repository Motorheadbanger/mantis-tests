using NUnit.Framework;
using System;
using System.Collections.Generic;

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

        public void Create(string projectName, AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projectData = new Mantis.ProjectData()
            {
                name = projectName
            };

            client.mc_project_add(account.Name, account.Password, projectData);
            driver.Navigate().Refresh();
        }

        public List<ProjectData> GetProjectList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projectList = client.mc_projects_get_user_accessible(account.Name, account.Password);
            List<ProjectData> list = new List<ProjectData>();

            foreach(Mantis.ProjectData item in projectList)
            {
                list.Add(new ProjectData
                {
                    Id = item.id,
                    Name = item.name
                });
            }

            return list;
        }
    }
}
