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
PRINT N'Altering [dbo].[Table]...';


GO
ALTER TABLE [dbo].[Table]
    ADD [GameStarted] BIT DEFAULT 0 NOT NULL;


GO
PRINT N'Refreshing [dbo].[usp_CreateTable]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_CreateTable';


GO
PRINT N'Refreshing [dbo].[usp_GetTableTimestamp]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_GetTableTimestamp';


GO
PRINT N'Refreshing [dbo].[usp_JoinTable]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_JoinTable';


GO
PRINT N'Refreshing [dbo].[usp_ListJoinableTables]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_ListJoinableTables';


GO
PRINT N'Refreshing [dbo].[usp_ListUserTables]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_ListUserTables';


GO
PRINT N'Refreshing [dbo].[usp_SuspendTable]...';


GO
EXECUTE sp_refreshsqlmodule N'dbo.usp_SuspendTable';


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

