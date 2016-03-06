CREATE TABLE [dbo].[GameScore]
(
	[TableId] INT NOT NULL , 
    [UserId] INT NOT NULL, 
    [Score] INT NOT NULL, 
    PRIMARY KEY ([TableId], [UserId]), 
    CONSTRAINT [FK_GameScore_Table] FOREIGN KEY ([TableId]) REFERENCES [Table]([TableId]), 
    CONSTRAINT [FK_GameScore_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
