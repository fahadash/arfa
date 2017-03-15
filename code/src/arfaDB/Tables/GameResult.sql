CREATE TABLE [dbo].[GameResult]
(
	[TableId] INT NOT NULL , 
    [WinnerUserId] INT NOT NULL, 
    PRIMARY KEY ([WinnerUserId], [TableId]), 
    CONSTRAINT [FK_GameResult_ToTable] FOREIGN KEY ([TableId]) REFERENCES [Table]([TableId]), 
    CONSTRAINT [FK_GameResult_ToUser] FOREIGN KEY ([WinnerUserId]) REFERENCES [User]([UserId])
)
