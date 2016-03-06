CREATE PROCEDURE [dbo].[usp_LogIn]
	@Username VARCHAR(100),
	@Password VARCHAR(100)
AS
	DECLARE @LoginToken UNIQUEIDENTIFIER, @ErrorCode VARCHAR(100), @Message VARCHAR(200)
	DECLARE @Firstname VARCHAR(100)
	DECLARE @Lastname VARCHAR(100)

	DECLARE @UserId INT

	SELECT @UserId = UserId, @Firstname = Firstname, @Lastname = Lastname
	 FROM [User] WHERE Username = @Username AND [Password] = @Password

	IF NOT @UserId IS NULL
	BEGIN
		SET @LoginToken = NEWID()
		SET @ErrorCode = 'SUCCESS'
		SET @Message = 'User logged in successfully'

		UPDATE [User] SET [Token] = @LoginToken, [TokenLastHit] = GETDATE() WHERE UserId = @UserId
	END
	ELSE
	BEGIN
		SET @LoginToken = dbo.fn_EmptyGUID()
		SET @ErrorCode = 'FAIL'
		SET @Message = 'Login failed.'
	
	END

	SELECT @LoginToken AS LoginToken, @Firstname as Firstname, @Lastname AS Lastname, @ErrorCode AS ErrorCode, @Message AS Message
RETURN 0
