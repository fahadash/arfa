CREATE FUNCTION [dbo].[fn_EmptyGUID]
(
)
RETURNS UNIQUEIDENTIFIER
AS
BEGIN
	RETURN Cast(replicate('0', 7) + '0-' + replicate('0', 4) + '-' + replicate('0', 4) + '-' + replicate('0', 4) + '-' + replicate('0', 12) as UNIQUEIDENTIFIER)
END
