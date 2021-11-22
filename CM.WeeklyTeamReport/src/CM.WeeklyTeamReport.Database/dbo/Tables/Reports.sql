CREATE TABLE [dbo].[Reports] (
    [ReportId]        INT            IDENTITY (1, 1) NOT NULL,
    [TeamMemberId]    INT            NULL,
    [MoraleId]        INT            NULL,
    [MoraleMessage]   NVARCHAR (300) NULL,
    [StressId]        INT            NULL,
    [StressMessage]   NVARCHAR (300) NULL,
    [WorkloadId]      INT            NULL,
    [WorkloadMessage] NVARCHAR (300) NULL,
    [YourHigh]        NVARCHAR (300) NULL,
    [YourLow]         NVARCHAR (300) NULL,
    [AnythingElse]    NVARCHAR (300) NULL,
    [DateOfReport]    DATETIME       NULL,
    CONSTRAINT [PK_ReportId] PRIMARY KEY CLUSTERED ([ReportId] ASC),
    CONSTRAINT [FK_MoraleId] FOREIGN KEY ([MoraleId]) REFERENCES [dbo].[Conditions] ([ConditionId]),
    CONSTRAINT [FK_StressId] FOREIGN KEY ([StressId]) REFERENCES [dbo].[Conditions] ([ConditionId]),
    CONSTRAINT [FK_TeamMemberId] FOREIGN KEY ([TeamMemberId]) REFERENCES [dbo].[TeamMembers] ([TeamMemberId]),
    CONSTRAINT [FK_WorkloadId] FOREIGN KEY ([WorkloadId]) REFERENCES [dbo].[Conditions] ([ConditionId])
);

