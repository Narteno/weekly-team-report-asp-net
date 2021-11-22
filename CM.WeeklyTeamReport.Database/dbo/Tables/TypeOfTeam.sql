CREATE TABLE [dbo].[TypeOfTeam] (
    [TypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (20) NULL,
    CONSTRAINT [PK_TypeId] PRIMARY KEY CLUSTERED ([TypeId] ASC)
);

