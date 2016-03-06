CREATE TABLE [dbo].[User] (
    [UserId]    INT          IDENTITY (1, 1) NOT NULL,
    [Username]  VARCHAR (50) NOT NULL,
    [Password]  VARCHAR (50) NOT NULL,
    [Firstname] VARCHAR (50) NULL,
    [Lastname]  VARCHAR (50) NULL,
    [Age]       INT          NULL,
    [Email]     VARCHAR (70) NULL
);

