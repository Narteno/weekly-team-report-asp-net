CREATE DATABASE WeeklyTeamReport
GO
USE WeeklyTeamReport

Create table [Company]
(
	CompanyId int IDENTITY(1,1) constraint [PK_CompanyId] primary key,
	Name nvarchar(50),
	JoinedDate datetime
)
Create table [TypeOfTeam]
(
	TypeId int IDENTITY(1,1) constraint [PK_TypeId] primary key,
	Name nvarchar(20)
)

Insert into [TypeOfTeam] (Name)
VALUES('Immediate'), ('Extended')

Create table [TeamMembers]
(
	TeamMemberId int IDENTITY(1,1) constraint [PK_TeamMemberId] primary key,
	CompanyId int constraint [FK_CompanyId] foreign key ([CompanyId]) references [Company]([CompanyId]),
	FirstName nvarchar(100),
	LastName nvarchar (100) not null,
	TypeId int constraint [FK_TypeId] foreign key ([TypeId]) references [TypeOfTeam]([TypeId])
)
Create table Conditions
(
	ConditionId int IDENTITY(1,1) constraint [PK_ConditionId] primary key,
	Name nvarchar(20)
)

INSERT INTO Conditions (Name)
Values('VeryLow'),('Low'),('Okay'),('Good'),('Great')

Create table [Reports]
(
	ReportId int IDENTITY(1,1) constraint [PK_ReportId] primary key,
	TeamMemberId int constraint [FK_TeamMemberId] foreign key ([TeamMemberId]) references [TeamMembers]([TeamMemberId]),
	MoraleId int constraint [FK_MoraleId] foreign key ([MoraleId]) references [Conditions]([ConditionId]),
	MoraleMessage nvarchar(300),
	StressId int constraint [FK_StressId] foreign key ([StressId]) references [Conditions]([ConditionId]),
	StressMessage nvarchar(300),
	WorkloadId int constraint [FK_WorkloadId] foreign key ([WorkloadId]) references [Conditions]([ConditionId]),
	WorkloadMessage nvarchar(300),
	YourHigh nvarchar(300),
	YourLow nvarchar(300),
	AnythingElse nvarchar(300),
	DateOfReport datetime
)