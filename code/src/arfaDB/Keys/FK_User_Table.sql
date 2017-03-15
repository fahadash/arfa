ALTER TABLE [dbo].[Table]
	ADD CONSTRAINT [FK_User_Table]
	FOREIGN KEY (OwnerUserId)
	REFERENCES [User] (UserId)
