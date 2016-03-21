CREATE TABLE [dbo].[TableHistory] (
    [TableHistoryId] BIGINT IDENTITY (1, 1) NOT NULL,
    [TableId]        INT    NOT NULL,
    [UserId]         INT    NOT NULL,
    [CardId]         INT    NOT NULL,
    CONSTRAINT [PK_TableHistory] PRIMARY KEY CLUSTERED ([TableHistoryId] ASC),
    CONSTRAINT [FK_TableHistory_Card] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card] ([CardId]),
    CONSTRAINT [FK_TableHistory_Table] FOREIGN KEY ([TableId]) REFERENCES [dbo].[Table] ([TableId]),
    CONSTRAINT [FK_TableHistory_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

