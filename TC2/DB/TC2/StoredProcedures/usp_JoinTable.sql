CREATE PROCEDURE [dbo].[usp_JoinTable]
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
	IF (EXISTS(SELECT * FROM [Table] WHERE TableId = @TableId AND OwnerUserId = @UserId  AND Suspended = 0))
	BEGIN
		SET @ErrorCode = 'USEROWNER'
		SET @Message = 'User owns this table.'
	END
	ELSE
	IF (EXISTS(SELECT * FROM [TableUser] WHERE TableId = @TableId AND [UserId] = @UserId ))
	BEGIN
		SET @ErrorCode = 'USERALREADYON'
		SET @Message = 'User is already on that table.'
	END
	ELSE
	IF ((SELECT COUNT(*) FROM [TableUser] WHERE TableId = @TableId AND [UserId] = @UserId ) = 4)
	BEGIN
		SET @ErrorCode = 'TABLEFULL'
		SET @Message = 'Table is full.'
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
		INSERT INTO TableUser
		([TableId], [UserId])
		VALUES
		(@TableId, @UserId)	

		UPDATE [Table] SET Timestamp=GETDATE() WHERE TableId = @TableId
	END

RETURN 0