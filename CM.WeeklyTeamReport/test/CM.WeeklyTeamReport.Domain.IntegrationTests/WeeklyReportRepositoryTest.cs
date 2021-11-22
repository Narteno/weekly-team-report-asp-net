using System;
using Xunit;
using CM.WeeklyTeamReport.Domain.Repositories;

namespace CM.WeeklyTeamReport.Domain.IntegrationTests
{
    public class WeeklyReportRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateWeeklyReportRepository()
        {
            var reportRepository = new WeeklyReportRepository();
            Assert.NotNull(reportRepository);
        }
        [Fact]
        public void ShouldBeAbleToCreateReportIntoDatabase()
        {
            var reportRepository = new WeeklyReportRepository();
            var report = new WeeklyReport(Condition.Good, "I'm fine", Condition.Okay, "I'm okay", Condition.Low,"I'm low",
                "I'm done", "nothing", "none", DateTime.Now);
            report.FK_TeamMemberId = 6;
            report = reportRepository.Create(report);
        }
        [Fact]
        public void ShouldBeAbleToReadReportFromDatabase()
        {
            var reportRepository = new WeeklyReportRepository();
            var reportFromDB = reportRepository.Read(4);
            Assert.Equal("I'm fine", reportFromDB.moraleMsg);
            Assert.True(reportFromDB.ReportId == 4);
        }
        [Fact]
        public void ShouldBeAbleToUpdateReportFromDatabase()
        {
            var reportRepository = new WeeklyReportRepository();
            var report = new WeeklyReport(Condition.Good, "Morale message", Condition.Great, "Stress message",
                Condition.VeryLow, "Workload message", "Your high message", "Your low message", "Any message",
                DateTime.Now);
            report.ReportId = 8;
            reportRepository.Update(report);
            report = reportRepository.Read(8);
            Assert.Equal(Condition.Good, report.Morale);
            Assert.True(report.moraleMsg == "Morale message");
        }
        [Fact]
        public void ShouldBeAbleToDeleteCompanyFromDatabase()
        {
            var reportRepository = new WeeklyReportRepository();
            reportRepository.Delete(9);
            var report = reportRepository.Read(9);
            Assert.Null(report);
        }
    }
}
