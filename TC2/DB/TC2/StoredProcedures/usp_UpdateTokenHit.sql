CREATE PROCEDURE [dbo].[usp_UpdateTokenHit]
	@UserId INT
AS
	IF NOT @UserId IS NULL
	BEGIN
		UPDATE [User] SET TokenLastHit = GETDATE() WHERE UserId = @UserId
	END
RETURN 0
