CREATE PROCEDURE [dbo].[usp_ListJoinableTables]
	@LoginToken UNIQUEIDENTIFIER
AS
	DECLARE @UserId INT
	DECLARE @ErrorCode VARCHAR(100), @Message VARCHAR(200)

	SET @UserId = dbo.fn_GetUserIdByToken(@LoginToken)
	
	IF (@UserId IS NULL)
	BEGIN
		SET @ErrorCode = 'INVALIDLOGINTOKEN'
		SET @Message = 'Invalid login token, make sure yu are logged in'
	END
	ELSE
	BEGIN
		EXEC [usp_UpdateTokenHit] @UserId

		SET @ErrorCode = 'SUCCESS'
		SET @Message = ''
	END

	if @ErrorCode <> 'SUCCESS'
	BEGIN
		RAISERROR (@ErrorCode, 11, 0);
	END

	SELECT TableId, TableName, 4 - (
		SELECT COUNT(*) FROM TableUser WHERE TableUser.TableId = [Table].TableId) SlotsAvailable, UserId, Username
	FROM [Table]
	INNER JOIN [User] ON [Table].OwnerUserId = [User].UserId

	WHERE OwnerUserId <> @UserId AND (SELECT COUNT(*) FROM TableUser WHERE TableUser.TableId = [Table].TableId) < 3 AND Suspended = 0

RETURN 0
