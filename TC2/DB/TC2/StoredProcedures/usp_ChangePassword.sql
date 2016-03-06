CREATE PROCEDURE [dbo].[usp_ChangePassword]
	@Username VARCHAR(100),
	@CurrentPassword VARCHAR(100),
	@NewPassword VARCHAR(100)
AS
	DECLARE @LoginToken UNIQUEIDENTIFIER, @ErrorCode VARCHAR(100), @Message VARCHAR(200)

	DECLARE @UserId INT

	SELECT @UserId = UserId FROM [User] WHERE Username = @Username AND [Password] = @CurrentPassword

	IF NOT @UserId IS NULL
	BEGIN
		SET @LoginToken = NEWID()
		SET @ErrorCode = 'SUCCESS'
		SET @Message = 'Password changed successfully'

		UPDATE [User] SET [Password] = @NewPassword, [Token] = @LoginToken, [TokenLastHit] = GETDATE() WHERE UserId = @UserId
	END
	ELSE
	BEGIN
		SET @LoginToken = dbo.fn_EmptyGUID()
		SET @ErrorCode = 'FAIL'
		SET @Message = 'Username or Current password does not match in the database.'
	
	END

	SELECT @LoginToken AS LoginToken, @ErrorCode AS ErrorCode, @Message AS Message
RETURN 0
