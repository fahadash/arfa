ALTER TABLE [dbo].[Game]
    ADD CONSTRAINT [DF_Game_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

