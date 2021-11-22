CREATE TABLE [dbo].[Company] (
    [CompanyId]  INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [JoinedDate] DATETIME      NULL,
    CONSTRAINT [PK_CompanyId] PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);

