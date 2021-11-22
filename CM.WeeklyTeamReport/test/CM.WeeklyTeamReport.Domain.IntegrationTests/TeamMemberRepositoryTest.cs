using System;
using Xunit;
using CM.WeeklyTeamReport.Domain.Repositories;

namespace CM.WeeklyTeamReport.Domain.IntegrationTests
{
    public class TeamMemberRepositoryTest
    {
        [Fact]
        public void ShoulbBeAbleToCreateTeamMemberRepository()
        {
            var memberRepository = new TeamMemberRepository();
            Assert.NotNull(memberRepository);
        }
        [Fact]
        public void ShoulbBeAbleToCreateTeamMemberIntoDatabase()
        {
            var memberRepository = new TeamMemberRepository();
            var member = new TeamMember("Ilya", "Krasnoperov", (TypeOfTeam)1);
            member.FK_CompanyId = 3;
            member = memberRepository.Create(member);
        }
        [Fact]
        public void ShoulbBeAbleToReadTeamMemberFromDatabase()
        {
            var memberRepository = new TeamMemberRepository();
            var memberFromDB = memberRepository.Read(4);
            Assert.Equal("Ilya", memberFromDB.FirstName);
            Assert.True(memberFromDB.typeOfTeam == TypeOfTeam.Immediate);
            Assert.Equal(3,memberFromDB.FK_CompanyId);
        }
        [Fact]
        public void ShouldBeAbleToUpdateTeamMemberFromDatabase()
        {
            var memberRepository = new TeamMemberRepository();
            var member = new TeamMember("IlyaIlya", "Krasnoperov1111", (TypeOfTeam)2);
            member.TeamMemberId = 5;
            memberRepository.Update(member);
            member = memberRepository.Read(5);
            Assert.Equal("IlyaIlya", member.FirstName);
            Assert.Equal("Krasnoperov1111", member.LastName);
        }
        [Fact]
        public void ShouldBeAbleToDeleteCompanyFromDatabase()
        {
            var memberRepository = new TeamMemberRepository();
            memberRepository.Delete(8);
            var member = memberRepository.Read(8);
            Assert.Null(member);
        }
    }
}
