CREATE TABLE [dbo].[TableUser]
(
	[TableUserId] INT NOT NULL IDENTITY(1,1),
	[TableId] INT NOT NULL , 
    [UserId] INT NOT NULL, 
    [Score] INT NOT NULL DEFAULT 0, 
    [IsDominant] BIT NOT NULL DEFAULT 0, 
    [IsTurnPlayer] BIT NULL, 
    CONSTRAINT [FK_TableUser_ToTable] FOREIGN KEY ([TableId]) REFERENCES [Table]([TableId]), 
    CONSTRAINT [FK_TableUser_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]), 
    CONSTRAINT [PK_TableUser] PRIMARY KEY ([TableUserId])
)
