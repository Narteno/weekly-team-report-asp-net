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
            Company company = new Company(1,"ANKO Corp", DateTime.Now);
            company.CompanyName = "Corp ANKO";
            Assert.Equal("Corp ANKO", company.CompanyName);
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
