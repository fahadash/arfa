CREATE PROCEDURE [dbo].[usp_RegisterUser]
	@UserName VARCHAR(100),
	@Password VARCHAR(100),
	@Firstname VARCHAR(100),
	@Lastname  VARCHAR(100),
	@Age		INT
AS
DECLARE @UserId INT, @ErrorCode VARCHAR(100), @Message VARCHAR(200)
	
	IF NOT EXISTS(SELECT * FROM [User] WHERE Username=@UserName) 
	BEGIN
		INSERT INTO dbo.[User](UserName, Password, Firstname, Lastname, Age)
		VALUES (@UserName, @Password, @Firstname, @Lastname, @Age)

		SET @UserId = @@IDENTITY
		SET @ErrorCode = 'SUCCESS'
		SET @Message = 'User created successfully'
	END
	ELSE
	BEGIN
		
		SET @UserId = 0
		SET @ErrorCode = 'USERNAMETAKEN'
		SET @Message = 'User name is already in use. Choose a different one.'
	END
	SELECT @UserId AS UserId, @ErrorCode AS ErrorCode, @Message AS [Message]
RETURN 0
