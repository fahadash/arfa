CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Firstname] VARCHAR(50) NULL, 
    [Lastname] VARCHAR(50) NULL, 
    [Age] INT NULL,
	[Token] UNIQUEIDENTIFIER NULL,
	[TokenLastHit] DATETIME NULL
)
