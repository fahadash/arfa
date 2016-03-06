CREATE PROCEDURE [dbo].[usp_SuspendTable]
	@LoginToken UNIQUEIDENTIFIER,
	@TableId int
AS
	DECLARE @UserId INT
	DECLARE @ErrorCode VARCHAR(100), @Message VARCHAR(200)

	SET @UserId = dbo.fn_GetUserIdByToken(@LoginToken)
	
	IF (NOT EXISTS(SELECT * FROM [Table] WHERE TableId = @TableId AND Suspended = 0))
	BEGIN
		SET @ErrorCode = 'INVALIDTABLEID'
		SET @Message = 'Invalid table.'
	END
	ELSE
	IF (@UserId IS NULL)
	BEGIN
		SET @ErrorCode = 'INVALIDLOGINTOKEN'
		SET @Message = 'Invalid login token, make sure yu are logged in'
	END
	ELSE
	IF (NOT EXISTS(SELECT * FROM [Table] WHERE TableId = @TableId AND OwnerUserId = @UserId  AND Suspended = 0))
	BEGIN
		SET @ErrorCode = 'USERNOTOWNER'
		SET @Message = 'User does not own this table.'
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
	ELSE
	BEGIN
		UPDATE [Table] SET Suspended = 1 WHERE TableId = @TableId	
	END

RETURN 0