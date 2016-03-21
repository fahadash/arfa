CREATE TABLE [dbo].[Table]
(
	[TableId] INT NOT NULL PRIMARY KEY IDENTITY,
	[TableName] VARCHAR(100) NOT NULL,
    [OwnerUserId] INT NOT NULL, 
    [Created] DATETIME NOT NULL DEFAULT GETDATE(), 
    [Suspended] BIT NOT NULL DEFAULT 0, 
    [Timestamp] VARCHAR(200) NULL, 
    [CurrentSuit] VARCHAR(10) NULL, 
    [Trump] VARCHAR(10) NULL, 
    [GameStarted] BIT NOT NULL DEFAULT 0, 
    [HandsAccumulated] INT NOT NULL DEFAULT 0, 
    [TurnStart] BIT NOT NULL DEFAULT 1, 
    [LastWinnerUserId] INT NULL, 
    [LastWinnerTimestamp] DATETIME NULL, 
	[TrumpChooser] INT NULL,
    CONSTRAINT [FK_Table_User1] FOREIGN KEY ([LastWinnerUserId]) REFERENCES [User]([UserId]) 
)
