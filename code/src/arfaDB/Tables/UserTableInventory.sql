CREATE TABLE [dbo].[UserTableInventory]
(
	[UserId] INT NOT NULL , 
    [TableId] INT NOT NULL, 
    [CardId] INT NOT NULL, 
    PRIMARY KEY ([UserId], [CardId], [TableId]), 
    CONSTRAINT [FK_UserTableInventory_User] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [FK_UserTableInventory_Table] FOREIGN KEY ([TableId]) REFERENCES [Table]([TableId]), 
    CONSTRAINT [FK_UserTableInventory_Card] FOREIGN KEY ([CardId]) REFERENCES [Card]([CardId])
)
