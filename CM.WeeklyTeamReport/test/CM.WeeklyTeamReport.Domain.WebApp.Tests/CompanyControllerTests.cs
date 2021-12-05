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
    public class CompanyControllerTests
    {
        [Fact]
        public void ShouldReturnAllCompanies()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.ReadAll())
                .Returns(new List<Company>()
                {
                    new Company(1, "ANKO", DateTime.Now),
                    new Company(2, "ANKO corp", DateTime.Now)
                });

            var controller = fixture.GetCompanyController();
            var actionResult = controller.GetAll();
            actionResult.Should().BeOfType<OkObjectResult>();

            var companies = (List<Company>)((actionResult as ObjectResult).Value);
            companies.Should().NotBeNull();
            companies.Should().HaveCount(2);
            fixture.CompanyRepository.Verify(x => x.ReadAll(), Times.Once);
        }
        [Fact]
        public void ShouldReturnNotFoundOnGetAllCompanies()
        {
            var fixture = new CompanyControllerFixture();
            var controller = fixture.GetCompanyController();
            var actionResult = controller.GetAll();
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.CompanyRepository.Verify(x => x.ReadAll(), Times.Once);
        }

        [Fact]
        public void ShouldReturnCompany()
        {
            var fixture = new CompanyControllerFixture();
            fixture.CompanyRepository
                .Setup(x => x.Read(5))
                .Returns(new Company(5, "ANKO", DateTime.Now));

            var controller = fixture.GetCompanyController();
            var actionResult = controller.Get(5);
            actionResult.Should().BeOfType<OkObjectResult>();

            var company = (Company)((actionResult as ObjectResult).Value);
            company.Should().NotBeNull();
            fixture.CompanyRepository.Verify(x => x.Read(5), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnGetCompany()
        {
            var fixture = new CompanyControllerFixture();
            var controller = fixture.GetCompanyController();
            var actionResult = controller.Get(0);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.CompanyRepository.Verify(x => x.Read(0), Times.Once);
        }

        [Fact]
        public void ShouldAddCompany()
        {
            var fixture = new CompanyControllerFixture();
            var company = new Company(5, "ANKO", DateTime.Now);
            fixture.CompanyRepository
                .Setup(x => x.Create(company))
                .Returns(company);

            var controller = fixture.GetCompanyController();
            var actionResult = controller.Post(company);
            actionResult.Should().BeOfType<OkObjectResult>();
            company = (Company)((actionResult as ObjectResult).Value);
            company.Should().NotBeNull();
            fixture.CompanyRepository.Verify(x => x.Create(company), Times.Once);
        }

        [Fact]
        public void ShouldReturnBadRequestOnAddCompany()
        {
            var fixture = new CompanyControllerFixture();
            var company = new Company(5, "ANKO", DateTime.Now);
            fixture.CompanyRepository
                .Setup(x => x.Create(company));

            var controller = fixture.GetCompanyController();
            var actionResult = controller.Post(company);
            actionResult.Should().BeOfType<BadRequestResult>();
            fixture.CompanyRepository.Verify(x => x.Create(company), Times.Once);
        }

        [Fact]
        public void ShouldUpdateCompany()
        {
            var fixture = new CompanyControllerFixture();
            var company = new Company(5, "ANKO", DateTime.Now);
            fixture.CompanyRepository
                .Setup(x => x.Read(company.CompanyId))
                .Returns(company);
            fixture.CompanyRepository
                .Setup(x => x.Update(company));

            var controller = fixture.GetCompanyController();
            var actionResult = controller.Put(company);
            actionResult.Should().BeOfType<OkResult>();
            fixture.CompanyRepository.Verify(x => x.Update(company), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnUpdateCompany()
        {
            var fixture = new CompanyControllerFixture();
            var company = new Company(5, "ANKO", DateTime.Now);
            fixture.CompanyRepository
                .Setup(x => x.Read(company.CompanyId));
            fixture.CompanyRepository
                .Setup(x => x.Update(company));

            var controller = fixture.GetCompanyController();
            var actionResult = controller.Put(company);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.CompanyRepository.Verify(x => x.Update(company), Times.Never);
        }

        [Fact]
        public void ShouldDeleteCompany()
        {
            var fixture = new CompanyControllerFixture();
            var company = new Company(5, "ANKO", DateTime.Now);
            fixture.CompanyRepository
                .Setup(x => x.Read(company.CompanyId))
                .Returns(company);
            fixture.CompanyRepository
                .Setup(x => x.Delete(company.CompanyId));

            var controller = fixture.GetCompanyController();
            var actionResult = controller.Delete(company.CompanyId);
            actionResult.Should().BeOfType<OkResult>();
            fixture.CompanyRepository.Verify(x => x.Delete(company.CompanyId), Times.Once);
        }

        [Fact]
        public void ShouldReturnNotFoundOnDeleteCompany()
        {
            var fixture = new CompanyControllerFixture();
            var company = new Company(5, "ANKO", DateTime.Now);
            fixture.CompanyRepository
                .Setup(x => x.Read(company.CompanyId));
            fixture.CompanyRepository
                .Setup(x => x.Delete(company.CompanyId));

            var controller = fixture.GetCompanyController();
            var actionResult = controller.Delete(company.CompanyId);
            actionResult.Should().BeOfType<NotFoundResult>();
            fixture.CompanyRepository.Verify(x => x.Delete(company.CompanyId), Times.Never);
        }
    }
    public class CompanyControllerFixture
    {
        public CompanyControllerFixture()
        {
            CompanyRepository = new Mock<IRepository<Company>>();
        }

        public Mock<IRepository<Company>> CompanyRepository { get; private set; }

        public CompanyController GetCompanyController()
        {
            return new CompanyController(CompanyRepository.Object);
        }
    }
}
