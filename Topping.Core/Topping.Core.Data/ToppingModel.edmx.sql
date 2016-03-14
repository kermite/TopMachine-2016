
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/08/2016 11:04:44
-- Generated from EDMX file: C:\Users\dvanb\Documents\TopMachine 2016\Topping.Core\Topping.Core.Data\ToppingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TopMachineDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'GameConfigs'
CREATE TABLE [dbo].[GameConfigs] (
    [Id] uniqueidentifier  NOT NULL,
    [XmlConfig] nvarchar(max)  NULL
);
GO

-- Creating table 'GameRankings'
CREATE TABLE [dbo].[GameRankings] (
    [Year] int  NOT NULL,
    [Month] int  NOT NULL,
    [ConfigGameId] uniqueidentifier  NOT NULL,
    [Playerid] uniqueidentifier  NOT NULL,
    [Percentage] float  NOT NULL,
    [NbParties] int  NOT NULL,
    [LostTops] float  NOT NULL,
    [Time] int  NOT NULL,
    [TotalTops] int  NOT NULL,
    [Position] int  NOT NULL,
    [Serie] int  NOT NULL,
    [PlayerTop] int  NOT NULL,
    [GameTop] int  NOT NULL,
    [Selection] int  NOT NULL,
    [BestTime] int  NOT NULL,
    [BestScore] float  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [GameId] uniqueidentifier  NOT NULL,
    [GameConfigId] uniqueidentifier  NOT NULL,
    [Date] datetime  NOT NULL,
    [Status] bigint  NOT NULL,
    [GameXml] nvarchar(max)  NOT NULL,
    [Name] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'ViewUserIdAndNames'
CREATE TABLE [dbo].[ViewUserIdAndNames] (
    [PKID] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'GameConfig1Set'
CREATE TABLE [dbo].[GameConfig1Set] (
    [Id] uniqueidentifier  NOT NULL,
    [XmlConfig] nvarchar(max)  NULL
);
GO

-- Creating table 'GameRanking1Set'
CREATE TABLE [dbo].[GameRanking1Set] (
    [Year] int  NOT NULL,
    [Month] int  NOT NULL,
    [ConfigGameId] uniqueidentifier  NOT NULL,
    [Playerid] uniqueidentifier  NOT NULL,
    [Percentage] float  NOT NULL,
    [NbParties] int  NOT NULL,
    [LostTops] float  NOT NULL,
    [Time] int  NOT NULL,
    [TotalTops] int  NOT NULL,
    [Position] int  NOT NULL,
    [Serie] int  NOT NULL,
    [PlayerTop] int  NOT NULL,
    [GameTop] int  NOT NULL,
    [Selection] int  NOT NULL,
    [BestTime] int  NOT NULL,
    [BestScore] float  NOT NULL
);
GO

-- Creating table 'Game1Set'
CREATE TABLE [dbo].[Game1Set] (
    [GameId] uniqueidentifier  NOT NULL,
    [GameConfigId] uniqueidentifier  NOT NULL,
    [Date] datetime  NOT NULL,
    [Status] bigint  NOT NULL,
    [GameXml] nvarchar(max)  NOT NULL,
    [Name] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'ViewUserIdAndName1Set'
CREATE TABLE [dbo].[ViewUserIdAndName1Set] (
    [PKID] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'GamesDetails'
CREATE TABLE [dbo].[GamesDetails] (
    [GameId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [DetailXml] nvarchar(max)  NULL,
    [Total] bigint  NULL,
    [Solos] bigint  NULL,
    [Warnings] bigint  NULL,
    [Time] bigint  NULL,
    [Percentage] float  NULL,
    [TopLost] bigint  NULL,
    [Top] bigint  NULL,
    [Negative] bigint  NULL,
    [Selection] bigint  NULL,
    [Rating] bigint  NULL,
    [Status] bigint  NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'ViewGetRankings'
CREATE TABLE [dbo].[ViewGetRankings] (
    [UserName] nvarchar(255)  NOT NULL,
    [Year] int  NOT NULL,
    [Month] int  NOT NULL,
    [ConfigGameId] uniqueidentifier  NOT NULL,
    [Playerid] uniqueidentifier  NOT NULL,
    [Percentage] float  NOT NULL,
    [NbParties] int  NOT NULL,
    [LostTops] float  NOT NULL,
    [Time] int  NOT NULL,
    [TotalTops] int  NOT NULL,
    [Position] int  NOT NULL,
    [Serie] int  NOT NULL,
    [PlayerTop] int  NOT NULL,
    [GameTop] int  NOT NULL,
    [Selection] int  NOT NULL,
    [BestTime] int  NOT NULL,
    [BestScore] float  NOT NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [PKID] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(255)  NOT NULL,
    [ApplicationName] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [Comment] nvarchar(255)  NULL,
    [Password] nvarchar(128)  NULL,
    [PasswordQuestion] nvarchar(255)  NULL,
    [PasswordAnswer] nvarchar(255)  NULL,
    [IsApproved] tinyint  NULL,
    [LastActivityDate] datetime  NULL,
    [LastLoginDate] datetime  NULL,
    [LastPasswordChangedDate] datetime  NULL,
    [CreationDate] datetime  NULL,
    [IsOnLine] tinyint  NULL,
    [IsLockedOut] tinyint  NULL,
    [LastLockedOutDate] datetime  NULL,
    [FailedPasswordAttemptCount] int  NULL,
    [FailedPasswordAttemptWindowStart] datetime  NULL,
    [FailedPasswordAnswerAttemptCount] int  NULL,
    [FailedPasswordAnswerAttemptWindowStart] datetime  NULL
);
GO

-- Creating table 'ViewGetGameDetails'
CREATE TABLE [dbo].[ViewGetGameDetails] (
    [UserName] nvarchar(255)  NOT NULL,
    [GameId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [Total] bigint  NULL,
    [Solos] bigint  NULL,
    [Warnings] bigint  NULL,
    [Time] bigint  NULL,
    [Percentage] float  NULL,
    [TopLost] bigint  NULL,
    [Top] bigint  NULL,
    [Negative] bigint  NULL,
    [Selection] bigint  NULL,
    [Rating] bigint  NULL,
    [Status] bigint  NULL,
    [Date] datetime  NOT NULL,
    [GameConfigId] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'GameConfigs'
ALTER TABLE [dbo].[GameConfigs]
ADD CONSTRAINT [PK_GameConfigs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Year], [Month], [ConfigGameId], [Playerid] in table 'GameRankings'
ALTER TABLE [dbo].[GameRankings]
ADD CONSTRAINT [PK_GameRankings]
    PRIMARY KEY CLUSTERED ([Year], [Month], [ConfigGameId], [Playerid] ASC);
GO

-- Creating primary key on [GameId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([GameId] ASC);
GO

-- Creating primary key on [PKID], [UserName] in table 'ViewUserIdAndNames'
ALTER TABLE [dbo].[ViewUserIdAndNames]
ADD CONSTRAINT [PK_ViewUserIdAndNames]
    PRIMARY KEY CLUSTERED ([PKID], [UserName] ASC);
GO

-- Creating primary key on [Id] in table 'GameConfig1Set'
ALTER TABLE [dbo].[GameConfig1Set]
ADD CONSTRAINT [PK_GameConfig1Set]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Year], [Month], [ConfigGameId], [Playerid] in table 'GameRanking1Set'
ALTER TABLE [dbo].[GameRanking1Set]
ADD CONSTRAINT [PK_GameRanking1Set]
    PRIMARY KEY CLUSTERED ([Year], [Month], [ConfigGameId], [Playerid] ASC);
GO

-- Creating primary key on [GameId] in table 'Game1Set'
ALTER TABLE [dbo].[Game1Set]
ADD CONSTRAINT [PK_Game1Set]
    PRIMARY KEY CLUSTERED ([GameId] ASC);
GO

-- Creating primary key on [PKID], [UserName] in table 'ViewUserIdAndName1Set'
ALTER TABLE [dbo].[ViewUserIdAndName1Set]
ADD CONSTRAINT [PK_ViewUserIdAndName1Set]
    PRIMARY KEY CLUSTERED ([PKID], [UserName] ASC);
GO

-- Creating primary key on [GameId], [UserId] in table 'GamesDetails'
ALTER TABLE [dbo].[GamesDetails]
ADD CONSTRAINT [PK_GamesDetails]
    PRIMARY KEY CLUSTERED ([GameId], [UserId] ASC);
GO

-- Creating primary key on [UserName], [Year], [Month], [ConfigGameId], [Playerid], [Percentage], [NbParties], [LostTops], [Time], [TotalTops], [Position], [Serie], [PlayerTop], [GameTop], [Selection], [BestTime], [BestScore] in table 'ViewGetRankings'
ALTER TABLE [dbo].[ViewGetRankings]
ADD CONSTRAINT [PK_ViewGetRankings]
    PRIMARY KEY CLUSTERED ([UserName], [Year], [Month], [ConfigGameId], [Playerid], [Percentage], [NbParties], [LostTops], [Time], [TotalTops], [Position], [Serie], [PlayerTop], [GameTop], [Selection], [BestTime], [BestScore] ASC);
GO

-- Creating primary key on [PKID] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([PKID] ASC);
GO

-- Creating primary key on [UserName], [GameId], [UserId], [Date], [GameConfigId] in table 'ViewGetGameDetails'
ALTER TABLE [dbo].[ViewGetGameDetails]
ADD CONSTRAINT [PK_ViewGetGameDetails]
    PRIMARY KEY CLUSTERED ([UserName], [GameId], [UserId], [Date], [GameConfigId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------