using System;
using Xunit;
using CM.WeeklyTeamReport.Domain.Repositories;

namespace CM.WeeklyTeamReport.Domain.IntegrationTests
{
    public class CompanyRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateRepository()
        {
            var companyRepository = new CompanyRepository();
            Assert.NotNull(companyRepository);
        }
        [Fact]
        public void ShouldBeAbleToCreateCompanyIntoDatabase()
        {
            var companyRepository = new CompanyRepository();
            var company = new Company("ANKO", DateTime.Now);
            company = companyRepository.Create(company);
        }
        [Fact]
        public void ShouldBeAbleToReadCompanyFromDatabase()
        {
            var companyRepository = new CompanyRepository();
            var companyFromDB = companyRepository.Read(1);
            Assert.Equal("ANKO",companyFromDB.CompanyName);
            Assert.True(companyFromDB.CompanyId == 1);
        }
        [Fact]
        public void ShouldBeAbleToUpdateCompanyFromDatabase()
        {
            var companyRepository = new CompanyRepository();
            var company = new Company("NOT ANKO", DateTime.Now);
            company.CompanyId = 3;
            companyRepository.Update(company);
            company = companyRepository.Read(3);
            Assert.Equal("NOT ANKO", company.CompanyName);
            Assert.True(company.CompanyId == 3);
        }
        [Fact]
        public void ShouldBeAbleToDeleteCompanyFromDatabase()
        {
            var companyRepository = new CompanyRepository();
            companyRepository.Delete(5);
            var company = companyRepository.Read(5);
            Assert.Null(company);
        }
    }
}
