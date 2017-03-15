CREATE TABLE [dbo].[TableUserCard]
(
	[TableUserId] INT NOT NULL , 
    [CardId] INT NOT NULL, 
    CONSTRAINT [FK_TableUserCard_TableUser] FOREIGN KEY (TableUserId) REFERENCES [TableUser]([TableUserId]), 
    CONSTRAINT [FK_TableUserCard_Card] FOREIGN KEY ([CardId]) REFERENCES [Card]([CardId]), 
    PRIMARY KEY ([TableUserId], [CardId])
)
