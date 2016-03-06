CREATE TABLE [dbo].[TableState]
(
	[TableId] INT NOT NULL , 
    [UserId] INT NOT NULL, 
    [CardId] INT NOT NULL, 
    PRIMARY KEY ([CardId], [UserId], [TableId]), 
    CONSTRAINT [FK_TableState_ToTable] FOREIGN KEY ([TableId]) REFERENCES [Table]([TableId]), 
    CONSTRAINT [FK_TableState_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_TableState_ToCard] FOREIGN KEY ([CardId]) REFERENCES [Card]([CardId])
)
