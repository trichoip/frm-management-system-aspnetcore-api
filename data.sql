USE [master]
GO
/****** Object:  Database [FRMManagement]    Script Date: 25/02/2023 19:52:51 ******/
CREATE DATABASE [FRMManagement]

GO
ALTER DATABASE [FRMManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FRMManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FRMManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FRMManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FRMManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FRMManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FRMManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [FRMManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FRMManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FRMManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FRMManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FRMManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FRMManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FRMManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FRMManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FRMManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FRMManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FRMManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FRMManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FRMManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FRMManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FRMManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FRMManagement] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FRMManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FRMManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [FRMManagement] SET  MULTI_USER 
GO
ALTER DATABASE [FRMManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FRMManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FRMManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FRMManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FRMManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FRMManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FRMManagement', N'ON'
GO
ALTER DATABASE [FRMManagement] SET QUERY_STORE = OFF
GO
USE [FRMManagement]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[assignmentSchemas]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[assignmentSchemas](
	[IDSyllabus] [bigint] NOT NULL,
	[PercentQuiz] [float] NULL,
	[PercentAssigment] [float] NULL,
	[PercentFinal] [float] NULL,
	[PercentTheory] [float] NULL,
	[PercentFinalPractice] [float] NULL,
	[PassingCriterial] [float] NULL,
 CONSTRAINT [PK_assignmentSchemas] PRIMARY KEY CLUSTERED 
(
	[IDSyllabus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttendeeTypes]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AttendeeTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassAdmins]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassAdmins](
	[IdUser] [bigint] NOT NULL,
	[IdClass] [bigint] NOT NULL,
 CONSTRAINT [PK_ClassAdmins] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ClassCode] [varchar](50) NULL,
	[Status] [int] NOT NULL,
	[StartTimeLearning] [time](7) NULL,
	[EndTimeLearing] [time](7) NULL,
	[ReviewedBy] [bigint] NULL,
	[ReviewedOn] [date] NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedOn] [date] NOT NULL,
	[ApprovedBy] [bigint] NULL,
	[ApprovedOn] [date] NULL,
	[PlannedAtendee] [int] NULL,
	[ActualAttendee] [int] NULL,
	[AcceptedAttendee] [int] NULL,
	[CurrentSession] [int] NULL,
	[CurrentUnit] [int] NULL,
	[StartYear] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[ClassNumber] [int] NOT NULL,
	[IdProgram] [bigint] NULL,
	[IdTechnicalGroup] [bigint] NULL,
	[IdFSU] [bigint] NULL,
	[IdFSUContact] [bigint] NULL,
	[IdStatus] [bigint] NOT NULL,
	[IdSite] [bigint] NULL,
	[IdUniversity] [bigint] NULL,
	[IdFormatType] [bigint] NULL,
	[IdProgramContent] [bigint] NULL,
	[IdAttendeeType] [bigint] NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassFormatTypes]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassFormatTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClassFormatTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassLocations]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassLocations](
	[IdClass] [bigint] NOT NULL,
	[IdLocation] [bigint] NOT NULL,
 CONSTRAINT [PK_ClassLocations] PRIMARY KEY CLUSTERED 
(
	[IdClass] ASC,
	[IdLocation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[classMentors]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[classMentors](
	[IdUser] [bigint] NOT NULL,
	[IdClass] [bigint] NOT NULL,
 CONSTRAINT [PK_classMentors] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassProgramCodes]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassProgramCodes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProgramCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClassProgramCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassSelectedDates]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassSelectedDates](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdClass] [bigint] NOT NULL,
	[ActiveDate] [date] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_ClassSelectedDates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassSites]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassSites](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Site] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClassSites] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassStatuses]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassStatuses](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClassStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassTechnicalGroups]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassTechnicalGroups](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ClassTechnicalGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassTrainees]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassTrainees](
	[IdUser] [bigint] NOT NULL,
	[IdClass] [bigint] NOT NULL,
 CONSTRAINT [PK_ClassTrainees] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassUniversityCodes]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassUniversityCodes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UniversityCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClassUniversityCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassUpdateHistories]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassUpdateHistories](
	[IdClass] [bigint] NOT NULL,
	[ModifyBy] [bigint] NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ClassUpdateHistories] PRIMARY KEY CLUSTERED 
