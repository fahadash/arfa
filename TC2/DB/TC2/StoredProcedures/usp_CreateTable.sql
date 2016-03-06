CREATE PROCEDURE [dbo].[usp_CreateTable]
	@LoginToken UNIQUEIDENTIFIER,
	@TableName VARCHAR(100)
AS
	DECLARE @UserId INT
	DECLARE @ErrorCode VARCHAR(100), @Message VARCHAR(200)
	DECLARE @TableId INT

	SET @UserId = dbo.fn_GetUserIdByToken(@LoginToken)
	
	IF (EXISTS(SELECT * FROM [Table] WHERE TableName = @TableName))
	BEGIN
		SET @ErrorCode = 'TABLENAMETAKEN'
		SET @Message = 'Table name is already in user, please choose a different one.'
	END
	ELSE
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
	ELSE
	BEGIN
	INSERT INTO dbo.[Table] (TableName, OwnerUserId, Suspended, [Timestamp])
	VALUES (@TableName, @UserId, 0, GETDATE())

	SET @TableId = @@IDENTITY

		INSERT INTO TableUser
		([TableId], [UserId])
		VALUES
		(@TableId, @UserId)	

		UPDATE [Table] SET Timestamp=GETDATE() WHERE TableId = @TableId
	END
	SELECT @TableId AS TableId, @TableName AS TableName, 3 AS AvailableSlots, UserId, Username FROM [User] WHERE UserId = @UserId

RETURN 0
