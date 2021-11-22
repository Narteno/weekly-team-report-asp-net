using System;
using System.Collections.Generic;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class GeneralTests
    {
        [Fact]
        public void ShouldBeAbleToCreateTeamMember()
        {
            TeamMember teamMember = new TeamMember("Ilya", "Krasnoperov", TypeOfTeam.Extended);
            Assert.Equal("Ilya", teamMember.FirstName);
            Assert.Equal("Krasnoperov", teamMember.LastName);
            Assert.Equal(TypeOfTeam.Extended, teamMember.typeOfTeam);
        }
        [Fact]
        public void ShouldBeAbleToCreateCompany()
        {
            TeamMember teamMember = new TeamMember("Ilya", "Krasnoperov", TypeOfTeam.Immediate);
            Company company = new Company("ANKO Corp", DateTime.Now, new List<TeamMember> { teamMember });
            company.CompanyName = "Corp ANKO";
            Assert.Equal("Corp ANKO", company.CompanyName);
            company.TeamMembers[0].ChangeInformation(null,null);
            Assert.Equal("Ilya", company.TeamMembers[0].FirstName);
            Assert.Equal("Krasnoperov", company.TeamMembers[0].LastName);
            company.TeamMembers[0].ChangeInformation("I", "Kra");
            Assert.Equal("I", company.TeamMembers[0].FirstName);
            Assert.Equal("Kra", company.TeamMembers[0].LastName);
        }
        [Fact]
        public void ShouldBeAbleToCreateWeeklyReport()
        {
            WeeklyReport teamMemberReport = new WeeklyReport(Condition.Good, "Morale message", Condition.Great, "Stress message",
                Condition.VeryLow, "Workload message", "Your high message", "Your low message", "Any message",
                DateTime.Now);
            TeamMember teamMember = new TeamMember("Ilya", "Krasnoperov", TypeOfTeam.Extended);
            teamMember.CreateMemberReport(teamMemberReport);
            Assert.True(teamMember.teamMemberReports.Count == 1);
            teamMember.DeleteReport(0);
            Assert.True(teamMember.teamMemberReports.Count == 0);
        }
    }
}