(
	[IdClass] ASC,
	[ModifyBy] ASC,
	[UpdateDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curricula]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curricula](
	[IdProgram] [bigint] NOT NULL,
	[IdSyllabus] [bigint] NOT NULL,
	[NumberOrder] [int] NOT NULL,
 CONSTRAINT [PK_Curricula] PRIMARY KEY CLUSTERED 
(
	[IdProgram] ASC,
	[IdSyllabus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryTypes]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DeliveryTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormatTypes]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormatTypes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FormatTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FsoftUnits]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FsoftUnits](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_FsoftUnits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FSUContactPoints]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FSUContactPoints](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IdFSU] [bigint] NOT NULL,
	[Contact] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_FSUContactPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryMaterials]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryMaterials](
	[IdUser] [bigint] NOT NULL,
	[IdMaterial] [bigint] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Action] [varchar](50) NOT NULL,
 CONSTRAINT [PK_HistoryMaterials] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdMaterial] ASC,
	[ModifiedOn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorySyllabi]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorySyllabi](
	[IdUser] [bigint] NOT NULL,
	[IdSyllabus] [bigint] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Action] [varchar](50) NOT NULL,
 CONSTRAINT [PK_HistorySyllabi] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdSyllabus] ASC,
	[ModifiedOn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryTrainingPrograms]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryTrainingPrograms](
	[IdUser] [bigint] NOT NULL,
	[IdProgram] [bigint] NOT NULL,
	[ModifiedOn] [date] NOT NULL,
 CONSTRAINT [PK_HistoryTrainingPrograms] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdProgram] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lessons]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lessons](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Duration] [int] NOT NULL,
	[IdDeliveryType] [bigint] NOT NULL,
	[IdFormatType] [bigint] NOT NULL,
	[IdOutputStandard] [bigint] NOT NULL,
	[Status] [int] NULL,
	[IdUnit] [bigint] NOT NULL,
 CONSTRAINT [PK_Lessons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Levels]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Levels](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Levels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materials]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materials](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[HyperLink] [nvarchar](max) NOT NULL,
	[IdLesson] [bigint] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutputStandards]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutputStandards](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OutputStandards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRights]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionRights](
	[IdRight] [bigint] NOT NULL,
	[IdRole] [bigint] NOT NULL,
	[IdPermission] [bigint] NOT NULL,
 CONSTRAINT [PK_PermissionRights] PRIMARY KEY CLUSTERED 
(
	[IdRight] ASC,
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[JwtId] [nvarchar](max) NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[IsRevoked] [bit] NOT NULL,
	[IssuedAt] [datetime2](7) NOT NULL,
	[ExpiredAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rights]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rights](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Rights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleRights]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleRights](
	[IDRight] [bigint] NOT NULL,
	[IDRole] [bigint] NOT NULL,
 CONSTRAINT [PK_RoleRights] PRIMARY KEY CLUSTERED 
(
	[IDRight] ASC,
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Index] [int] NOT NULL,
	[IdSyllabus] [bigint] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Syllabi]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Syllabi](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[AttendeeNumber] [int] NOT NULL,
	[Version] [float] NOT NULL,
	[Technicalrequirement] [ntext] NULL,
	[CourseObjectives] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[TrainingPrinciple] [ntext] NULL,
	[Description] [ntext] NULL,
	[HyperLink] [nvarchar](max) NULL,
	[IdLevel] [bigint] NOT NULL,
 CONSTRAINT [PK_Syllabi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SyllabusTrainers]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyllabusTrainers](
	[IdUser] [bigint] NOT NULL,
	[IdSyllabus] [bigint] NOT NULL,
 CONSTRAINT [PK_SyllabusTrainers] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC,
	[IdSyllabus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingPrograms]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingPrograms](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TrainingPrograms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Index] [int] NOT NULL,
	[IdSession] [bigint] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 25/02/2023 19:52:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Image] [varbinary](max) NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [char](1) NOT NULL,
	[Phone] [char](10) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[ResetPasswordOtp] [nvarchar](6) NULL,
	[IdRole] [bigint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221123013709_toan1', N'6.0.10')
GO
SET IDENTITY_INSERT [dbo].[AttendeeTypes] ON 

INSERT [dbo].[AttendeeTypes] ([Id], [Name]) VALUES (1, N'FRF')
INSERT [dbo].[AttendeeTypes] ([Id], [Name]) VALUES (2, N'FR')
INSERT [dbo].[AttendeeTypes] ([Id], [Name]) VALUES (3, N'CPL')
INSERT [dbo].[AttendeeTypes] ([Id], [Name]) VALUES (4, N'PFR')
INSERT [dbo].[AttendeeTypes] ([Id], [Name]) VALUES (5, N'CPLU')
SET IDENTITY_INSERT [dbo].[AttendeeTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([Id], [Name], [ClassCode], [Status], [StartTimeLearning], [EndTimeLearing], [ReviewedBy], [ReviewedOn], [CreatedBy], [CreatedOn], [ApprovedBy], [ApprovedOn], [PlannedAtendee], [ActualAttendee], [AcceptedAttendee], [CurrentSession], [CurrentUnit], [StartYear], [StartDate], [EndDate], [ClassNumber], [IdProgram], [IdTechnicalGroup], [IdFSU], [IdFSUContact], [IdStatus], [IdSite], [IdUniversity], [IdFormatType], [IdProgramContent], [IdAttendeeType]) VALUES (1, N'Class name', N'Class code', 1, CAST(N'08:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, CAST(N'2022-11-05' AS Date), 1, CAST(N'2022-11-04' AS Date), 1, CAST(N'2022-11-06' AS Date), 20, 18, 18, 1, 1, 2022, CAST(N'2022-11-07' AS Date), CAST(N'2022-12-01' AS Date), 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassFormatTypes] ON 

INSERT [dbo].[ClassFormatTypes] ([Id], [Name]) VALUES (1, N'Offline')
INSERT [dbo].[ClassFormatTypes] ([Id], [Name]) VALUES (2, N'Online')
INSERT [dbo].[ClassFormatTypes] ([Id], [Name]) VALUES (3, N'OJT')
INSERT [dbo].[ClassFormatTypes] ([Id], [Name]) VALUES (4, N'Virtual Training')
INSERT [dbo].[ClassFormatTypes] ([Id], [Name]) VALUES (5, N'Blended')
SET IDENTITY_INSERT [dbo].[ClassFormatTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassProgramCodes] ON 

INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (1, N'JAVA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (2, N'NET')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (3, N'FE')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (4, N'Android')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (5, N'CPP')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (6, N'Angular')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (7, N'REACT')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (8, N'PRN')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (9, N'EMB')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (10, N'OST')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (11, N'SP')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (12, N'TEST')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (13, N'IOS')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (14, N'COBOL')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (15, N'AUT')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (16, N'AI')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (17, N'DE')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (18, N'QA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (19, N'COMTOR')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (20, N'DevOps')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (21, N'SAP')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (22, N'AC')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (23, N'TC')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (24, N'GOL')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (25, N'Flutter')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (26, N'ServiceNow')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (27, N'PFR_JAVA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (28, N'FJW')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (29, N'JWD')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (30, N'JSE')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (31, N'PAD')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (32, N'FED')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (33, N'FNW')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (34, N'NWD')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (35, N'NPD')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (36, N'WAT')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (37, N'PRD')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (38, N'PML')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (39, N'ITF')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (40, N'FJB')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (41, N'OCA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (42, N'BA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (43, N'APM')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (44, N'DSA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (45, N'FIF')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (46, N'DEE')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (47, N'STE')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (48, N'Flexcube')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (49, N'OCP')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (50, N'FUJS')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (51, N'CES')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (52, N'CLOUD')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (53, N'PHP')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (54, N'NodeJS')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (55, N'ASE')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (56, N'MPP')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (57, N'DATA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (58, N'Sitecore')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (59, N'MAT')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (60, N'AND')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (61, N'ADR')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (62, N'JAVA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (63, N'Mobile')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (64, N'GST_JAVA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (65, N'LITE_CPP')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (66, N'WinApp')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (67, N'Magento')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (68, N'Python')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (69, N'RN')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (70, N'FUKS')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (71, N'RPA')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (72, N'Erlang')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (73, N'Golang')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (74, N'C++/Linux')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (75, N'AEM')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (76, N'GST_EMB')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (77, N'GST_NET')
INSERT [dbo].[ClassProgramCodes] ([Id], [ProgramCode]) VALUES (78, N'JP')
SET IDENTITY_INSERT [dbo].[ClassProgramCodes] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassSites] ON 

INSERT [dbo].[ClassSites] ([Id], [Site]) VALUES (1, N'HN')
INSERT [dbo].[ClassSites] ([Id], [Site]) VALUES (2, N'HCM')
INSERT [dbo].[ClassSites] ([Id], [Site]) VALUES (3, N'DN')
INSERT [dbo].[ClassSites] ([Id], [Site]) VALUES (4, N'CT')
INSERT [dbo].[ClassSites] ([Id], [Site]) VALUES (5, N'QN')
SET IDENTITY_INSERT [dbo].[ClassSites] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassStatuses] ON 

INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (1, N'Draft')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (2, N'Reviewing')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (3, N'Approving')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (4, N'Start')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (5, N'Done')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (6, N'Delayed')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (7, N'Opened')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (8, N'Active')
INSERT [dbo].[ClassStatuses] ([Id], [Name]) VALUES (9, N'Inactive')
SET IDENTITY_INSERT [dbo].[ClassStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassTechnicalGroups] ON 

INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (1, N'Java')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (2, N'.NET')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (3, N'FE')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (4, N'Android')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (5, N'CPP')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (6, N'Angular')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (7, N'React')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (8, N'Embedded')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (9, N'Out System')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (10, N'Sharepoint')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (11, N'iOS')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (12, N'Cobol')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (13, N'AUT')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (14, N'AI')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (15, N'Data')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (16, N'QA')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (17, N'Comtor')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (18, N'DevOps')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (19, N'SAP')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (20, N'ABAP')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (21, N'Go Lang')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (22, N'Flutter')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (23, N'ServiceNow')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (24, N'Front-End')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (25, N'Manual Test')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (26, N'Automation Test')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (27, N'C++')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (28, N'Python')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (29, N'IT')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (30, N'OCA8')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (31, N'BA')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (32, N'APM')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (33, N'DSA')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (34, N'FIF')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (35, N'STE')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (36, N'Flexcube')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (37, N'CLOUD')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (38, N'PHP')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (39, N'NodeJS')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (40, N'Security Engineer')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (41, N'Microsoft Power Platform')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (42, N'Data Engineer')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (43, N'Sitecore')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (44, N'Agile')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (45, N'React Native')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (46, N'Certificate')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (47, N'SAP,ABAP')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (48, N'Mobile')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (49, N'WinApp')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (50, N'PHP')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (51, N'RPA')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (52, N'Erlang')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (53, N'Fullstack Java')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (54, N'Fullstack .NET')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (55, N'Java Standard')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (56, N'.NET standard')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (57, N'Golang')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (58, N'C++/Linux')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (59, N'AEM')
INSERT [dbo].[ClassTechnicalGroups] ([Id], [Name]) VALUES (60, N'JP')
SET IDENTITY_INSERT [dbo].[ClassTechnicalGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassUniversityCodes] ON 

INSERT [dbo].[ClassUniversityCodes] ([Id], [UniversityCode]) VALUES (1, N'ALL')
SET IDENTITY_INSERT [dbo].[ClassUniversityCodes] OFF
GO
INSERT [dbo].[ClassUpdateHistories] ([IdClass], [ModifyBy], [UpdateDate]) VALUES (1, 1, CAST(N'2022-11-10T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[DeliveryTypes] ON 

INSERT [dbo].[DeliveryTypes] ([Id], [Name]) VALUES (1, N'Assignment/Lab')
INSERT [dbo].[DeliveryTypes] ([Id], [Name]) VALUES (2, N'Concept/Lecture')
INSERT [dbo].[DeliveryTypes] ([Id], [Name]) VALUES (3, N'Guide/Review')
INSERT [dbo].[DeliveryTypes] ([Id], [Name]) VALUES (4, N'Test/Quiz')
INSERT [dbo].[DeliveryTypes] ([Id], [Name]) VALUES (5, N'Exam')
INSERT [dbo].[DeliveryTypes] ([Id], [Name]) VALUES (6, N'Seminar/Workshop')
SET IDENTITY_INSERT [dbo].[DeliveryTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[FormatTypes] ON 

INSERT [dbo].[FormatTypes] ([Id], [Name]) VALUES (1, N'Offline')
INSERT [dbo].[FormatTypes] ([Id], [Name]) VALUES (2, N'Online')
SET IDENTITY_INSERT [dbo].[FormatTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[FsoftUnits] ON 

INSERT [dbo].[FsoftUnits] ([Id], [Name], [Status]) VALUES (1, N'FHM', 1)
INSERT [dbo].[FsoftUnits] ([Id], [Name], [Status]) VALUES (2, N'FU', 1)
INSERT [dbo].[FsoftUnits] ([Id], [Name], [Status]) VALUES (3, N'FPTN', 1)
SET IDENTITY_INSERT [dbo].[FsoftUnits] OFF
GO
SET IDENTITY_INSERT [dbo].[FSUContactPoints] ON 

INSERT [dbo].[FSUContactPoints] ([Id], [IdFSU], [Contact], [Status]) VALUES (1, 1, N'BaCH@fsoft.com.vn', 1)
INSERT [dbo].[FSUContactPoints] ([Id], [IdFSU], [Contact], [Status]) VALUES (2, 2, N'0912345678', 1)
SET IDENTITY_INSERT [dbo].[FSUContactPoints] OFF
GO
INSERT [dbo].[HistoryTrainingPrograms] ([IdUser], [IdProgram], [ModifiedOn]) VALUES (1, 1, CAST(N'2022-11-10' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Levels] ON 

INSERT [dbo].[Levels] ([Id], [Name]) VALUES (1, N'All level')
INSERT [dbo].[Levels] ([Id], [Name]) VALUES (2, N'Fresher')
INSERT [dbo].[Levels] ([Id], [Name]) VALUES (3, N'Intern')
SET IDENTITY_INSERT [dbo].[Levels] OFF
GO
SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([Id], [Name], [Status]) VALUES (1, N'FTown 1', 0)
INSERT [dbo].[Locations] ([Id], [Name], [Status]) VALUES (2, N'FTown 2', 0)
INSERT [dbo].[Locations] ([Id], [Name], [Status]) VALUES (3, N'FTown 3', 0)
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
SET IDENTITY_INSERT [dbo].[OutputStandards] ON 

INSERT [dbo].[OutputStandards] ([Id], [Name]) VALUES (1, N'H4SD')
INSERT [dbo].[OutputStandards] ([Id], [Name]) VALUES (2, N'K6SD')
INSERT [dbo].[OutputStandards] ([Id], [Name]) VALUES (3, N'H6SD')
INSERT [dbo].[OutputStandards] ([Id], [Name]) VALUES (4, N'H1ST')
INSERT [dbo].[OutputStandards] ([Id], [Name]) VALUES (5, N'H2SD')
INSERT [dbo].[OutputStandards] ([Id], [Name]) VALUES (6, N'K4SD')
INSERT [dbo].[OutputStandards] ([Id], [Name]) VALUES (7, N'K3SD ')
SET IDENTITY_INSERT [dbo].[OutputStandards] OFF
GO
INSERT [dbo].[PermissionRights] ([IdRight], [IdRole], [IdPermission]) VALUES (1, 1, 5)
INSERT [dbo].[PermissionRights] ([IdRight], [IdRole], [IdPermission]) VALUES (2, 1, 5)
INSERT [dbo].[PermissionRights] ([IdRight], [IdRole], [IdPermission]) VALUES (3, 1, 5)
INSERT [dbo].[PermissionRights] ([IdRight], [IdRole], [IdPermission]) VALUES (4, 1, 5)
INSERT [dbo].[PermissionRights] ([IdRight], [IdRole], [IdPermission]) VALUES (5, 1, 5)
INSERT [dbo].[PermissionRights] ([IdRight], [IdRole], [IdPermission]) VALUES (6, 1, 5)
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([Id], [Name]) VALUES (1, N'Access denied')
INSERT [dbo].[Permissions] ([Id], [Name]) VALUES (2, N'View')
INSERT [dbo].[Permissions] ([Id], [Name]) VALUES (3, N'Modify')
INSERT [dbo].[Permissions] ([Id], [Name]) VALUES (4, N'Create')
INSERT [dbo].[Permissions] ([Id], [Name]) VALUES (5, N'Full access')
INSERT [dbo].[Permissions] ([Id], [Name]) VALUES (6, N'Delete while viewing')
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'90f380af-194d-4497-a0e7-3ab442a4c965', 1, N'pCKxYY71RMRM+EaFjotM3EXTuSXPCDcKzpqSSZwBJS0=', N'20afac43-acd4-4661-b9ba-0122abf372a6', 0, 0, CAST(N'2022-12-06T15:38:11.4656863' AS DateTime2), CAST(N'2022-12-06T16:38:11.4657108' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'cc7cae4b-7f67-438a-a534-5e9b00937c45', 1, N'XVfsY9pUo+vEskp/FLTU/FWrOj8BN0bP2DlCoDcvK70=', N'31981d32-ea85-447f-945b-cda141612bb0', 0, 0, CAST(N'2022-12-09T09:40:10.0853286' AS DateTime2), CAST(N'2022-12-09T10:40:10.0853718' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'9ebd6c5e-0442-43ac-bcc0-6866ade063c3', 1, N'OyC28vPBFkbmTFmGVYMPDwzmMuXXXNfAja1PtFyzxdY=', N'08a6bc0d-21f2-46cb-819b-bae38fe35344', 0, 0, CAST(N'2022-12-06T15:42:00.1331760' AS DateTime2), CAST(N'2022-12-06T16:42:00.1332088' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'c41adb84-bacd-426a-b737-87dc87572bf4', 1, N'UnT94xdhvw+g7imAufQIwW5ubMRstuXi6xFs9NdMqpk=', N'14f3667b-7d5b-42d4-852d-691a8a01d828', 0, 0, CAST(N'2022-12-06T15:24:10.5418154' AS DateTime2), CAST(N'2022-12-06T16:24:10.5418401' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'771c2961-e46b-4e08-8624-a87d8d2e5ab8', 1, N'foUrkA2aUZt/ympAxPaRRC1hwYIpR5vIeyxHMMi3HJc=', N'dc702307-5752-435c-b612-d60078fd302c', 0, 0, CAST(N'2022-12-06T15:43:27.1869556' AS DateTime2), CAST(N'2022-12-06T16:43:27.1869821' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'3a550ed9-7ad6-4529-b7c4-b2983add3da3', 1, N'wLhm/ro+e0lrU0MqGB19Alw/5o6XvJYa3hFlFi5/J2Q=', N'e5c6eefd-cb00-4c1f-a576-e2b6de79a353', 0, 0, CAST(N'2022-12-06T15:24:50.9847986' AS DateTime2), CAST(N'2022-12-06T16:24:50.9847992' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'e8afddf1-93ed-462c-b42d-d690da5df201', 1, N'Bj/6Phmt/Cv9gsYQwmQpZ4SlbKGf6e5CRXZbjM7ywHU=', N'346badcd-95e1-4704-bc34-a2f1c3f443b5', 0, 0, CAST(N'2022-12-06T15:16:20.8144886' AS DateTime2), CAST(N'2022-12-06T16:16:20.8145128' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'67aa05f6-c01d-490e-80a2-e7e3ba300be9', 1, N'BL5jI2sf5z6W2M9F0UZNvshlau3v3xccLD2dGuPtMkQ=', N'09a800a2-3649-4980-9f50-e3352703e146', 0, 0, CAST(N'2022-12-06T15:52:36.3895217' AS DateTime2), CAST(N'2022-12-06T16:52:36.3895484' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'934f32ab-24cd-44c4-8792-eb2b58e87133', 1, N'mYOrVNan52qrkSs3lwftFQmcfjw0sR99RRRDUvqWWUg=', N'84476b3e-2a46-495e-9578-d38cf3de36bc', 0, 0, CAST(N'2022-12-06T08:39:36.4651503' AS DateTime2), CAST(N'2022-12-06T09:39:36.4651908' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'1108eab1-f36a-412b-aea2-eb55ca08a202', 1, N'wsHgj1xY/WsyKjHz8Q20BJv/OSGr9h0FCKAF5+V1ouE=', N'b38f6ea1-6c8b-4de8-a96a-aee3d43a4f83', 0, 0, CAST(N'2022-12-06T15:46:09.1367051' AS DateTime2), CAST(N'2022-12-06T16:46:09.1367427' AS DateTime2))
INSERT [dbo].[RefreshTokens] ([Id], [UserId], [Token], [JwtId], [IsUsed], [IsRevoked], [IssuedAt], [ExpiredAt]) VALUES (N'44adfe4d-fa93-4213-b19e-f778d2f05bb6', 1, N'nr+KVuSAtz2/BjFfqyh2PDkTacgq6AIRpBKgBygAvR4=', N'4bb96f8b-102f-4558-820f-2b291365ead8', 0, 0, CAST(N'2022-12-06T15:44:47.6013915' AS DateTime2), CAST(N'2022-12-06T16:44:47.6014219' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Rights] ON 

INSERT [dbo].[Rights] ([Id], [Name]) VALUES (1, N'Syllabus')
INSERT [dbo].[Rights] ([Id], [Name]) VALUES (2, N'Training program')
INSERT [dbo].[Rights] ([Id], [Name]) VALUES (3, N'Class')
INSERT [dbo].[Rights] ([Id], [Name]) VALUES (4, N'Learning material')
INSERT [dbo].[Rights] ([Id], [Name]) VALUES (5, N'User')
INSERT [dbo].[Rights] ([Id], [Name]) VALUES (6, N'Training calendar')
SET IDENTITY_INSERT [dbo].[Rights] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Super Admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Class Admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'Trainer')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (4, N'Student')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (5, N'ABC')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[TrainingPrograms] ON 

INSERT [dbo].[TrainingPrograms] ([Id], [Name], [Status]) VALUES (1, N'C# Foundation', 1)
SET IDENTITY_INSERT [dbo].[TrainingPrograms] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [UserName], [Password], [FullName], [Image], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Status], [ResetPasswordOtp], [IdRole]) VALUES (1, N'superadmin@fsoft.com', N'$2a$11$k7zKp9cQOIE3/c22YdD29O52l8x.9bbji4kJOPJ3Jy.f4kIUYIQ0G', N'Super Admin', NULL, CAST(N'2000-08-02' AS Date), N'M', N'0123456789', N'superadmin@fsoft.com', N'123 đường 456', 1, N'374523', 1)
INSERT [dbo].[Users] ([ID], [UserName], [Password], [FullName], [Image], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Status], [ResetPasswordOtp], [IdRole]) VALUES (2, N'classadmin@fsoft.com', N'$2a$11$IWL97xH2L60fhHMoo38msOYC7ZsP6GsrpnO.CLS04IRNBkqs8TdWS', N'Class Admin', NULL, CAST(N'2000-08-02' AS Date), N'F', N'0123456789', N'classadmin@fsoft.com', N'123 đường 456', 1, NULL, 2)
INSERT [dbo].[Users] ([ID], [UserName], [Password], [FullName], [Image], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Status], [ResetPasswordOtp], [IdRole]) VALUES (3, N'trainer@fsoft.com', N'$2a$11$4/mkPNwz0l/.e7zXfRT69eKsP327tqz10Ldf5s0iWAZLNCWRRRrxK', N'Trainer', NULL, CAST(N'2000-08-02' AS Date), N'M', N'0123456789', N'trainer@fsoft.com', N'123 đường 456', 1, NULL, 3)
INSERT [dbo].[Users] ([ID], [UserName], [Password], [FullName], [Image], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Status], [ResetPasswordOtp], [IdRole]) VALUES (4, N'student@fsoft.com', N'$2a$11$WTeAE4MdAkR4ZtVooCdZkuuzam5sdxDTpm1VJqL/RFIEJNLJk.PX2', N'Student', NULL, CAST(N'2000-08-02' AS Date), N'M', N'0123456789', N'student@fsoft.com', N'123 đường 456', 1, NULL, 4)
INSERT [dbo].[Users] ([ID], [UserName], [Password], [FullName], [Image], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Status], [ResetPasswordOtp], [IdRole]) VALUES (5, N'Hoa', N'$2a$11$DBML8vYARig46rwtrIet8ewkggg6Ej.8FJyKKQThcjGk.iq1BK.IS', N'Ngoc Hoa', NULL, CAST(N'2022-10-31' AS Date), N'M', N'2316843218', N'hoa@gmail.com', N'HCM', 1, NULL, 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_ClassAdmins_IdClass]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_ClassAdmins_IdClass] ON [dbo].[ClassAdmins]
(
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_ApprovedBy]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_ApprovedBy] ON [dbo].[Classes]
(
	[ApprovedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_CreatedBy]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_CreatedBy] ON [dbo].[Classes]
(
	[CreatedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdAttendeeType]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdAttendeeType] ON [dbo].[Classes]
(
	[IdAttendeeType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdFormatType]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdFormatType] ON [dbo].[Classes]
(
	[IdFormatType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdFSU]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdFSU] ON [dbo].[Classes]
(
	[IdFSU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdFSUContact]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdFSUContact] ON [dbo].[Classes]
(
	[IdFSUContact] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdProgram]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdProgram] ON [dbo].[Classes]
(
	[IdProgram] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdProgramContent]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdProgramContent] ON [dbo].[Classes]
(
	[IdProgramContent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdSite]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdSite] ON [dbo].[Classes]
(
	[IdSite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdStatus]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdStatus] ON [dbo].[Classes]
(
	[IdStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdTechnicalGroup]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdTechnicalGroup] ON [dbo].[Classes]
(
	[IdTechnicalGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_IdUniversity]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_IdUniversity] ON [dbo].[Classes]
(
	[IdUniversity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classes_ReviewedBy]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Classes_ReviewedBy] ON [dbo].[Classes]
(
	[ReviewedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ClassLocations_IdLocation]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_ClassLocations_IdLocation] ON [dbo].[ClassLocations]
(
	[IdLocation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_classMentors_IdClass]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_classMentors_IdClass] ON [dbo].[classMentors]
(
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ClassSelectedDates_IdClass]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_ClassSelectedDates_IdClass] ON [dbo].[ClassSelectedDates]
(
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ClassTrainees_IdClass]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_ClassTrainees_IdClass] ON [dbo].[ClassTrainees]
(
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ClassUpdateHistories_ModifyBy]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_ClassUpdateHistories_ModifyBy] ON [dbo].[ClassUpdateHistories]
(
	[ModifyBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Curricula_IdSyllabus]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Curricula_IdSyllabus] ON [dbo].[Curricula]
(
	[IdSyllabus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FSUContactPoints_IdFSU]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_FSUContactPoints_IdFSU] ON [dbo].[FSUContactPoints]
(
	[IdFSU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HistoryMaterials_IdMaterial]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_HistoryMaterials_IdMaterial] ON [dbo].[HistoryMaterials]
(
	[IdMaterial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HistorySyllabi_IdSyllabus]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_HistorySyllabi_IdSyllabus] ON [dbo].[HistorySyllabi]
(
	[IdSyllabus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HistoryTrainingPrograms_IdProgram]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_HistoryTrainingPrograms_IdProgram] ON [dbo].[HistoryTrainingPrograms]
(
	[IdProgram] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lessons_IdDeliveryType]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Lessons_IdDeliveryType] ON [dbo].[Lessons]
(
	[IdDeliveryType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lessons_IdFormatType]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Lessons_IdFormatType] ON [dbo].[Lessons]
(
	[IdFormatType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lessons_IdOutputStandard]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Lessons_IdOutputStandard] ON [dbo].[Lessons]
(
	[IdOutputStandard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lessons_IdUnit]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Lessons_IdUnit] ON [dbo].[Lessons]
(
	[IdUnit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Materials_IdLesson]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Materials_IdLesson] ON [dbo].[Materials]
(
	[IdLesson] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermissionRights_IdPermission]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_PermissionRights_IdPermission] ON [dbo].[PermissionRights]
(
	[IdPermission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermissionRights_IdRole]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_PermissionRights_IdRole] ON [dbo].[PermissionRights]
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RefreshTokens_UserId]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_RefreshTokens_UserId] ON [dbo].[RefreshTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleRights_IDRole]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_RoleRights_IDRole] ON [dbo].[RoleRights]
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sessions_IdSyllabus]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Sessions_IdSyllabus] ON [dbo].[Sessions]
(
	[IdSyllabus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Syllabi_IdLevel]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Syllabi_IdLevel] ON [dbo].[Syllabi]
