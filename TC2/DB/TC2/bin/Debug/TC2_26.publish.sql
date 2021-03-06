﻿/*
Deployment script for TC2

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TC2"
:setvar DefaultFilePrefix "TC2"
:setvar DefaultDataPath "c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key e7976e6b-f865-4b3e-b501-e3757c02ff83 is skipped, element [dbo].[TableUserCard].[Id] (SqlSimpleColumn) will not be renamed to TableUserId';


GO
PRINT N'Dropping DF__TableUser__Score__17036CC0...';


GO
ALTER TABLE [dbo].[TableUser] DROP CONSTRAINT [DF__TableUser__Score__17036CC0];


GO
PRINT N'Dropping DF__TableUser__IsDom__17F790F9...';


GO
ALTER TABLE [dbo].[TableUser] DROP CONSTRAINT [DF__TableUser__IsDom__17F790F9];


GO
PRINT N'Dropping FK_TableUser_ToUser...';


GO
ALTER TABLE [dbo].[TableUser] DROP CONSTRAINT [FK_TableUser_ToUser];


GO
PRINT N'Dropping FK_TableUser_ToTable...';


GO
ALTER TABLE [dbo].[TableUser] DROP CONSTRAINT [FK_TableUser_ToTable];


GO
PRINT N'Starting rebuilding table [dbo].[TableUser]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_TableUser] (
    [TableUserId] INT IDENTITY (1, 1) NOT NULL,
    [TableId]     INT NOT NULL,
    [UserId]      INT NOT NULL,
    [Score]       INT DEFAULT 0 NOT NULL,
    [IsDominant]  BIT DEFAULT 0 NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_TableUser] PRIMARY KEY CLUSTERED ([TableUserId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[TableUser])
    BEGIN
        
        INSERT INTO [dbo].[tmp_ms_xx_TableUser] ([TableId], [UserId], [Score], [IsDominant])
        SELECT [TableId],
               [UserId],
               [Score],
               [IsDominant]
        FROM   [dbo].[TableUser];
        
    END

DROP TABLE [dbo].[TableUser];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_TableUser]', N'TableUser';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_TableUser]', N'PK_TableUser', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[TableUserCard]...';


GO
CREATE TABLE [dbo].[TableUserCard] (
    [TableUserId] INT NOT NULL,
    [CardId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([TableUserId] ASC, [CardId] ASC)
);


GO
PRINT N'Creating FK_TableUser_ToUser...';


GO
ALTER TABLE [dbo].[TableUser] WITH NOCHECK
    ADD CONSTRAINT [FK_TableUser_ToUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]);


GO
PRINT N'Creating FK_TableUser_ToTable...';


GO
ALTER TABLE [dbo].[TableUser] WITH NOCHECK
    ADD CONSTRAINT [FK_TableUser_ToTable] FOREIGN KEY ([TableId]) REFERENCES [dbo].[Table] ([TableId]);


GO
PRINT N'Creating FK_TableUserCard_TableUser...';


GO
ALTER TABLE [dbo].[TableUserCard] WITH NOCHECK
    ADD CONSTRAINT [FK_TableUserCard_TableUser] FOREIGN KEY ([TableUserId]) REFERENCES [dbo].[TableUser] ([TableUserId]);


GO
PRINT N'Creating FK_TableUserCard_Card...';


GO
ALTER TABLE [dbo].[TableUserCard] WITH NOCHECK
    ADD CONSTRAINT [FK_TableUserCard_Card] FOREIGN KEY ([CardId]) REFERENCES [dbo].[Card] ([CardId]);


GO
PRINT N'Altering [dbo].[usp_JoinTable]...';


GO
ALTER PROCEDURE [dbo].[usp_JoinTable]
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
GO
PRINT N'Refreshing [dbo].[usp_GetTableTimestamp]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_GetTableTimestamp';


GO
PRINT N'Refreshing [dbo].[usp_ListJoinableTables]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_ListJoinableTables';


GO
PRINT N'Refreshing [dbo].[usp_ListUserTables]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_ListUserTables';


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'e7976e6b-f865-4b3e-b501-e3757c02ff83')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('e7976e6b-f865-4b3e-b501-e3757c02ff83')

GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DELETE FROM Card
GO

SET IDENTITY_INSERT [dbo].[Card] ON 

GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (2, N'Ace of Spades', N'AS')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (3, N'Ace of Clubs', N'AC')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (4, N'Ace of Hearts', N'AH')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (5, N'Ace of Diamonds', N'AD')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (6, N'King of Spades', N'KS')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (7, N'King of Clubs', N'KC')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (8, N'King of Hearts', N'KH')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (9, N'King of Diamonds', N'KD')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (10, N'Queen of Spades', N'QS')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (11, N'Queen of Clubs', N'QC')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (12, N'Queen of Hearts', N'QH')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (13, N'Queen of Diamonds', N'QD')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (14, N'Jack of Spades', N'JS')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (15, N'Jack of Clubs', N'JC')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (16, N'Jack of Hearts', N'JH')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (17, N'Jack of Diamonds', N'JD')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (18, N'10 of Spades', N'10S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (19, N'10 of Clubs', N'10C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (20, N'10 of Hearts', N'10H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (21, N'10 of Diamonds', N'10D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (22, N'9 of Spades', N'9S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (23, N'9 of Clubs', N'9C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (24, N'9 of Hearts', N'9H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (25, N'9 of Diamonds', N'9D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (26, N'8 of Spades', N'8S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (27, N'8 of Clubs', N'8C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (28, N'8 of Hearts', N'8H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (29, N'8 of Diamonds', N'8D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (30, N'7 of Spades', N'7S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (31, N'7 of Clubs', N'7C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (32, N'7 of Hearts', N'7H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (33, N'7 of Diamonds', N'7D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (34, N'6 of Spades', N'6S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (35, N'6 of Clubs', N'6C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (36, N'6 of Hearts', N'6H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (37, N'6 of Diamonds', N'6D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (38, N'5 of Spades', N'5S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (39, N'5 of Clubs', N'5C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (40, N'5 of Hearts', N'5H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (41, N'5 of Diamonds', N'5D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (42, N'4 of Spades', N'4S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (43, N'4 of Clubs', N'4C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (44, N'4 of Hearts', N'4H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (45, N'4 of Diamonds', N'4D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (46, N'3 of Spades', N'3S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (47, N'3 of Clubs', N'3C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (48, N'3 of Hearts', N'3H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (49, N'3 of Diamonds', N'3D')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (50, N'2 of Spades', N'2S')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (51, N'2 of Clubs', N'2C')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (52, N'2 of Hearts', N'2H')
GO
INSERT [dbo].[Card] ([CardId], [CardName], [CardAlias]) VALUES (53, N'2 of Diamonds', N'2D')
GO
SET IDENTITY_INSERT [dbo].[Card] OFF
GO

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[TableUser] WITH CHECK CHECK CONSTRAINT [FK_TableUser_ToUser];

ALTER TABLE [dbo].[TableUser] WITH CHECK CHECK CONSTRAINT [FK_TableUser_ToTable];

ALTER TABLE [dbo].[TableUserCard] WITH CHECK CHECK CONSTRAINT [FK_TableUserCard_TableUser];

ALTER TABLE [dbo].[TableUserCard] WITH CHECK CHECK CONSTRAINT [FK_TableUserCard_Card];


GO
PRINT N'Update complete.';


GO
