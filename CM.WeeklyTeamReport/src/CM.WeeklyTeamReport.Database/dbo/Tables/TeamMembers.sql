CREATE TABLE [dbo].[TeamMembers] (
    [TeamMemberId] INT            IDENTITY (1, 1) NOT NULL,
    [CompanyId]    INT            NULL,
    [FirstName]    NVARCHAR (100) NULL,
    [LastName]     NVARCHAR (100) NOT NULL,
    [TypeId]       INT            NULL,
    CONSTRAINT [PK_TeamMemberId] PRIMARY KEY CLUSTERED ([TeamMemberId] ASC),
    CONSTRAINT [FK_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[TypeOfTeam] ([TypeId])
);