(
	[IdLevel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SyllabusTrainers_IdSyllabus]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_SyllabusTrainers_IdSyllabus] ON [dbo].[SyllabusTrainers]
(
	[IdSyllabus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Units_IdSession]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Units_IdSession] ON [dbo].[Units]
(
	[IdSession] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_IdRole]    Script Date: 25/02/2023 19:52:51 ******/
CREATE NONCLUSTERED INDEX [IX_Users_IdRole] ON [dbo].[Users]
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[assignmentSchemas]  WITH CHECK ADD  CONSTRAINT [FK_assignmentSchemas_Syllabi_IDSyllabus] FOREIGN KEY([IDSyllabus])
REFERENCES [dbo].[Syllabi] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[assignmentSchemas] CHECK CONSTRAINT [FK_assignmentSchemas_Syllabi_IDSyllabus]
GO
ALTER TABLE [dbo].[ClassAdmins]  WITH CHECK ADD  CONSTRAINT [FK_ClassAdmins_Classes_IdClass] FOREIGN KEY([IdClass])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassAdmins] CHECK CONSTRAINT [FK_ClassAdmins_Classes_IdClass]
GO
ALTER TABLE [dbo].[ClassAdmins]  WITH CHECK ADD  CONSTRAINT [FK_ClassAdmins_Users_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[ClassAdmins] CHECK CONSTRAINT [FK_ClassAdmins_Users_IdUser]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_AttendeeTypes_IdAttendeeType] FOREIGN KEY([IdAttendeeType])
REFERENCES [dbo].[AttendeeTypes] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_AttendeeTypes_IdAttendeeType]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_ClassFormatTypes_IdFormatType] FOREIGN KEY([IdFormatType])
REFERENCES [dbo].[ClassFormatTypes] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_ClassFormatTypes_IdFormatType]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_ClassProgramCodes_IdProgramContent] FOREIGN KEY([IdProgramContent])
REFERENCES [dbo].[ClassProgramCodes] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_ClassProgramCodes_IdProgramContent]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_ClassSites_IdSite] FOREIGN KEY([IdSite])
REFERENCES [dbo].[ClassSites] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_ClassSites_IdSite]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_ClassStatuses_IdStatus] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[ClassStatuses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_ClassStatuses_IdStatus]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_ClassTechnicalGroups_IdTechnicalGroup] FOREIGN KEY([IdTechnicalGroup])
REFERENCES [dbo].[ClassTechnicalGroups] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_ClassTechnicalGroups_IdTechnicalGroup]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_ClassUniversityCodes_IdUniversity] FOREIGN KEY([IdUniversity])
REFERENCES [dbo].[ClassUniversityCodes] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_ClassUniversityCodes_IdUniversity]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_FsoftUnits_IdFSU] FOREIGN KEY([IdFSU])
REFERENCES [dbo].[FsoftUnits] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_FsoftUnits_IdFSU]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_FSUContactPoints_IdFSUContact] FOREIGN KEY([IdFSUContact])
REFERENCES [dbo].[FSUContactPoints] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_FSUContactPoints_IdFSUContact]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_TrainingPrograms_IdProgram] FOREIGN KEY([IdProgram])
REFERENCES [dbo].[TrainingPrograms] ([Id])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_TrainingPrograms_IdProgram]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Users_ApprovedBy] FOREIGN KEY([ApprovedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Users_ApprovedBy]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Users_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Users_CreatedBy]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Users_ReviewedBy] FOREIGN KEY([ReviewedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Users_ReviewedBy]
GO
ALTER TABLE [dbo].[ClassLocations]  WITH CHECK ADD  CONSTRAINT [FK_ClassLocations_Classes_IdClass] FOREIGN KEY([IdClass])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassLocations] CHECK CONSTRAINT [FK_ClassLocations_Classes_IdClass]
GO
ALTER TABLE [dbo].[ClassLocations]  WITH CHECK ADD  CONSTRAINT [FK_ClassLocations_Locations_IdLocation] FOREIGN KEY([IdLocation])
REFERENCES [dbo].[Locations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassLocations] CHECK CONSTRAINT [FK_ClassLocations_Locations_IdLocation]
GO
ALTER TABLE [dbo].[classMentors]  WITH CHECK ADD  CONSTRAINT [FK_classMentors_Classes_IdClass] FOREIGN KEY([IdClass])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[classMentors] CHECK CONSTRAINT [FK_classMentors_Classes_IdClass]
GO
ALTER TABLE [dbo].[classMentors]  WITH CHECK ADD  CONSTRAINT [FK_classMentors_Users_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[classMentors] CHECK CONSTRAINT [FK_classMentors_Users_IdUser]
GO
ALTER TABLE [dbo].[ClassSelectedDates]  WITH CHECK ADD  CONSTRAINT [FK_ClassSelectedDates_Classes_IdClass] FOREIGN KEY([IdClass])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassSelectedDates] CHECK CONSTRAINT [FK_ClassSelectedDates_Classes_IdClass]
GO
ALTER TABLE [dbo].[ClassTrainees]  WITH CHECK ADD  CONSTRAINT [FK_ClassTrainees_Classes_IdClass] FOREIGN KEY([IdClass])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassTrainees] CHECK CONSTRAINT [FK_ClassTrainees_Classes_IdClass]
GO
ALTER TABLE [dbo].[ClassTrainees]  WITH CHECK ADD  CONSTRAINT [FK_ClassTrainees_Users_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[ClassTrainees] CHECK CONSTRAINT [FK_ClassTrainees_Users_IdUser]
GO
ALTER TABLE [dbo].[ClassUpdateHistories]  WITH CHECK ADD  CONSTRAINT [FK_ClassUpdateHistories_Classes_IdClass] FOREIGN KEY([IdClass])
REFERENCES [dbo].[Classes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClassUpdateHistories] CHECK CONSTRAINT [FK_ClassUpdateHistories_Classes_IdClass]
GO
ALTER TABLE [dbo].[ClassUpdateHistories]  WITH CHECK ADD  CONSTRAINT [FK_ClassUpdateHistories_Users_ModifyBy] FOREIGN KEY([ModifyBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[ClassUpdateHistories] CHECK CONSTRAINT [FK_ClassUpdateHistories_Users_ModifyBy]
GO
ALTER TABLE [dbo].[Curricula]  WITH CHECK ADD  CONSTRAINT [FK_Curricula_Syllabi_IdSyllabus] FOREIGN KEY([IdSyllabus])
REFERENCES [dbo].[Syllabi] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Curricula] CHECK CONSTRAINT [FK_Curricula_Syllabi_IdSyllabus]
GO
ALTER TABLE [dbo].[Curricula]  WITH CHECK ADD  CONSTRAINT [FK_Curricula_TrainingPrograms_IdProgram] FOREIGN KEY([IdProgram])
REFERENCES [dbo].[TrainingPrograms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Curricula] CHECK CONSTRAINT [FK_Curricula_TrainingPrograms_IdProgram]
GO
ALTER TABLE [dbo].[FSUContactPoints]  WITH CHECK ADD  CONSTRAINT [FK_FSUContactPoints_FsoftUnits_IdFSU] FOREIGN KEY([IdFSU])
REFERENCES [dbo].[FsoftUnits] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FSUContactPoints] CHECK CONSTRAINT [FK_FSUContactPoints_FsoftUnits_IdFSU]
GO
ALTER TABLE [dbo].[HistoryMaterials]  WITH CHECK ADD  CONSTRAINT [FK_HistoryMaterials_Materials_IdMaterial] FOREIGN KEY([IdMaterial])
REFERENCES [dbo].[Materials] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistoryMaterials] CHECK CONSTRAINT [FK_HistoryMaterials_Materials_IdMaterial]
GO
ALTER TABLE [dbo].[HistoryMaterials]  WITH CHECK ADD  CONSTRAINT [FK_HistoryMaterials_Users_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistoryMaterials] CHECK CONSTRAINT [FK_HistoryMaterials_Users_IdUser]
GO
ALTER TABLE [dbo].[HistorySyllabi]  WITH CHECK ADD  CONSTRAINT [FK_HistorySyllabi_Syllabi_IdSyllabus] FOREIGN KEY([IdSyllabus])
REFERENCES [dbo].[Syllabi] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistorySyllabi] CHECK CONSTRAINT [FK_HistorySyllabi_Syllabi_IdSyllabus]
GO
ALTER TABLE [dbo].[HistorySyllabi]  WITH CHECK ADD  CONSTRAINT [FK_HistorySyllabi_Users_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistorySyllabi] CHECK CONSTRAINT [FK_HistorySyllabi_Users_IdUser]
GO
ALTER TABLE [dbo].[HistoryTrainingPrograms]  WITH CHECK ADD  CONSTRAINT [FK_HistoryTrainingPrograms_TrainingPrograms_IdProgram] FOREIGN KEY([IdProgram])
REFERENCES [dbo].[TrainingPrograms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistoryTrainingPrograms] CHECK CONSTRAINT [FK_HistoryTrainingPrograms_TrainingPrograms_IdProgram]
GO
ALTER TABLE [dbo].[HistoryTrainingPrograms]  WITH CHECK ADD  CONSTRAINT [FK_HistoryTrainingPrograms_Users_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistoryTrainingPrograms] CHECK CONSTRAINT [FK_HistoryTrainingPrograms_Users_IdUser]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_DeliveryTypes_IdDeliveryType] FOREIGN KEY([IdDeliveryType])
REFERENCES [dbo].[DeliveryTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_DeliveryTypes_IdDeliveryType]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_FormatTypes_IdFormatType] FOREIGN KEY([IdFormatType])
REFERENCES [dbo].[FormatTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_FormatTypes_IdFormatType]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_OutputStandards_IdOutputStandard] FOREIGN KEY([IdOutputStandard])
REFERENCES [dbo].[OutputStandards] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_OutputStandards_IdOutputStandard]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Lessons_Units_IdUnit] FOREIGN KEY([IdUnit])
REFERENCES [dbo].[Units] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Lessons_Units_IdUnit]
GO
ALTER TABLE [dbo].[Materials]  WITH CHECK ADD  CONSTRAINT [FK_Materials_Lessons_IdLesson] FOREIGN KEY([IdLesson])
REFERENCES [dbo].[Lessons] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Materials] CHECK CONSTRAINT [FK_Materials_Lessons_IdLesson]
GO
ALTER TABLE [dbo].[PermissionRights]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRights_Permissions_IdPermission] FOREIGN KEY([IdPermission])
REFERENCES [dbo].[Permissions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermissionRights] CHECK CONSTRAINT [FK_PermissionRights_Permissions_IdPermission]
GO
ALTER TABLE [dbo].[PermissionRights]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRights_Rights_IdRight] FOREIGN KEY([IdRight])
REFERENCES [dbo].[Rights] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermissionRights] CHECK CONSTRAINT [FK_PermissionRights_Rights_IdRight]
GO
ALTER TABLE [dbo].[PermissionRights]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRights_Roles_IdRole] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermissionRights] CHECK CONSTRAINT [FK_PermissionRights_Roles_IdRole]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users_UserId]
GO
ALTER TABLE [dbo].[RoleRights]  WITH CHECK ADD  CONSTRAINT [FK_RoleRights_Rights_IDRight] FOREIGN KEY([IDRight])
REFERENCES [dbo].[Rights] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleRights] CHECK CONSTRAINT [FK_RoleRights_Rights_IDRight]
GO
ALTER TABLE [dbo].[RoleRights]  WITH CHECK ADD  CONSTRAINT [FK_RoleRights_Roles_IDRole] FOREIGN KEY([IDRole])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleRights] CHECK CONSTRAINT [FK_RoleRights_Roles_IDRole]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Syllabi_IdSyllabus] FOREIGN KEY([IdSyllabus])
REFERENCES [dbo].[Syllabi] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Syllabi_IdSyllabus]
GO
ALTER TABLE [dbo].[Syllabi]  WITH CHECK ADD  CONSTRAINT [FK_Syllabi_Levels_IdLevel] FOREIGN KEY([IdLevel])
REFERENCES [dbo].[Levels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Syllabi] CHECK CONSTRAINT [FK_Syllabi_Levels_IdLevel]
GO
ALTER TABLE [dbo].[SyllabusTrainers]  WITH CHECK ADD  CONSTRAINT [FK_SyllabusTrainers_Syllabi_IdSyllabus] FOREIGN KEY([IdSyllabus])
REFERENCES [dbo].[Syllabi] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SyllabusTrainers] CHECK CONSTRAINT [FK_SyllabusTrainers_Syllabi_IdSyllabus]
GO
ALTER TABLE [dbo].[SyllabusTrainers]  WITH CHECK ADD  CONSTRAINT [FK_SyllabusTrainers_Users_IdUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SyllabusTrainers] CHECK CONSTRAINT [FK_SyllabusTrainers_Users_IdUser]
GO
ALTER TABLE [dbo].[Units]  WITH CHECK ADD  CONSTRAINT [FK_Units_Sessions_IdSession] FOREIGN KEY([IdSession])
REFERENCES [dbo].[Sessions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Units] CHECK CONSTRAINT [FK_Units_Sessions_IdSession]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_IdRole] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_IdRole]
GO
USE [master]
GO
ALTER DATABASE [FRMManagement] SET  READ_WRITE 
GO
