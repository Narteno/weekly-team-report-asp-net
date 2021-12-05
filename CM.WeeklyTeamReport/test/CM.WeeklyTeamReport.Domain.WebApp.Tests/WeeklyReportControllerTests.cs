using CM.WeeklyTeamReport.WebApp.Controllers;
using CM.WeeklyTeamReport.Domain.Repositories;
using FluentAssertions;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CM.WeeklyTeamReport.Domain.WebApp.Tests
{
    public class WeeklyReportControllerTests
    {
        [Fact]
        public void ShouldBeAbleToCreateWeeklyReportController()
        {
            var fixture = new WeeklyReportControllerFixture();
            var controller = fixture.GetWeeklyReportController();
        }

        [Fact]
        public void ShouldReturnAllWeeklyReportsByTeamMember()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository.Setup(x => x.ReadAllByParentId(1))
                .Returns(new List<WeeklyReport>()
                {
                    new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low","something else", DateTime.Now)
                });

            var controller = fixture.GetWeeklyReportController();

            var actionResult = controller.GetAllByMember(1);
            actionResult.Should().BeOfType<OkObjectResult>();

            var weeklyReports = (List<WeeklyReport>)((actionResult as ObjectResult).Value);
            weeklyReports.Should().NotBeNull();
            weeklyReports.Should().HaveCount(1);
            fixture.WeeklyReportRepository.Verify(x => x.ReadAllByParentId(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnGetAllWeeklyReportsByTeamMember()
        {
            var fixture = new WeeklyReportControllerFixture();
            fixture.WeeklyReportRepository.Setup(x => x.ReadAllByParentId(1));

            var controller = fixture.GetWeeklyReportController();

            var actionResult = controller.GetAllByMember(1);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.WeeklyReportRepository.Verify(x => x.ReadAllByParentId(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository.Setup(x => x.Read(1))
                .Returns(weeklyReport);

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Get(1);
            actionResult.Should().BeOfType<OkObjectResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Read(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnGetWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository.Setup(x => x.Read(1));

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Get(1);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Read(1), Times.Once);
        }

        [Fact]
        public void ShouldAddWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository
                .Setup(x => x.Create(weeklyReport))
                .Returns(weeklyReport);

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Post(weeklyReport);
            actionResult.Should().BeOfType<OkObjectResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Create(weeklyReport), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestOnAddWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository
                .Setup(x => x.Create(weeklyReport));

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Post(weeklyReport);
            actionResult.Should().BeOfType<BadRequestResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Create(weeklyReport), Times.Once);
        }

        [Fact]
        public void ShouldUpdateWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(weeklyReport.ReportId))
                .Returns(weeklyReport);
            fixture.WeeklyReportRepository
                .Setup(x => x.Update(weeklyReport));

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Put(weeklyReport);
            actionResult.Should().BeOfType<OkResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Update(weeklyReport), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnUpdateWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(weeklyReport.ReportId));
            fixture.WeeklyReportRepository
                .Setup(x => x.Update(weeklyReport));

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Put(weeklyReport);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Update(weeklyReport), Times.Never);
        }

        [Fact]
        public void ShouldDeleteWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(weeklyReport.ReportId))
                .Returns(weeklyReport);
            fixture.WeeklyReportRepository
                .Setup(x => x.Delete(weeklyReport.ReportId));

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Delete(weeklyReport.ReportId);
            actionResult.Should().BeOfType<OkResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Delete(weeklyReport.ReportId), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnDeleteWeeklyReport()
        {
            var fixture = new WeeklyReportControllerFixture();
            var weeklyReport = new WeeklyReport(1, 3, Condition.Good, "my morale string",
                                     Condition.Okay, "my stress string", Condition.VeryLow, "my workload string",
                                     "my high", "my low", "something else", DateTime.Now);
            fixture.WeeklyReportRepository
                .Setup(x => x.Read(weeklyReport.ReportId));
            fixture.WeeklyReportRepository
                .Setup(x => x.Delete(weeklyReport.ReportId));

            var controller = fixture.GetWeeklyReportController();
            var actionResult = controller.Delete(weeklyReport.ReportId);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.WeeklyReportRepository.Verify(x => x.Delete(weeklyReport.ReportId), Times.Never);
        }
    }
    public class WeeklyReportControllerFixture
    {
        public Mock<IRepository<WeeklyReport>> WeeklyReportRepository { get; set; }

        public WeeklyReportControllerFixture()
        {
            WeeklyReportRepository = new Mock<IRepository<WeeklyReport>>();
        }

        public WeeklyReportController GetWeeklyReportController()
        {
            return new WeeklyReportController(WeeklyReportRepository.Object);
        }
    }
}
