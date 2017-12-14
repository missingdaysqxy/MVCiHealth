
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/07/2017 18:21:11
-- Generated from EDMX file: E:\Document\程序设计方法学\MVCiHealth\MVCiHealth\Models\iHealthModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LocalDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DISEASE_TYPE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DISEASE_TYPE];
GO
IF OBJECT_ID(N'[dbo].[DOCTOR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DOCTOR];
GO
IF OBJECT_ID(N'[dbo].[DOCTOR_EVALUATION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DOCTOR_EVALUATION];
GO
IF OBJECT_ID(N'[dbo].[GROUPINFO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GROUPINFO];
GO
IF OBJECT_ID(N'[dbo].[PATIENT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PATIENT];
GO
IF OBJECT_ID(N'[dbo].[PATIENT_HISTORY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PATIENT_HISTORY];
GO
IF OBJECT_ID(N'[dbo].[RESERVATION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RESERVATION];
GO
IF OBJECT_ID(N'[dbo].[SECTION_TYPE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SECTION_TYPE];
GO
IF OBJECT_ID(N'[dbo].[SYSLOG]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SYSLOG];
GO
IF OBJECT_ID(N'[dbo].[USERINFO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[USERINFO];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DISEASE_TYPE'
CREATE TABLE [dbo].[DISEASE_TYPE] (
    [DISEASE_ID] int  NOT NULL,
    [DISEASE_NM] nvarchar(20)  NOT NULL,
    [DISEASE_DESCRIPTION] nvarchar(100)  NULL,
    [SECTION_ID] int  NULL
);
GO

-- Creating table 'DOCTOR'
CREATE TABLE [dbo].[DOCTOR] (
    [DOCTOR_ID] int  NOT NULL,
    [DOCTOR_NM] nvarchar(20)  NULL,
    [GENDER] nvarchar(1)  NULL,
    [AGE] int  NULL,
    [TEL] nchar(20)  NULL,
    [PHOTO_URL] nvarchar(100)  NULL,
    [SECTION_ID] int  NULL,
    [DISEASE_ID] int  NULL,
    [INTRODUCTION] nvarchar(100)  NULL,
    [INSDATE] datetime  NULL
);
GO

-- Creating table 'DOCTOR_EVALUATION'
CREATE TABLE [dbo].[DOCTOR_EVALUATION] (
    [EVALUATION_ID] int  NOT NULL,
    [PATIENT_ID] int  NOT NULL,
    [DOCTOR_ID] int  NOT NULL,
    [LEVEL] int  NULL,
    [DETAIL] nvarchar(100)  NULL,
    [INSDATE] datetime  NOT NULL,
    [AGREETIMES] int  NOT NULL
);
GO

-- Creating table 'GROUPINFO'
CREATE TABLE [dbo].[GROUPINFO] (
    [GROUP_ID] int  NOT NULL,
    [GROUP_NM] nvarchar(20)  NOT NULL,
    [AUTHENTICATION] nchar(20)  NOT NULL,
    [INSDATE] nchar(10)  NOT NULL
);
GO

-- Creating table 'PATIENT'
CREATE TABLE [dbo].[PATIENT] (
    [PATIENT_ID] int  NOT NULL,
    [PATIENT_NM] nvarchar(20)  NULL,
	[PHOTO_URL] nvarchar(100)  NULL,
    [BIRTH] datetime  NULL,
    [GENDER] nvarchar(10)  NULL,
    [TEL] nchar(20)  NULL,
    [TEL2] nchar(20)  NULL,
    [EMAIL] nvarchar(50)  NULL,
    [ADDRESS] nvarchar(200)  NULL,
    [BLOOD_TYPE] nvarchar(5)  NULL,
    [ALLERGIC_HISTORY] nvarchar(50)  NULL,
    [GENETIC_HISTORY] nvarchar(150)  NULL,
    [CAPITAL_OPERATION] nvarchar(150)  NULL,
    [EMERGENCY_NAME] nvarchar(20)  NULL,
    [EMERGENCY_TEL] nchar(20)  NULL,
    [COMMENT] nvarchar(200)  NULL,
    [INSDATE] datetime  NOT NULL
);
GO

-- Creating table 'PATIENT_HISTORY'
CREATE TABLE [dbo].[PATIENT_HISTORY] (
    [HISTORY_ID] int  NOT NULL,
    [PATIENT_ID] int  NOT NULL,
    [HISTORY_URL] nvarchar(100)  NOT NULL,
    [PATIENT_IN] nchar(1)  NULL,
    [INSDATE] datetime  NULL,
    [UPDATE] datetime  NULL
);
GO

-- Creating table 'RESERVATION'
CREATE TABLE [dbo].[RESERVATION] (
    [RESERVATION_ID] int  NOT NULL,
    [DOCTOR_ID] int  NULL,
    [PATIENT_ID] int  NULL,
    [TIME_START] datetime  NULL,
    [TIME_FINISH] datetime  NULL,
    [CONFIRMED] nchar(1)  NULL,
    [VALID] nchar(1)  NULL,
    [COMMENT] nvarchar(200)  NULL,
    [INSDATE] datetime  NULL
);
GO

-- Creating table 'SECTION_TYPE'
CREATE TABLE [dbo].[SECTION_TYPE] (
    [SECTION_ID] int  NOT NULL,
    [SECTION_OFFICER] nvarchar(20)  NULL,
    [SECTION_OID] nvarchar(max)  NULL,
    [SECTION_NM] nvarchar(20)  NULL
);
GO

-- Creating table 'SYSLOG'
CREATE TABLE [dbo].[SYSLOG] (
    [LOG_ID] int  NOT NULL,
    [SENDER] nvarchar(50)  NULL,
    [TYPE] int  NULL,
    [LEVEL] int  NULL,
    [MESSAGE] nvarchar(max)  NULL,
    [AUTHORITY] int  NULL,
    [INSDATE] datetime  NULL,
    [OUTDATE] datetime  NULL
);
GO

-- Creating table 'USERINFO'
CREATE TABLE [dbo].[USERINFO] (
    [USER_ID] int  NOT NULL,
    [USER_NM] nchar(10)  NOT NULL,
    [USER_PW] nchar(10)  NOT NULL,
    [GROUP_ID] int  NOT NULL,
    [EMAIL] nvarchar(50)  NULL,
    [TEL] nchar(20)  NULL,
    [COUNTORY] nchar(10)  NULL,
    [PROVINCE] nchar(10)  NULL,
    [CITY] nchar(10)  NULL,
    [ADDRESS] nchar(10)  NULL,
    [VALID] nchar(10)  NULL,
    [DOC_URL] nchar(10)  NULL,
    [INSDATE] datetime  NOT NULL,
    [UPDATE] datetime  NULL,
    [LASTLOGINTIME] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DISEASE_ID] in table 'DISEASE_TYPE'
ALTER TABLE [dbo].[DISEASE_TYPE]
ADD CONSTRAINT [PK_DISEASE_TYPE]
    PRIMARY KEY CLUSTERED ([DISEASE_ID] ASC);
GO

-- Creating primary key on [DOCTOR_ID] in table 'DOCTOR'
ALTER TABLE [dbo].[DOCTOR]
ADD CONSTRAINT [PK_DOCTOR]
    PRIMARY KEY CLUSTERED ([DOCTOR_ID] ASC);
GO

-- Creating primary key on [EVALUATION_ID] in table 'DOCTOR_EVALUATION'
ALTER TABLE [dbo].[DOCTOR_EVALUATION]
ADD CONSTRAINT [PK_DOCTOR_EVALUATION]
    PRIMARY KEY CLUSTERED ([EVALUATION_ID] ASC);
GO

-- Creating primary key on [GROUP_ID] in table 'GROUPINFO'
ALTER TABLE [dbo].[GROUPINFO]
ADD CONSTRAINT [PK_GROUPINFO]
    PRIMARY KEY CLUSTERED ([GROUP_ID] ASC);
GO

-- Creating primary key on [PATIENT_ID] in table 'PATIENT'
ALTER TABLE [dbo].[PATIENT]
ADD CONSTRAINT [PK_PATIENT]
    PRIMARY KEY CLUSTERED ([PATIENT_ID] ASC);
GO

-- Creating primary key on [HISTORY_ID] in table 'PATIENT_HISTORY'
ALTER TABLE [dbo].[PATIENT_HISTORY]
ADD CONSTRAINT [PK_PATIENT_HISTORY]
    PRIMARY KEY CLUSTERED ([HISTORY_ID] ASC);
GO

-- Creating primary key on [RESERVATION_ID] in table 'RESERVATION'
ALTER TABLE [dbo].[RESERVATION]
ADD CONSTRAINT [PK_RESERVATION]
    PRIMARY KEY CLUSTERED ([RESERVATION_ID] ASC);
GO

-- Creating primary key on [SECTION_ID] in table 'SECTION_TYPE'
ALTER TABLE [dbo].[SECTION_TYPE]
ADD CONSTRAINT [PK_SECTION_TYPE]
    PRIMARY KEY CLUSTERED ([SECTION_ID] ASC);
GO

-- Creating primary key on [LOG_ID] in table 'SYSLOG'
ALTER TABLE [dbo].[SYSLOG]
ADD CONSTRAINT [PK_SYSLOG]
    PRIMARY KEY CLUSTERED ([LOG_ID] ASC);
GO

-- Creating primary key on [USER_ID] in table 'USERINFO'
ALTER TABLE [dbo].[USERINFO]
ADD CONSTRAINT [PK_USERINFO]
    PRIMARY KEY CLUSTERED ([USER_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------