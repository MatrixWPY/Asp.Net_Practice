CREATE TABLE [dbo].[Tbl_ContactInfo] (
    [ContactInfoID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (10)  NOT NULL,
    [Nickname]      NVARCHAR (10)  NULL,
    [Gender]        TINYINT        NULL,
    [Age]           TINYINT        NULL,
    [PhoneNo]       VARCHAR (20)   NOT NULL,
    [Address]       NVARCHAR (100) NOT NULL,
    [IsEnable]      BIT            NOT NULL,
    [CreateTime]    DATETIME       NOT NULL,
    [UpdateTime]    DATETIME       NULL
);