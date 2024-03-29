/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2022 (16.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2022
    Target Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [StudentTrackingDB]    Script Date: 7.01.2024 21:05:28 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'StudentTrackingDB')
BEGIN
CREATE DATABASE [StudentTrackingDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StudentTrackingDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\StudentTrackingDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StudentTrackingDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\StudentTrackingDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 COLLATE Turkish_CI_AS
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
END
GO
ALTER DATABASE [StudentTrackingDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StudentTrackingDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StudentTrackingDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StudentTrackingDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StudentTrackingDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StudentTrackingDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StudentTrackingDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET RECOVERY FULL 
GO
ALTER DATABASE [StudentTrackingDB] SET  MULTI_USER 
GO
ALTER DATABASE [StudentTrackingDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StudentTrackingDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StudentTrackingDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StudentTrackingDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StudentTrackingDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StudentTrackingDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'StudentTrackingDB', N'ON'
GO
ALTER DATABASE [StudentTrackingDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [StudentTrackingDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/****** Object:  Login [YASUS\ENES]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'YASUS\ENES')
CREATE LOGIN [YASUS\ENES] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT Service\MSSQLSERVER]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT Service\MSSQLSERVER')
CREATE LOGIN [NT Service\MSSQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\Winmgmt]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT SERVICE\Winmgmt')
CREATE LOGIN [NT SERVICE\Winmgmt] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLWriter]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT SERVICE\SQLWriter')
CREATE LOGIN [NT SERVICE\SQLWriter] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLTELEMETRY]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT SERVICE\SQLTELEMETRY')
CREATE LOGIN [NT SERVICE\SQLTELEMETRY] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLSERVERAGENT]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT SERVICE\SQLSERVERAGENT')
CREATE LOGIN [NT SERVICE\SQLSERVERAGENT] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT AUTHORITY\SYSTEM]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT AUTHORITY\SYSTEM')
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyTsqlExecutionLogin##]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'##MS_PolicyTsqlExecutionLogin##')
CREATE LOGIN [##MS_PolicyTsqlExecutionLogin##] WITH PASSWORD=N'kY1uyB6F1zGsy5ckn1LvielmYPEiWgVgi9ja0rBSm+o=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyTsqlExecutionLogin##] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyEventProcessingLogin##]    Script Date: 7.01.2024 21:05:29 ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'##MS_PolicyEventProcessingLogin##')
CREATE LOGIN [##MS_PolicyEventProcessingLogin##] WITH PASSWORD=N'3kt+xbW/QHQt8EJ+mYx4N+4i5fMMjQTE9aJQY2zpvOQ=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyEventProcessingLogin##] DISABLE
GO
ALTER AUTHORIZATION ON DATABASE::[StudentTrackingDB] TO [YASUS\ENES]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [YASUS\ENES]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQLSERVER]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLSERVERAGENT]
GO
USE [StudentTrackingDB]
GO
GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO [public] AS [dbo]
GO
GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO [public] AS [dbo]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Class]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Class](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Turkish_CI_AS NULL,
	[Capacity] [int] NULL,
	[StartedDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Class] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Exam]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Exam]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Exam](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](max) COLLATE Turkish_CI_AS NULL,
	[StatusID] [int] NULL,
	[Date] [datetime] NULL,
	[isActive] [bit] NULL,
	[Name] [nvarchar](50) COLLATE Turkish_CI_AS NULL,
	[ClassID] [int] NULL,
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Exam] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Project]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Project]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Turkish_CI_AS NULL,
	[StartedDate] [datetime] NULL,
	[isFinal] [bit] NULL,
	[EndDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[ClassID] [int] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Project] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Question]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Question]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Question](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionName] [nvarchar](max) COLLATE Turkish_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE Turkish_CI_AS NULL,
	[StudentID] [int] NULL,
	[Date] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Question] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Report]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Report](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NULL,
	[Description] [nvarchar](max) COLLATE Turkish_CI_AS NULL,
	[Score] [int] NULL,
	[Date] [datetime] NULL,
	[WeekOfYear] [int] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Report] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Status]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) COLLATE Turkish_CI_AS NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Status] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Student]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Student](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) COLLATE Turkish_CI_AS NULL,
	[LastName] [nvarchar](50) COLLATE Turkish_CI_AS NULL,
	[Email] [nvarchar](100) COLLATE Turkish_CI_AS NULL,
	[StatusID] [int] NULL,
	[ClassID] [int] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Student] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[StudentExam]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentExam]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentExam](
	[StudentID] [int] NOT NULL,
	[ExamID] [int] NOT NULL,
	[Score] [int] NULL,
	[Description] [nvarchar](max) COLLATE Turkish_CI_AS NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_StudentExam] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC,
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[StudentExam] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[StudentProject]    Script Date: 7.01.2024 21:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentProject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentProject](
	[StudentID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[Score] [int] NULL,
	[Description] [nvarchar](max) COLLATE Turkish_CI_AS NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_StudentProject] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC,
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[StudentProject] TO  SCHEMA OWNER 
GO
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([ID], [Name], [Capacity], [StartedDate], [EndDate], [isActive]) VALUES (1, N'Fineksus-01', 20, CAST(N'2022-08-01T00:00:00.000' AS DateTime), CAST(N'2023-11-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Class] ([ID], [Name], [Capacity], [StartedDate], [EndDate], [isActive]) VALUES (2, N'Fineksus-02', 14, CAST(N'2023-09-23T00:00:00.000' AS DateTime), CAST(N'2023-12-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Class] ([ID], [Name], [Capacity], [StartedDate], [EndDate], [isActive]) VALUES (3, N'fayneksus', 15, CAST(N'1999-11-11T00:00:00.000' AS DateTime), CAST(N'1999-12-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Class] ([ID], [Name], [Capacity], [StartedDate], [EndDate], [isActive]) VALUES (4, N'fayneksus2', 55, CAST(N'1999-11-11T00:00:00.000' AS DateTime), CAST(N'1999-12-11T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Exam] ON 

INSERT [dbo].[Exam] ([ID], [Body], [StatusID], [Date], [isActive], [Name], [ClassID]) VALUES (1, N'sinav1Body', 201, CAST(N'2023-11-25T00:00:00.000' AS DateTime), 1, N'sinav1', 1)
INSERT [dbo].[Exam] ([ID], [Body], [StatusID], [Date], [isActive], [Name], [ClassID]) VALUES (2, N'sinav2Body', 201, CAST(N'2023-10-03T00:00:00.000' AS DateTime), 1, N'sinav2', 2)
INSERT [dbo].[Exam] ([ID], [Body], [StatusID], [Date], [isActive], [Name], [ClassID]) VALUES (3, N'asd', 201, CAST(N'2023-10-03T00:00:00.000' AS DateTime), 1, NULL, 2)
INSERT [dbo].[Exam] ([ID], [Body], [StatusID], [Date], [isActive], [Name], [ClassID]) VALUES (4, N'External1body', 202, CAST(N'2023-10-03T00:00:00.000' AS DateTime), 0, N'ExternalSinav1', 2)
INSERT [dbo].[Exam] ([ID], [Body], [StatusID], [Date], [isActive], [Name], [ClassID]) VALUES (5, N'External2body', 202, CAST(N'2023-11-28T00:00:00.000' AS DateTime), 1, N'ExternalSınav2', 1)
INSERT [dbo].[Exam] ([ID], [Body], [StatusID], [Date], [isActive], [Name], [ClassID]) VALUES (6, N'gg', 201, CAST(N'2023-11-28T00:00:00.000' AS DateTime), 0, N'ggffdg', 3)
SET IDENTITY_INSERT [dbo].[Exam] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([ID], [Name], [StartedDate], [isFinal], [EndDate], [isActive], [ClassID]) VALUES (1, N'CarTender', CAST(N'2022-10-10T00:00:00.000' AS DateTime), 1, CAST(N'2022-10-17T00:00:00.000' AS DateTime), 0, 2)
INSERT [dbo].[Project] ([ID], [Name], [StartedDate], [isFinal], [EndDate], [isActive], [ClassID]) VALUES (2, N'YesilEvApp', CAST(N'2022-10-09T00:00:00.000' AS DateTime), 0, CAST(N'2022-09-17T00:00:00.000' AS DateTime), NULL, 2)
INSERT [dbo].[Project] ([ID], [Name], [StartedDate], [isFinal], [EndDate], [isActive], [ClassID]) VALUES (13, N'asd', CAST(N'2011-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2011-01-01T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Project] ([ID], [Name], [StartedDate], [isFinal], [EndDate], [isActive], [ClassID]) VALUES (14, N'asdasd', CAST(N'2019-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2019-01-01T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Project] ([ID], [Name], [StartedDate], [isFinal], [EndDate], [isActive], [ClassID]) VALUES (15, N'sdasdas', CAST(N'1999-01-01T00:00:00.000' AS DateTime), 1, CAST(N'1999-02-12T00:00:00.000' AS DateTime), 1, 2)
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
SET IDENTITY_INSERT [dbo].[Question] ON 

INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (30, N'bca', N'iyidir', 3, CAST(N'2023-11-28T15:16:42.650' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (31, N'abc', N'iyidir', 3, CAST(N'2023-11-21T15:16:58.520' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (32, N'furkansoru', N'aynen', 4, CAST(N'2023-11-24T18:19:02.547' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (33, N'mertinsoru', N'sa', 5, CAST(N'2023-11-29T23:42:31.530' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (34, N'mertinsoru2', N'naber', 5, CAST(N'2023-11-29T23:42:31.530' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (45, N'ddfdfdf', N'fdfdfd', 4, CAST(N'2024-01-04T18:21:50.520' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (46, N'fddfsf', N'dsfsdf', 3, CAST(N'2024-01-04T18:26:57.853' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (47, N'dsgfdg', N'gfdgfdg', 4, CAST(N'2024-01-04T18:29:08.963' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (48, N'deneme2', N'2', 4, CAST(N'2024-01-04T18:31:50.930' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (49, N'deneme3', N'denem3', 4, CAST(N'2024-01-04T18:32:18.497' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (50, N'son', N'son', 4, CAST(N'2024-01-04T21:33:22.190' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (51, N'denemeeee', N'fdfdfdfdf', 4, CAST(N'2024-01-04T21:38:01.617' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (52, N'dfdsfdsfdsfs', N'fdsfdsfds', 4, CAST(N'2024-01-04T21:38:27.577' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (86, N'g', N'f', 6, CAST(N'2024-01-05T00:34:48.513' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (87, N'b', N'l', 8, CAST(N'2024-01-05T00:38:04.360' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (123, N'1', N'1', 5, CAST(N'2024-01-06T13:36:48.593' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (124, N'2', N'2', 5, CAST(N'2024-01-06T13:37:50.740' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (125, N'4', N'4', 5, CAST(N'2024-01-06T13:44:44.623' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (126, N'5', N'5', 5, CAST(N'2024-01-06T13:46:09.030' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (127, N'1', N'1', 12, CAST(N'2024-01-06T13:55:55.503' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (128, N'2', N'2', 12, CAST(N'2024-01-06T13:56:13.070' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (129, N'3', N'3', 12, CAST(N'2024-01-06T13:56:25.080' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (130, N'5', N'5', 12, CAST(N'2024-01-06T13:59:36.393' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (131, N'6', N'6', 12, CAST(N'2024-01-06T13:59:49.390' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (132, N'6', N'6', 12, CAST(N'2024-01-06T14:02:40.413' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (133, N'2', N'2', 8, CAST(N'2024-01-06T14:07:57.720' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (134, N'66', N'66', 8, CAST(N'2024-01-06T14:17:14.113' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (135, N'77', N'77', 8, CAST(N'2024-01-06T14:18:22.907' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (136, N'55', N'55', 8, CAST(N'2024-01-06T14:20:29.570' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (137, N'99', N'99', 12, CAST(N'2024-01-06T14:21:09.860' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (138, N'66', N'66', 8, CAST(N'2024-01-06T14:21:26.983' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (139, N'77', N'77', 12, CAST(N'2024-01-06T14:21:46.097' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (140, N'22', N'22', 6, CAST(N'2024-01-06T14:21:54.153' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (141, N'a', N'sdsds', 12, CAST(N'2024-01-07T19:53:12.530' AS DateTime), 1)
INSERT [dbo].[Question] ([ID], [QuestionName], [Description], [StudentID], [Date], [isActive]) VALUES (142, N'soru 1', N's', 3, CAST(N'2024-01-08T19:54:53.507' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Question] OFF
GO
SET IDENTITY_INSERT [dbo].[Report] ON 

INSERT [dbo].[Report] ([ID], [StudentID], [Description], [Score], [Date], [WeekOfYear], [isActive]) VALUES (1, 4, N'asdasd', 342, CAST(N'2023-10-03T00:00:00.000' AS DateTime), 3, 0)
INSERT [dbo].[Report] ([ID], [StudentID], [Description], [Score], [Date], [WeekOfYear], [isActive]) VALUES (3, 3, N'dasfdsfgsd', 99, CAST(N'2023-10-02T01:00:00.000' AS DateTime), 3, 1)
SET IDENTITY_INSERT [dbo].[Report] OFF
GO
INSERT [dbo].[Status] ([ID], [Name], [isActive]) VALUES (101, N'Devam Ediyor', NULL)
INSERT [dbo].[Status] ([ID], [Name], [isActive]) VALUES (102, N'Mezun Oldu', NULL)
INSERT [dbo].[Status] ([ID], [Name], [isActive]) VALUES (103, N'Elendi', NULL)
INSERT [dbo].[Status] ([ID], [Name], [isActive]) VALUES (201, N'Normal Sınav', NULL)
INSERT [dbo].[Status] ([ID], [Name], [isActive]) VALUES (202, N'External Sınav', NULL)
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (3, N'Muhammet Enes', N'Ay', N'menes@gmail.com', 101, 2, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (4, N'Furkan', N'Gökırmak', N'furkan@gmail.com', 101, 2, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (5, N'Mert', N'Musabeşeoğlu', N'mert@gmail.com', 101, 2, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (6, N'Esma', N'Bediz', N'esma@gmail.com', 101, 2, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (7, N'Semih', N'Kaya', N'semih@gmail.com', 101, 1, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (8, N'Mahmut', N'Tuncer', N'mahmut@gmail.com', 103, 2, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (9, N'Sarp', N'İpekçi', N'sarp@gmail.com', 102, 1, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (10, N'Mert', N'Dalkıran', N'mertdalkiran@gmail.com', 102, 1, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (11, N'Can', N'İçcan', N'can@gmail.com', 102, 1, NULL)
INSERT [dbo].[Student] ([ID], [FirstName], [LastName], [Email], [StatusID], [ClassID], [isActive]) VALUES (12, N'abece', N'dsdfsdf', N'fd@gmail.com', 101, 2, 0)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (3, 1, 29, N'enesinSinav1', 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (3, 2, 50, N'EnesinSinav2', 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (3, 4, 60, N'externalSinavDescription', 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (3, 5, 70, N'ExternalSinavDesciption2', 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (4, 2, 10, N'furki', 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (5, 2, 45, N'mertinsinav', 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (6, 1, 34, N'esmaninsinav', 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (6, 2, 0, NULL, 1)
INSERT [dbo].[StudentExam] ([StudentID], [ExamID], [Score], [Description], [isActive]) VALUES (12, 2, 0, NULL, 1)
GO
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (3, 2, 100, N'mütiş', NULL)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (3, 15, NULL, NULL, 1)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (4, 13, 15, N'idare eder', 1)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (4, 14, NULL, NULL, 1)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (5, 13, 10, N'ee', 1)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (5, 14, NULL, NULL, 1)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (6, 14, NULL, NULL, 1)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (9, 1, 90, N'dümen', NULL)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (9, 2, 95, N'Güzel', NULL)
INSERT [dbo].[StudentProject] ([StudentID], [ProjectID], [Score], [Description], [isActive]) VALUES (10, 1, 85, N'yaaani', NULL)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Class_StartedDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Class] ADD  CONSTRAINT [DF_Class_StartedDate]  DEFAULT (getdate()) FOR [StartedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Class_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Class] ADD  CONSTRAINT [DF_Class_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Exam_Date]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Exam] ADD  CONSTRAINT [DF_Exam_Date]  DEFAULT (getdate()) FOR [Date]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Exam_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Exam] ADD  CONSTRAINT [DF_Exam_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Project_Date]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_Date]  DEFAULT (getdate()) FOR [StartedDate]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Project_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Question_Date]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_Date]  DEFAULT (getdate()) FOR [Date]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Question_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Report_Date]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Report] ADD  CONSTRAINT [DF_Report_Date]  DEFAULT (getdate()) FOR [Date]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Report_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Report] ADD  CONSTRAINT [DF_Report_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Status_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Student_StatusID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_StatusID]  DEFAULT ((101)) FOR [StatusID]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Student_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_StudentExam_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StudentExam] ADD  CONSTRAINT [DF_StudentExam_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_StudentProject_isActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StudentProject] ADD  CONSTRAINT [DF_StudentProject_isActive]  DEFAULT ((1)) FOR [isActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Exam_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[Exam]'))
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Exam_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Exam_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[Exam]'))
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK_Exam_Class]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Exam_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Exam]'))
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Exam_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Exam_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Exam]'))
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK_Exam_Status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Project_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[Project]'))
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Project_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[Project]'))
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Class]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Question_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Question]'))
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Question_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Question]'))
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Student]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Report_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Report]'))
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Report_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Report]'))
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_Student]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Class]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Class]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentExam_Exam]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentExam]'))
ALTER TABLE [dbo].[StudentExam]  WITH CHECK ADD  CONSTRAINT [FK_StudentExam_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exam] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentExam_Exam]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentExam]'))
ALTER TABLE [dbo].[StudentExam] CHECK CONSTRAINT [FK_StudentExam_Exam]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentExam_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentExam]'))
ALTER TABLE [dbo].[StudentExam]  WITH CHECK ADD  CONSTRAINT [FK_StudentExam_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentExam_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentExam]'))
ALTER TABLE [dbo].[StudentExam] CHECK CONSTRAINT [FK_StudentExam_Student]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentProject_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentProject]'))
ALTER TABLE [dbo].[StudentProject]  WITH CHECK ADD  CONSTRAINT [FK_StudentProject_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentProject_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentProject]'))
ALTER TABLE [dbo].[StudentProject] CHECK CONSTRAINT [FK_StudentProject_Project]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentProject_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentProject]'))
ALTER TABLE [dbo].[StudentProject]  WITH CHECK ADD  CONSTRAINT [FK_StudentProject_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_StudentProject_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[StudentProject]'))
ALTER TABLE [dbo].[StudentProject] CHECK CONSTRAINT [FK_StudentProject_Student]
GO
USE [master]
GO
ALTER DATABASE [StudentTrackingDB] SET  READ_WRITE 
GO
