CREATE PROCEDURE [dbo].[usp_ListTablesUserIsOn]
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

	SELECT [Table].TableId, TableName, 4 - (
		SELECT COUNT(*) FROM TableUser WHERE TableUser.TableId = [Table].TableId) SlotsAvailable, [User].UserId, Username
	FROM [Table]
	INNER JOIN [TableUser] ON [Table].[TableId] = [TableUser].TableId
	
	INNER JOIN [User] ON [Table].OwnerUserId = [User].UserId

	WHERE TableUser.UserId = @UserId  AND Suspended = 0

RETURN 0
