CREATE TABLE [dbo].[Conditions] (
    [ConditionId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (20) NULL,
    CONSTRAINT [PK_ConditionId] PRIMARY KEY CLUSTERED ([ConditionId] ASC)
);

