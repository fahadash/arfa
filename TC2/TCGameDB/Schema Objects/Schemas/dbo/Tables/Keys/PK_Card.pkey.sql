﻿ALTER TABLE [dbo].[Card]
    ADD CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED ([CardId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

