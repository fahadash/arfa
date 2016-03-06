ALTER TABLE [dbo].[GameState]
    ADD CONSTRAINT [FK_GameState_Player] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([PlayerId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

