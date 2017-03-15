CREATE FUNCTION [dbo].[fn_GetUserIdByToken]
(
	@LoginToken UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	DECLARE @UserId INT

	SELECT @UserId = UserId FROM [User] WHERE Token = @LoginToken AND DATEDIFF(Minute, TokenLastHit, GETDATE()) < 16
	


	RETURN @UserId
END
