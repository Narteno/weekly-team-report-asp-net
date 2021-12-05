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
    public class TeamMemberControllerTests
    {
        [Fact]
        public void ShouldBeAbleToCreateTeamMemberController()
        {
            var fixture = new TeamMemberControllerFixture();
            var controller = fixture.GetTeamMemberController();
        }

        [Fact]
        public void ShouldReturnAllTeamMembersByCompany()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository.Setup(x => x.ReadAllByParentId(1))
                .Returns(new List<TeamMember>()
                {
                    new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended)
                });

            var controller = fixture.GetTeamMemberController();

            var actionResult = controller.GetAllByCompanyId(1);
            actionResult.Should().BeOfType<OkObjectResult>();

            var teamMembers = (List<TeamMember>)((actionResult as ObjectResult).Value);
            teamMembers.Should().NotBeNull();
            teamMembers.Should().HaveCount(1);
            fixture.TeamMemberRepository.Verify(x => x.ReadAllByParentId(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnGetAllTeamMembers()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository.Setup(x => x.ReadAllByParentId(1));

            var controller = fixture.GetTeamMemberController();

            var actionResult = controller.GetAllByCompanyId(1);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.TeamMemberRepository.Verify(x => x.ReadAllByParentId(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository.Setup(x => x.Read(1))
                .Returns(new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended));

            var controller = fixture.GetTeamMemberController();

            var actionResult = controller.Get(1);
            actionResult.Should().BeOfType<OkObjectResult>();

            var teamMember = (TeamMember)((actionResult as ObjectResult).Value);
            teamMember.Should().NotBeNull();
            fixture.TeamMemberRepository.Verify(x => x.Read(1), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnGetTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            fixture.TeamMemberRepository.Setup(x => x.Read(1));

            var controller = fixture.GetTeamMemberController();

            var actionResult = controller.Get(1);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.TeamMemberRepository.Verify(x => x.Read(1), Times.Once);
        }

        [Fact]
        public void ShouldAddTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            TeamMember teamMember = new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended);
            fixture.TeamMemberRepository
                .Setup(x => x.Create(teamMember))
                .Returns(teamMember);

            var controller = fixture.GetTeamMemberController();
            var actionResult = controller.Post(teamMember);
            actionResult.Should().BeOfType<OkObjectResult>();
            fixture.TeamMemberRepository.Verify(x => x.Create(teamMember), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestOnAddTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            TeamMember teamMember = new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended);
            fixture.TeamMemberRepository
                .Setup(x => x.Create(teamMember));

            var controller = fixture.GetTeamMemberController();
            var actionResult = controller.Post(teamMember);
            actionResult.Should().BeOfType<BadRequestResult>();
            fixture.TeamMemberRepository.Verify(x => x.Create(teamMember), Times.Once);
        }

        [Fact]
        public void ShouldUpdateTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            TeamMember teamMember = new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended);
            fixture.TeamMemberRepository
                .Setup(x => x.Read(teamMember.TeamMemberId))
                .Returns(teamMember);
            fixture.TeamMemberRepository
                .Setup(x => x.Update(teamMember));

            var controller = fixture.GetTeamMemberController();
            var actionResult = controller.Put(teamMember);
            actionResult.Should().BeOfType<OkResult>();
            fixture.TeamMemberRepository.Verify(x => x.Update(teamMember), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundUpdateTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            TeamMember teamMember = new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended);
            fixture.TeamMemberRepository
                .Setup(x => x.Read(teamMember.TeamMemberId));
            fixture.TeamMemberRepository
                .Setup(x => x.Update(teamMember));

            var controller = fixture.GetTeamMemberController();
            var actionResult = controller.Put(teamMember);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.TeamMemberRepository.Verify(x => x.Update(teamMember), Times.Never);
        }

        [Fact]
        public void ShouldDeleteTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            TeamMember teamMember = new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended);
            fixture.TeamMemberRepository
                .Setup(x => x.Read(teamMember.TeamMemberId))
                .Returns(teamMember);
            fixture.TeamMemberRepository
                .Setup(x => x.Delete(teamMember.TeamMemberId));

            var controller = fixture.GetTeamMemberController();
            var actionResult = controller.Delete(teamMember.TeamMemberId);
            actionResult.Should().BeOfType<OkResult>();
            fixture.TeamMemberRepository.Verify(x => x.Delete(teamMember.TeamMemberId), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnDeleteTeamMember()
        {
            var fixture = new TeamMemberControllerFixture();
            TeamMember teamMember = new TeamMember(1, 1, "Ilya", "Krasnoperov", TypeOfTeam.Extended);
            fixture.TeamMemberRepository
                .Setup(x => x.Read(teamMember.TeamMemberId));
            fixture.TeamMemberRepository
                .Setup(x => x.Delete(teamMember.TeamMemberId));

            var controller = fixture.GetTeamMemberController();
            var actionResult = controller.Delete(teamMember.TeamMemberId);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.TeamMemberRepository.Verify(x => x.Delete(teamMember.TeamMemberId), Times.Never);
        }
    }
    public class TeamMemberControllerFixture
    {
        public Mock<IRepository<TeamMember>> TeamMemberRepository { get; set; }

        public TeamMemberControllerFixture()
        {
            TeamMemberRepository = new Mock<IRepository<TeamMember>>();
        }

        public TeamMemberController GetTeamMemberController()
        {
            return new TeamMemberController(TeamMemberRepository.Object);
        }
    }
}
