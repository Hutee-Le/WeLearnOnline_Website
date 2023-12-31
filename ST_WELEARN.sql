USE [master]
GO
/****** Object:  Database [ST_WELEARN]    Script Date: 10/21/2023 7:37:00 AM ******/
CREATE DATABASE [ST_WELEARN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ST_WeLearn', FILENAME = N'D:\DO_AN_CHUYEN_NGANH_A\sql\ST_WeLearn.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ST_WeLearn_log', FILENAME = N'D:\DO_AN_CHUYEN_NGANH_A\sql\ST_WeLearn_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ST_WELEARN] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ST_WELEARN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ST_WELEARN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ST_WELEARN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ST_WELEARN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ST_WELEARN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ST_WELEARN] SET ARITHABORT OFF 
GO
ALTER DATABASE [ST_WELEARN] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ST_WELEARN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ST_WELEARN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ST_WELEARN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ST_WELEARN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ST_WELEARN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ST_WELEARN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ST_WELEARN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ST_WELEARN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ST_WELEARN] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ST_WELEARN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ST_WELEARN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ST_WELEARN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ST_WELEARN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ST_WELEARN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ST_WELEARN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ST_WELEARN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ST_WELEARN] SET RECOVERY FULL 
GO
ALTER DATABASE [ST_WELEARN] SET  MULTI_USER 
GO
ALTER DATABASE [ST_WELEARN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ST_WELEARN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ST_WELEARN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ST_WELEARN] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ST_WELEARN] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ST_WELEARN] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ST_WELEARN', N'ON'
GO
ALTER DATABASE [ST_WELEARN] SET QUERY_STORE = ON
GO
ALTER DATABASE [ST_WELEARN] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ST_WELEARN]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[BillID] [uniqueidentifier] NOT NULL,
	[UserID] [int] NULL,
	[Total] [money] NOT NULL,
	[Historical_cost] [money] NULL,
	[Promotion] [money] NULL,
	[Email] [varchar](70) NULL,
	[CreateAt] [datetime] NULL,
	[Payment_Method] [nvarchar](50) NOT NULL,
	[Card_HolderName] [nvarchar](50) NULL,
	[Expiration_date] [datetime] NULL,
 CONSTRAINT [PK__Bill__11F2FC4ADF4F3861] PRIMARY KEY CLUSTERED 
(
	[BillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[BillDetailID] [uniqueidentifier] NOT NULL,
	[BillID] [uniqueidentifier] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK__BillDeta__793CAF7534987058] PRIMARY KEY CLUSTERED 
(
	[BillDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoriesID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](70) NOT NULL,
	[ParentCategories] [int] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoriesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories_Course]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories_Course](
	[Cat_CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CategoriesID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Topic] [nvarchar](50) NULL,
 CONSTRAINT [PK_Categories_Course] PRIMARY KEY CLUSTERED 
(
	[Cat_CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CmtID] [int] IDENTITY(1,1) NOT NULL,
	[StaffID] [varchar](50) NULL,
	[UserID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Content_Note] [nvarchar](max) NOT NULL,
	[Date] [datetime] NULL,
	[ReplyID] [int] NULL,
 CONSTRAINT [PK_Comment_1] PRIMARY KEY CLUSTERED 
(
	[CmtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[LevelID] [int] NULL,
	[StaffID] [varchar](50) NOT NULL,
	[Title] [nvarchar](70) NOT NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Required] [nvarchar](max) NULL,
	[ImageCourseURL] [nvarchar](max) NULL,
	[Price] [money] NOT NULL,
	[DiscountPrice] [money] NULL,
	[PreviewURL] [nvarchar](max) NULL,
	[Status] [bit] NOT NULL,
	[Count] [int] NULL,
	[Rating] [float] NULL,
	[Language] [nvarchar](50) NULL,
	[TimeTotal] [int] NULL,
 CONSTRAINT [PK_Course_1] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FavList]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavList](
	[FavID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_FavList_1] PRIMARY KEY CLUSTERED 
(
	[FavID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History_Payment]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History_Payment](
	[History_PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[BillID] [uniqueidentifier] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Date] [date] NULL,
	[Status] [nchar](10) NULL,
 CONSTRAINT [PK_History_Payment] PRIMARY KEY CLUSTERED 
(
	[History_PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[LessonID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[STT] [int] NULL,
	[Title] [nvarchar](70) NOT NULL,
	[URL_video] [varchar](max) NOT NULL,
	[ImageLessonURL] [nvarchar](max) NULL,
	[Time] [int] NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[LessonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Level]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Level](
	[LevelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[LevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[SkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[StaffID] [varchar](50) NOT NULL,
	[StaffName] [nvarchar](70) NOT NULL,
	[Email] [varchar](70) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Desciption] [nvarchar](max) NULL,
	[Experience] [nvarchar](255) NULL,
	[AvatarURL] [varchar](255) NULL,
	[PhoneNumber] [char](11) NULL,
	[Role] [nvarchar](20) NOT NULL,
	[Rating] [float] NULL,
	[Address] [nvarchar](255) NULL,
	[FacebookURL] [varchar](255) NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff_Skill]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff_Skill](
	[SF_SL] [int] IDENTITY(1,1) NOT NULL,
	[StaffID] [varchar](50) NOT NULL,
	[SkillID] [int] NOT NULL,
 CONSTRAINT [PK_Staff_Skill] PRIMARY KEY CLUSTERED 
(
	[SF_SL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](15) NOT NULL,
	[TypeDescription] [nvarchar](50) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [varchar](70) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[PhoneNumber] [varchar](11) NULL,
	[Address] [nvarchar](255) NULL,
	[Avatar] [nvarchar](max) NULL,
	[CreateAt] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Course_Rating]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Course_Rating](
	[UCRID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Rating] [float] NULL,
 CONSTRAINT [PK_User_Course_Rating] PRIMARY KEY CLUSTERED 
(
	[UCRID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Lesson]    Script Date: 10/21/2023 7:37:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Lesson](
	[ULID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[LessonID] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[Time] [int] NULL,
 CONSTRAINT [PK_User_Lesson] PRIMARY KEY CLUSTERED 
(
	[ULID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Bill] ([BillID], [UserID], [Total], [Historical_cost], [Promotion], [Email], [CreateAt], [Payment_Method], [Card_HolderName], [Expiration_date]) VALUES (N'69c4115f-d162-ee11-a372-9d5bbc95139e', 1, 3918000.0000, 4127000.0000, 200000.0000, NULL, NULL, N'Internet Banking', NULL, NULL)
INSERT [dbo].[Bill] ([BillID], [UserID], [Total], [Historical_cost], [Promotion], [Email], [CreateAt], [Payment_Method], [Card_HolderName], [Expiration_date]) VALUES (N'57ca7e1b-d362-ee11-a372-9d5bbc95139e', 2, 2219000.0000, 2319000.0000, 100000.0000, NULL, NULL, N'Card', N'Mai', CAST(N'2027-09-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Bill] ([BillID], [UserID], [Total], [Historical_cost], [Promotion], [Email], [CreateAt], [Payment_Method], [Card_HolderName], [Expiration_date]) VALUES (N'74458b4b-d362-ee11-a372-9d5bbc95139e', 3, 2290000.0000, 2490000.0000, 200000.0000, NULL, NULL, N'Card', N'Jennifer', CAST(N'2025-10-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Bill] ([BillID], [UserID], [Total], [Historical_cost], [Promotion], [Email], [CreateAt], [Payment_Method], [Card_HolderName], [Expiration_date]) VALUES (N'8401f45c-d362-ee11-a372-9d5bbc95139e', 4, 1790000.0000, 1990000.0000, 200000.0000, NULL, NULL, N'Internet Banking', NULL, NULL)
GO
INSERT [dbo].[BillDetail] ([BillDetailID], [BillID], [CourseID], [Price], [Date]) VALUES (N'a68f6cac-d362-ee11-a372-9d5bbc95139e', N'69c4115f-d162-ee11-a372-9d5bbc95139e', 2, 1699000.0000, CAST(N'2023-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[BillDetail] ([BillDetailID], [BillID], [CourseID], [Price], [Date]) VALUES (N'dbbbaac4-d362-ee11-a372-9d5bbc95139e', N'69c4115f-d162-ee11-a372-9d5bbc95139e', 5, 1790000.0000, CAST(N'2023-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[BillDetail] ([BillDetailID], [BillID], [CourseID], [Price], [Date]) VALUES (N'4cc102d9-d362-ee11-a372-9d5bbc95139e', N'69c4115f-d162-ee11-a372-9d5bbc95139e', 7, 4290000.0000, CAST(N'2023-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[BillDetail] ([BillDetailID], [BillID], [CourseID], [Price], [Date]) VALUES (N'edcc2ffd-d362-ee11-a372-9d5bbc95139e', N'57ca7e1b-d362-ee11-a372-9d5bbc95139e', 8, 1790000.0000, CAST(N'2023-10-10T00:00:00.000' AS DateTime))
INSERT [dbo].[BillDetail] ([BillDetailID], [BillID], [CourseID], [Price], [Date]) VALUES (N'373d750c-d462-ee11-a372-9d5bbc95139e', N'57ca7e1b-d362-ee11-a372-9d5bbc95139e', 7, 4290000.0000, CAST(N'2023-10-10T00:00:00.000' AS DateTime))
INSERT [dbo].[BillDetail] ([BillDetailID], [BillID], [CourseID], [Price], [Date]) VALUES (N'1905b9c3-d462-ee11-a372-9d5bbc95139e', N'74458b4b-d362-ee11-a372-9d5bbc95139e', 9, 2290000.0000, CAST(N'2023-10-30T00:00:00.000' AS DateTime))
INSERT [dbo].[BillDetail] ([BillDetailID], [BillID], [CourseID], [Price], [Date]) VALUES (N'42344bdc-d462-ee11-a372-9d5bbc95139e', N'8401f45c-d362-ee11-a372-9d5bbc95139e', 5, 1790000.0000, CAST(N'2023-09-11T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (12, N'Website Development', 17)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (13, N'Data Science', 17)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (14, N'HTML/CSS', 12)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (15, N'Mobile Development', NULL)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (16, N'Flutter', 15)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (17, N'Development', NULL)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (18, N'IT & Software', NULL)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (19, N'Hardware', 18)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (20, N'React Native', 15)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (21, N'Javascript', 12)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (22, N'Design', NULL)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (23, N'Web Design', 22)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (24, N'Design Tool', 22)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (25, N'Game Design', 22)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (27, N'Figma', 23)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (28, N'Mobile App Design', 23)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (29, N'Adobe Photoshop', 23)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (30, N'User Interface Design', 23)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (31, N'Unity', 25)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (32, N'Game Development Fundamental', 25)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (33, N'Al Art Generation', 24)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (34, N'Adobe Photoshop', 24)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (35, N'Adruino', 19)
INSERT [dbo].[Categories] ([CategoriesID], [CategoryName], [ParentCategories]) VALUES (36, N'Raspberry Pi', 19)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories_Course] ON 

INSERT [dbo].[Categories_Course] ([Cat_CourseID], [CategoriesID], [CourseID], [Topic]) VALUES (1, 13, 5, NULL)
INSERT [dbo].[Categories_Course] ([Cat_CourseID], [CategoriesID], [CourseID], [Topic]) VALUES (2, 13, 2, NULL)
INSERT [dbo].[Categories_Course] ([Cat_CourseID], [CategoriesID], [CourseID], [Topic]) VALUES (3, 16, 8, NULL)
INSERT [dbo].[Categories_Course] ([Cat_CourseID], [CategoriesID], [CourseID], [Topic]) VALUES (4, 15, 8, NULL)
INSERT [dbo].[Categories_Course] ([Cat_CourseID], [CategoriesID], [CourseID], [Topic]) VALUES (5, 12, 7, NULL)
INSERT [dbo].[Categories_Course] ([Cat_CourseID], [CategoriesID], [CourseID], [Topic]) VALUES (6, 14, 7, NULL)
INSERT [dbo].[Categories_Course] ([Cat_CourseID], [CategoriesID], [CourseID], [Topic]) VALUES (7, 15, 9, NULL)
SET IDENTITY_INSERT [dbo].[Categories_Course] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([CmtID], [StaffID], [UserID], [CourseID], [Title], [Content_Note], [Date], [ReplyID]) VALUES (1, NULL, 1, 2, NULL, N'This was my first course on python and i got a lot of hands on experience and even created loads of my own projects following it. Along with the book, it proved to be a great learning source', CAST(N'2023-09-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Comment] ([CmtID], [StaffID], [UserID], [CourseID], [Title], [Content_Note], [Date], [ReplyID]) VALUES (2, N'SF01', 1, 2, NULL, N'Thank you so much', NULL, 1)
INSERT [dbo].[Comment] ([CmtID], [StaffID], [UserID], [CourseID], [Title], [Content_Note], [Date], [ReplyID]) VALUES (4, NULL, 2, 8, NULL, N'Thanks a lot ....

The information was very useful .

The course was the best .', CAST(N'2023-09-30T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([CourseID], [LevelID], [StaffID], [Title], [ShortDescription], [Description], [Required], [ImageCourseURL], [Price], [DiscountPrice], [PreviewURL], [Status], [Count], [Rating], [Language], [TimeTotal]) VALUES (2, 1, N'SF01', N'Automate the Boring Stuff with Python Programming', N'A practical programming course for office workers, academics, and administrators who want to improve their productivity.', N'If you''re an office worker, student, administrator, or just want to become more productive with your computer, programming will allow you write code that can automate tedious tasks. This course follows the popular (and free!) book, Automate the Boring Stuff with Python.

Automate the Boring Stuff with Python was written for people who want to get up to speed writing small programs that do practical tasks as soon as possible. You don''t need to know sorting algorithms or object-oriented programming, so this course skips all the computer science and concentrates on writing code that gets stuff done.

This course is for complete beginners and covers the popular Python programming language. You''ll learn basic concepts as well as:

Web scraping
Parsing PDFs and Excel spreadsheets
Automating the keyboard and mouse
Sending emails and texts
And several other practical topics
By the end of this course, you''ll be able to write code that not only dramatically increases your productivity, but also be able to list this fun and creative skill on your resume.', N'No programming experience is required.
Downloading and installing Python is covered at the start of the course.
Basic computer skills: surfing websites, running programs, saving and opening documents, etc.', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 1699000.0000, 0.0000, N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', 0, 1000, NULL, N'English', 570)
INSERT [dbo].[Course] ([CourseID], [LevelID], [StaffID], [Title], [ShortDescription], [Description], [Required], [ImageCourseURL], [Price], [DiscountPrice], [PreviewURL], [Status], [Count], [Rating], [Language], [TimeTotal]) VALUES (5, 2, N'SF02', N'Python for Data Science and Machine Learning Bootcamp', N'Learn how to use NumPy, Pandas, Seaborn , Matplotlib , Plotly , Scikit-Learn , Machine Learning, Tensorflow , and more!', N'Are you ready to start your path to becoming a Data Scientist! 
This comprehensive course will be your guide to learning how to use the power of Python to analyze data, create beautiful visualizations, and use powerful machine learning algorithms!
Data Scientist has been ranked the number one job on Glassdoor and the average salary of a data scientist is over $120,000 in the United States according to Indeed! Data Science is a rewarding career that allows you to solve some of the world''s most interesting problems!
This course is designed for both beginners with some programming experience or experienced developers looking to make the jump to Data Science!
This comprehensive course is comparable to other Data Science bootcamps that usually cost thousands of dollars, but now you can learn all that information at a fraction of the cost! With over 100 HD video lectures and detailed code notebooks for every lecture this is one of the most comprehensive course for data science and machine learning on Welearn!
We''ll teach you how to program with Python, how to create amazing data visualizations, and how to use Machine Learning with Python! Here a just a few of the topics we will be learning:
Programming with Python
NumPy with Python
Using pandas Data Frames to solve complex tasks
Use pandas to handle Excel Files
Web scraping with python
Connect Python to SQL
Use matplotlib and seaborn for data visualizations
Use plotly for interactive visualizations
Machine Learning with SciKit Learn, including:
Linear Regression
K Nearest Neighbors
K Means Clustering
Decision Trees
Random Forests
Natural Language Processing
Neural Nets and Deep Learning
Support Vector Machines
and much, much more!
Enroll in the course and become a data scientist today!
', N'Some programming experience
Admin permissions to download files
', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 1999000.0000, 1790000.0000, N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', 0, 60000, NULL, N'English', 1200)
INSERT [dbo].[Course] ([CourseID], [LevelID], [StaffID], [Title], [ShortDescription], [Description], [Required], [ImageCourseURL], [Price], [DiscountPrice], [PreviewURL], [Status], [Count], [Rating], [Language], [TimeTotal]) VALUES (7, 1, N'SF03', N'HTML/CSS cho người mới bắt đầu 2023', N'HTML/CSS, Lập trình web, Frontend', N'Bạn có đam mê về thiết kế web?

Bạn muốn tạo ra những trang web đẹp mắt, tương tác và chuyên nghiệp?

Bạn lo lắng học online có hiệu quả không?

Bạn sợ việc phải bỏ tiền ra mua một khóa học online nhưng lại nhận về một khóa học không chất lượng?

Mình hoàn toàn hiểu những lo lắng đó của các bạn. Khóa học HTML/CSS sẽ giúp bạn giải quyết được những lo lắng này. Trong khóa học này mình có video hướng dẫn phương pháp học, đảm bảo quá trình học tập của bạn sẽ là học tập thực thụ.



Giới thiệu khóa học:

Khóa học HTML/CSS của mình không chỉ là một khóa học thông thường. Đây là hành trình thú vị đưa bạn từ những khái niệm cơ bản đến việc xây dựng và tối ưu hóa các trang web ấn tượng.

Dựa trên hơn 5 năm kinh nghiệm thực tế trong lĩnh vực thiết kế web, mình đã tạo ra một khóa học mang tính ứng dụng cao, giúp bạn có thể áp dụng ngay những kiến thức bạn học vào dự án thực tế.



Học viên sẽ học được gì?

Trình bày được cấu trúc của tài liệu HTML

Sử dụng được các thẻ HTML cơ bản và nâng cao

Sử dụng được CSS để tùy chỉnh trang Web

Áp dụng được các kỹ thuật bố cục trang Web với CSS

Áp dụng được kỹ thuật responsive hiển thị hình ảnh trên nhiều loại kích thước màn hình

Sử dụng được Sass

Áp dụng được các nguyên tắc thiết yếu của UX/UI

Sử dụng được Git

Tự xây dựng các ứng dụng front-end phục vụ cho các mục đích khác nhau

Tham gia được vào vị trí Chuyên viên Thiết kế Website tại doanh nghiệp', N'Sử dụng máy tính ở mức cơ bản
Chăm chỉ, có kĩ năng tự học tốt', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 429000.0000, 0.0000, N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', 1, 109, NULL, N'Việt Nam', NULL)
INSERT [dbo].[Course] ([CourseID], [LevelID], [StaffID], [Title], [ShortDescription], [Description], [Required], [ImageCourseURL], [Price], [DiscountPrice], [PreviewURL], [Status], [Count], [Rating], [Language], [TimeTotal]) VALUES (8, 3, N'SF04', N'Flutter & Dart - The Complete Guide [2023 Edition]', N'A Complete Guide to the Flutter SDK & Flutter Framework for building native iOS and Android apps', N'May 2023: This course was completely updated (re-recorded from the ground up) and is now better than ever!

---

Discover the power of Flutter and Dart to create stunning, high-performance mobile apps for iOS and Android with the most comprehensive and bestselling Flutter course! With over 30 hours of comprehensive content, this course is the ultimate resource for anyone who wants to build beautiful, responsive, and feature-rich applications from scratch.

Learn from a Bestselling Instructor: Maximilian Schwarzmüller

I''m Maximilian Schwarzmüller, a bestselling, top-rated online course instructor with a vast variety of courses on web and mobile development. I''m excited to be your guide throughout this journey. My goal with this course is to teach you Flutter from the ground up, step-by-step and in a highly practice-oriented way!

Unlock Your Potential in Mobile App Development

This Flutter & Dart course is designed for absolute beginners with no prior programming experience, as well as for those with existing iOS, Android or other development skills. Through video lessons and hands-on projects, you''ll learn the ins and outs of Flutter and Dart!

Throughout the course you''ll build multiple demo apps - ranging from simple to more complex - and, by the end of the course, you''ll be able to build your own iOS and Android apps with Flutter.

Why Choose Flutter?

Developed by Google, Flutter is a framework that allows you to learn one language (Dart) and build beautiful native mobile apps in no time

Write code only once and ship your apps both to the Apple App Store and Google Play

Use the rich widget suite Flutter provides to add common UI elements or build your own custom widgets

What You''ll Learn:

Detailed setup instructions for both macOS and Windows

A thorough introduction to Flutter, Dart, and the concept behind widgets

An overview of the built-in widgets and how to add your own

Debugging tips & tricks

Page navigation with tabs, side drawers, and stack-based navigation

State management solutions

Handling and validating user input

Connecting your Flutter app to backend servers by sending HTTP requests

User authentication

Adding Google Maps

Using native device features like the camera

Adding beautiful animations & page transitions

Image upload

Push notifications - manual approach and automated

And much more!

Course Highlights:

On-Demand, Video-Based Learning: An immersive experience with practical examples and demo apps to help you develop your skills at your own pace

Lifetime Access: Revisit topics and refresh your knowledge anytime

Regularly Updated: The course has been updated multiple times in the past to ensure it stays up-to-date with the latest industry standards

No Experience Necessary

This course is designed to be accessible to everyone, regardless of their programming background. Whether you''re a complete beginner or an experienced developer looking to expand your skillset, I will guide you through each step of the process, ensuring you gain a deep understanding of the fundamentals and advanced concepts.

Prerequisites:

Basic programming language knowledge will help but is not required

No prior knowledge of Flutter or Dart is needed

No iOS (Swift/ObjectiveC) or Android (Java/Kotlin) development experience required

Take the First Step Towards Your Mobile App Development Career

With the skyrocketing demand for skilled mobile app developers in today''s job market, there has never been a better time to jump into the world of Flutter & Dart. By the end of this course, you''ll be fully equipped to design, develop, and deploy stunning iOS and Android apps, setting yourself up for a successful career in mobile app development.

Don''t let this opportunity pass you by! Enroll in "Flutter & Dart - The Complete Guide" today and start building amazing apps for the iOS and Android platforms!', N'Basic programming language will help but is not a must-have
You can use either Windows, macOS or Linux for Android app development - iOS apps can only be built on macOS though
NO prior iOS or Android development experience is required
NO prior Flutter or Dart experience is required - this course starts at zero!', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 1890000.0000, 1790000.0000, N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', 1, 0, NULL, N'English', NULL)
INSERT [dbo].[Course] ([CourseID], [LevelID], [StaffID], [Title], [ShortDescription], [Description], [Required], [ImageCourseURL], [Price], [DiscountPrice], [PreviewURL], [Status], [Count], [Rating], [Language], [TimeTotal]) VALUES (9, 3, N'SF05', N'iOS & Swift - The Complete iOS App Development Bootcamp', N'From Beginner to iOS App Developer with Just One Course! Fully Updated with a Comprehensive Module Dedicated to SwiftUI!', N'Welcome to the Complete iOS App Development Bootcamp. With over 39,000 5 star ratings and a 4.8 average my iOS course is the HIGHEST RATED iOS Course in the history of Udemy!

At 55+ hours, this iOS 13 course is the most comprehensive iOS development course online!

This Swift 5.1 course is based on our in-person app development bootcamp in London, where we''ve perfected the curriculum over 4 years of in-person teaching.

Our complete app development bootcamp teaches you how to code using Swift 5.1 and build beautiful iOS 13 apps for iPhone and iPad. Even if you have ZERO programming experience.

I''ll take you step-by-step through engaging and fun video tutorials and teach you everything you need to know to succeed as an iOS app developer.

The course includes 55+ hours of HD video tutorials and builds your programming knowledge while making real world apps. e.g. Pokemon Go, Whatsapp, QuizUp and Yahoo Weather.

The curriculum has been completely revamped for iOS 13 and Xcode 11. Including comprehensive modules on Apple''s latest technology - SwiftUI iOS, iPadOS and macOS app interface design, ARKit for making Augmented Reality apps as well as CoreML & CreateML for making intelligent apps with Machine Learning. You''ll be building 3D augmented reality apps that look like Pokemon Go and Harry Potter''s magical newspapers!

By the end of this course, you will be fluently programming in Swift 5.1 and be ready to make your own apps or start a freelancing job as an iOS 13 developer.

You''ll also have a portfolio of over 25 apps that you can show off to any potential employer.

Sign up today, and look forwards to:

Over 55 hours of HD 1080p video content, everything you''ll ever need to succeed as a iOS developer.

Building over 25 fully-fledged apps including ones that use machine learning and augmented reality

All the knowledge you need to start building any app you want

A giant bundle of design assets

Our best selling 12 Rules to Learn to Code eBook

$8000+ app development bootcamp course materials and curriculum

From Beginner to iOS 13 App Developer with Just One Course

We know that you''re here because you value your time. If you wanted to watch someone program for hours and hours without explaining what they''re doing, you''d be on YouTube.

By getting this course, you can be rest assured that the course is carefully thought out and edited. There are beautiful animations that explain all the difficult concepts and the videos are fully up to date with the latest versions of Swift and Xcode.

So by the end of the course, you''ll completely understand:

Concepts of Object Oriented Programming (OOP): The type system, variables, functions and methods, inheritance, structures, classes and protocols.

Control Structures: Using If/­Else clauses, Switch statements and logic to control the flow of execution.

Data Structures: How to work with collections, such as arrays and dictionaries.

Software Design: How to organise and format code for readability and how to implement the Model ­View­ Controller (MVC) design pattern, Apple''s favourite delegation pattern and the publisher pattern.

Networking: How to make asynchronous API calls, store and retrieve data from the cloud, and use the JSON format for server communication.

Persistent Local Data Storage: How to use Core Data, Realm, Codable and User Defaults to store your app data locally.

How to Implement In-App Purchases with Apple StoreKit

Machine Learning: How to make artificially intelligent apps and build your own machine learning models using iOS 13''s new CoreML2 and CreateML frameworks.

Augmented Reality: How to create 3D objects in augmented reality and create incredible 3D animations and real-life interactions using Apple''s latest ARKit2 framework.

SwiftUI: How to use Apple''s brand new UI framework to create user interfaces programmatically that look good across all Apple products.', N'No programming experience needed - I''ll teach you everything you need to know
A Mac computer running macOS 10.15 (Catalina) or a PC running macOS.
No paid software required - all apps will be created in Xcode 11 (which is free to download)
I''ll walk you through, step-by-step how to get Xcode installed and set up', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 2490000.0000, 2290000.0000, N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', 0, 345000, NULL, N'English', NULL)
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[FavList] ON 

INSERT [dbo].[FavList] ([FavID], [CourseID], [UserID]) VALUES (1, 9, NULL)
INSERT [dbo].[FavList] ([FavID], [CourseID], [UserID]) VALUES (2, 2, NULL)
INSERT [dbo].[FavList] ([FavID], [CourseID], [UserID]) VALUES (3, 5, NULL)
INSERT [dbo].[FavList] ([FavID], [CourseID], [UserID]) VALUES (4, 7, NULL)
INSERT [dbo].[FavList] ([FavID], [CourseID], [UserID]) VALUES (5, 8, NULL)
SET IDENTITY_INSERT [dbo].[FavList] OFF
GO
SET IDENTITY_INSERT [dbo].[History_Payment] ON 

INSERT [dbo].[History_Payment] ([History_PaymentID], [UserID], [BillID], [CourseID], [Date], [Status]) VALUES (1, 1, N'69c4115f-d162-ee11-a372-9d5bbc95139e', 2, CAST(N'2023-09-01' AS Date), N'Success   ')
INSERT [dbo].[History_Payment] ([History_PaymentID], [UserID], [BillID], [CourseID], [Date], [Status]) VALUES (2, 2, N'57ca7e1b-d362-ee11-a372-9d5bbc95139e', 8, CAST(N'2023-10-10' AS Date), N'Success   ')
INSERT [dbo].[History_Payment] ([History_PaymentID], [UserID], [BillID], [CourseID], [Date], [Status]) VALUES (3, 1, N'69c4115f-d162-ee11-a372-9d5bbc95139e', 5, CAST(N'2023-09-01' AS Date), N'Success   ')
INSERT [dbo].[History_Payment] ([History_PaymentID], [UserID], [BillID], [CourseID], [Date], [Status]) VALUES (4, 2, N'57ca7e1b-d362-ee11-a372-9d5bbc95139e', 7, CAST(N'2023-10-10' AS Date), N'Success   ')
INSERT [dbo].[History_Payment] ([History_PaymentID], [UserID], [BillID], [CourseID], [Date], [Status]) VALUES (5, 3, N'74458b4b-d362-ee11-a372-9d5bbc95139e', 9, CAST(N'2023-10-30' AS Date), N'Success   ')
SET IDENTITY_INSERT [dbo].[History_Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Lesson] ON 

INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (5, 2, 69, 1, N'Get Python Installed', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 5)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (6, 2, 69, 2, N'Basic Terminology and Using IDLE', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 10)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (7, 2, 69, 3, N'Writing Our First Program', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 10)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (8, 2, 68, 4, N'Python''s Built-In Functions', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 15)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (9, 5, 68, 1, N'Introduction to the Course', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 3)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (10, 5, 68, 2, N'Jupyter Overview', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 17)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (11, 5, 69, 3, N'Python for Data Analysis - NumPy', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 15)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (12, 7, 69, 1, N'Cấu trúc và Các thẻ HTML cơ bản', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 8)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (14, 8, 68, 1, N'Flutter & Dart Basics I - Getting a Solid Foundation [ROLL DICE APP]', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 18)
INSERT [dbo].[Lesson] ([LessonID], [CourseID], [TypeID], [STT], [Title], [URL_video], [ImageLessonURL], [Time]) VALUES (15, 9, 68, NULL, N'Getting Started with iOS Development and Swift 5', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/videos%2Fvecteezy_60-second-countdown-one-minute-animation-from-60-to-0_20926526_414.mp4?alt=media&token=f51a6b07-4291-4a38-b6d3-a4196fb5d7fb', N'https://firebasestorage.googleapis.com/v0/b/welearnwebsite.appspot.com/o/images%2Flogo1.png?alt=media&token=a4465cae-1cb7-4786-902a-b9c3a60a11f9', 14)
SET IDENTITY_INSERT [dbo].[Lesson] OFF
GO
SET IDENTITY_INSERT [dbo].[Level] ON 

INSERT [dbo].[Level] ([LevelID], [Name]) VALUES (1, N'Beginner')
INSERT [dbo].[Level] ([LevelID], [Name]) VALUES (2, N'Intermediate')
INSERT [dbo].[Level] ([LevelID], [Name]) VALUES (3, N'Expert')
INSERT [dbo].[Level] ([LevelID], [Name]) VALUES (4, N'All levels')
SET IDENTITY_INSERT [dbo].[Level] OFF
GO
SET IDENTITY_INSERT [dbo].[Skill] ON 

INSERT [dbo].[Skill] ([SkillID], [Title], [Description]) VALUES (1, N'Python', NULL)
INSERT [dbo].[Skill] ([SkillID], [Title], [Description]) VALUES (2, N'C++', NULL)
INSERT [dbo].[Skill] ([SkillID], [Title], [Description]) VALUES (3, N'Flutter', NULL)
INSERT [dbo].[Skill] ([SkillID], [Title], [Description]) VALUES (4, N'ASP Net Core', NULL)
INSERT [dbo].[Skill] ([SkillID], [Title], [Description]) VALUES (5, N'JavaScript', NULL)
INSERT [dbo].[Skill] ([SkillID], [Title], [Description]) VALUES (6, N'HTML/CSS', NULL)
INSERT [dbo].[Skill] ([SkillID], [Title], [Description]) VALUES (7, N'IOS/Android', NULL)
SET IDENTITY_INSERT [dbo].[Skill] OFF
GO
INSERT [dbo].[Staff] ([StaffID], [StaffName], [Email], [Password], [Desciption], [Experience], [AvatarURL], [PhoneNumber], [Role], [Rating], [Address], [FacebookURL]) VALUES (N'AD', N'Quang Minh', N'quangminh@gmail.com', N'quangminhAD', N'Quản trị viên của toàn hệ thống', NULL, NULL, NULL, N'Admin', NULL, N'Việt Nam', NULL)
INSERT [dbo].[Staff] ([StaffID], [StaffName], [Email], [Password], [Desciption], [Experience], [AvatarURL], [PhoneNumber], [Role], [Rating], [Address], [FacebookURL]) VALUES (N'SF01', N'Al Sweigart', N'sweigart@gmail.com', N'sweigart01', N'Al Sweigart is a software developer and author. He has written eight programming books, spoken at Python conferences, and has taught both kids and adults how to program. Python is his favorite programming language, and he is the developer of several open source modules for it. He is driven to make programming knowledge available to all, and his books freely available under a Creative Commons license.', N'Up to 7+ years’ relevant experience in Software development', NULL, N'09876273938', N'Instructor', 4.3, N'
United States', N'https://www.facebook.com/profile.php?id=100011073167250')
INSERT [dbo].[Staff] ([StaffID], [StaffName], [Email], [Password], [Desciption], [Experience], [AvatarURL], [PhoneNumber], [Role], [Rating], [Address], [FacebookURL]) VALUES (N'SF02', N'Jerry M.', N'jerry@gmail.com', N'jerry02', N' It covers all the most important topics in machine learning, and gives just enough theoretical knowledge to have some basic understanding of the algorithms behind the scenes. I''m already using some of the knowledge learned (and practiced!) here at work.', N'Strong knowledge of Javascript, Python, and PostgreSQL.', NULL, N'06780277388', N'Instructor', 4.6, N'France', N'https://www.facebook.com/engrjerrym')
INSERT [dbo].[Staff] ([StaffID], [StaffName], [Email], [Password], [Desciption], [Experience], [AvatarURL], [PhoneNumber], [Role], [Rating], [Address], [FacebookURL]) VALUES (N'SF03', N'Thúy Nguyễn', N'ThuyNguyen@gmail.com', N'thuy03', N'Thúy hiện đang làm tại phòng Nghiên cứu và phát triển (RnD) - Công ty CP CodeGym Việt Nam. Nhiệm vụ chính của mình là nghiên cứu và sản xuất ra những học liệu phù hợp với nhu cầu của người học. Mình mong muốn được chia sẻ những kiến thức cũng như học liệu chất lượng đến với mọi đối tượng đang muốn tìm hiểu về lĩnh vực Công nghệ thông tin.', N'Từ năm 2016-2020 mình học tại đại học Sư Phạm Hà Nội, chuyên ngành Sư phạm Tin học.

Từ 2020 đến nay, mình làm tại Công ty CP CodeGym Việt Nam với vai trò  chuyên viên nghiên cứu và phát triển (RnD).', NULL, N'09245748215', N'Instructor', NULL, N'Việt Nam', N'https://www.facebook.com/codegym.vn')
INSERT [dbo].[Staff] ([StaffID], [StaffName], [Email], [Password], [Desciption], [Experience], [AvatarURL], [PhoneNumber], [Role], [Rating], [Address], [FacebookURL]) VALUES (N'SF04', N'John M.', N'john@gmail.com', N'john04', N'I have taken a few different courses on Flutter now, but I have to say that Max has gone above and beyond. His recent updates have made it an excellent resource and the best course overall for learning Flutter. Be prepared to be challenged, but if you work through the course you will be quite knowledgable by the time you are done.', N'At least 3 Years experience in Flutter', NULL, N'07898273817', N'Instructor', 4.7, N'Australia', N'https://www.facebook.com/john.mueller.14289')
INSERT [dbo].[Staff] ([StaffID], [StaffName], [Email], [Password], [Desciption], [Experience], [AvatarURL], [PhoneNumber], [Role], [Rating], [Address], [FacebookURL]) VALUES (N'SF05', N'Robert B.', N'robert@gmail.com', N'rebert05', N'The courses from this Instructor are the best I have ever gone through. It is not just copy what I do and see it works, She explains what is going on so you gain a better understanding of how and why you code works the way it does. I have purchased ALL of the App Brewery courses and cant wait to go through them all. Great job :-)', N'Strong understanding of the Dart programming language and Flutter framework, along with experience in state management, widget customization, and responsive design.', NULL, N'09453241662', N'Instructor', 4.7, N'United States', N'https://www.facebook.com/UniversityOfMichigan')
INSERT [dbo].[Staff] ([StaffID], [StaffName], [Email], [Password], [Desciption], [Experience], [AvatarURL], [PhoneNumber], [Role], [Rating], [Address], [FacebookURL]) VALUES (N'ÐT01', N'Văn Hứa', N'vanhua@gmail.com', N'vanhuaDT', N'Đào tào của hệ thống', NULL, NULL, N'04567299183', N'Đào Tạo', NULL, N'Việt Nam', NULL)
GO
SET IDENTITY_INSERT [dbo].[Staff_Skill] ON 

INSERT [dbo].[Staff_Skill] ([SF_SL], [StaffID], [SkillID]) VALUES (1, N'SF01', 1)
INSERT [dbo].[Staff_Skill] ([SF_SL], [StaffID], [SkillID]) VALUES (2, N'SF01', 5)
INSERT [dbo].[Staff_Skill] ([SF_SL], [StaffID], [SkillID]) VALUES (3, N'SF02', 1)
INSERT [dbo].[Staff_Skill] ([SF_SL], [StaffID], [SkillID]) VALUES (4, N'SF03', 6)
INSERT [dbo].[Staff_Skill] ([SF_SL], [StaffID], [SkillID]) VALUES (5, N'SF04', 3)
INSERT [dbo].[Staff_Skill] ([SF_SL], [StaffID], [SkillID]) VALUES (6, N'SF04', 7)
INSERT [dbo].[Staff_Skill] ([SF_SL], [StaffID], [SkillID]) VALUES (7, N'SF05', 7)
SET IDENTITY_INSERT [dbo].[Staff_Skill] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([TypeID], [Title], [TypeDescription]) VALUES (68, N'Lý thuyết', NULL)
INSERT [dbo].[Type] ([TypeID], [Title], [TypeDescription]) VALUES (69, N'Thực hành', NULL)
INSERT [dbo].[Type] ([TypeID], [Title], [TypeDescription]) VALUES (73, N'Quiz', NULL)
INSERT [dbo].[Type] ([TypeID], [Title], [TypeDescription]) VALUES (74, N'Mini Test', NULL)
INSERT [dbo].[Type] ([TypeID], [Title], [TypeDescription]) VALUES (76, N'Assignment', NULL)
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserName], [Email], [Password], [PhoneNumber], [Address], [Avatar], [CreateAt]) VALUES (1, N'Hồng Anh', N'honganh@gmail.com', N'honganh123', N'09876543897', N'Quận 1, HCM', NULL, CAST(N'2023-07-09T00:00:00.000' AS DateTime))
INSERT [dbo].[User] ([UserID], [UserName], [Email], [Password], [PhoneNumber], [Address], [Avatar], [CreateAt]) VALUES (2, N'Trúc Mai', N'trucmai@gmail.com', N'trucmai222', N'05678973572', N'Ba Đình, Hà Nội', NULL, CAST(N'2023-08-10T00:00:00.000' AS DateTime))
INSERT [dbo].[User] ([UserID], [UserName], [Email], [Password], [PhoneNumber], [Address], [Avatar], [CreateAt]) VALUES (3, N'Jennifer', N'jennie@gmail.com', N'jennie333', N'07891873849', N'Birmingham', NULL, CAST(N'2023-05-20T00:00:00.000' AS DateTime))
INSERT [dbo].[User] ([UserID], [UserName], [Email], [Password], [PhoneNumber], [Address], [Avatar], [CreateAt]) VALUES (4, N'William', N'william@gmail.com', N'william444', N'09675346728', N'London, England', NULL, CAST(N'2023-01-29T00:00:00.000' AS DateTime))
INSERT [dbo].[User] ([UserID], [UserName], [Email], [Password], [PhoneNumber], [Address], [Avatar], [CreateAt]) VALUES (5, N'Quốc Thành', N'thanh@gmail.com', N'thanh555', N'09876378411', N'Đà Nẵng', NULL, CAST(N'2023-08-08T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Course_Rating] ON 

INSERT [dbo].[User_Course_Rating] ([UCRID], [UserID], [CourseID], [Rating]) VALUES (17, 1, 2, 4.6)
INSERT [dbo].[User_Course_Rating] ([UCRID], [UserID], [CourseID], [Rating]) VALUES (18, 1, 7, 4)
INSERT [dbo].[User_Course_Rating] ([UCRID], [UserID], [CourseID], [Rating]) VALUES (19, 2, 7, 5)
INSERT [dbo].[User_Course_Rating] ([UCRID], [UserID], [CourseID], [Rating]) VALUES (20, 3, 9, 3)
INSERT [dbo].[User_Course_Rating] ([UCRID], [UserID], [CourseID], [Rating]) VALUES (21, 4, 5, 4)
SET IDENTITY_INSERT [dbo].[User_Course_Rating] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Lesson] ON 

INSERT [dbo].[User_Lesson] ([ULID], [UserID], [LessonID], [Status], [Time]) VALUES (1, 1, 5, 1, 5)
INSERT [dbo].[User_Lesson] ([ULID], [UserID], [LessonID], [Status], [Time]) VALUES (2, 1, 6, 0, 2)
INSERT [dbo].[User_Lesson] ([ULID], [UserID], [LessonID], [Status], [Time]) VALUES (3, 1, 7, 0, 1)
INSERT [dbo].[User_Lesson] ([ULID], [UserID], [LessonID], [Status], [Time]) VALUES (4, 1, 8, 0, 1)
INSERT [dbo].[User_Lesson] ([ULID], [UserID], [LessonID], [Status], [Time]) VALUES (5, 2, 12, 1, 8)
INSERT [dbo].[User_Lesson] ([ULID], [UserID], [LessonID], [Status], [Time]) VALUES (6, 3, 15, 0, 12)
INSERT [dbo].[User_Lesson] ([ULID], [UserID], [LessonID], [Status], [Time]) VALUES (7, 4, 10, 1, 17)
SET IDENTITY_INSERT [dbo].[User_Lesson] OFF
GO
ALTER TABLE [dbo].[Bill] ADD  CONSTRAINT [DF__Bill__BillID__1F2E9E6D]  DEFAULT (newsequentialid()) FOR [BillID]
GO
ALTER TABLE [dbo].[BillDetail] ADD  CONSTRAINT [DF__BillDetai__BillD__25DB9BFC]  DEFAULT (newsequentialid()) FOR [BillDetailID]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_User]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Bill] FOREIGN KEY([BillID])
REFERENCES [dbo].[Bill] ([BillID])
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Bill]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Course]
GO
ALTER TABLE [dbo].[Categories_Course]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Course_Categories] FOREIGN KEY([CategoriesID])
REFERENCES [dbo].[Categories] ([CategoriesID])
GO
ALTER TABLE [dbo].[Categories_Course] CHECK CONSTRAINT [FK_Categories_Course_Categories]
GO
ALTER TABLE [dbo].[Categories_Course]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Course_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Categories_Course] CHECK CONSTRAINT [FK_Categories_Course_Course]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Course]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Staff] FOREIGN KEY([StaffID])
REFERENCES [dbo].[Staff] ([StaffID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Staff]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Level] FOREIGN KEY([LevelID])
REFERENCES [dbo].[Level] ([LevelID])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Level]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Staff] FOREIGN KEY([StaffID])
REFERENCES [dbo].[Staff] ([StaffID])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Staff]
GO
ALTER TABLE [dbo].[FavList]  WITH CHECK ADD  CONSTRAINT [FK_FavList_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[FavList] CHECK CONSTRAINT [FK_FavList_Course]
GO
ALTER TABLE [dbo].[FavList]  WITH CHECK ADD  CONSTRAINT [FK_FavList_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[FavList] CHECK CONSTRAINT [FK_FavList_User]
GO
ALTER TABLE [dbo].[History_Payment]  WITH CHECK ADD  CONSTRAINT [FK_History_Payment_Bill] FOREIGN KEY([BillID])
REFERENCES [dbo].[Bill] ([BillID])
GO
ALTER TABLE [dbo].[History_Payment] CHECK CONSTRAINT [FK_History_Payment_Bill]
GO
ALTER TABLE [dbo].[History_Payment]  WITH CHECK ADD  CONSTRAINT [FK_History_Payment_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[History_Payment] CHECK CONSTRAINT [FK_History_Payment_Course]
GO
ALTER TABLE [dbo].[History_Payment]  WITH CHECK ADD  CONSTRAINT [FK_History_Payment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[History_Payment] CHECK CONSTRAINT [FK_History_Payment_User]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_Course]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_Type] FOREIGN KEY([TypeID])
REFERENCES [dbo].[Type] ([TypeID])
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_Type]
GO
ALTER TABLE [dbo].[Staff_Skill]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Skill_Skill] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([SkillID])
GO
ALTER TABLE [dbo].[Staff_Skill] CHECK CONSTRAINT [FK_Staff_Skill_Skill]
GO
ALTER TABLE [dbo].[Staff_Skill]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Skill_Staff] FOREIGN KEY([StaffID])
REFERENCES [dbo].[Staff] ([StaffID])
GO
ALTER TABLE [dbo].[Staff_Skill] CHECK CONSTRAINT [FK_Staff_Skill_Staff]
GO
ALTER TABLE [dbo].[User_Course_Rating]  WITH CHECK ADD  CONSTRAINT [FK_User_Course_Rating_Course1] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[User_Course_Rating] CHECK CONSTRAINT [FK_User_Course_Rating_Course1]
GO
ALTER TABLE [dbo].[User_Course_Rating]  WITH CHECK ADD  CONSTRAINT [FK_User_Course_Rating_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[User_Course_Rating] CHECK CONSTRAINT [FK_User_Course_Rating_User]
GO
ALTER TABLE [dbo].[User_Lesson]  WITH CHECK ADD  CONSTRAINT [FK_User_Lesson_Lesson] FOREIGN KEY([LessonID])
REFERENCES [dbo].[Lesson] ([LessonID])
GO
ALTER TABLE [dbo].[User_Lesson] CHECK CONSTRAINT [FK_User_Lesson_Lesson]
GO
ALTER TABLE [dbo].[User_Lesson]  WITH CHECK ADD  CONSTRAINT [FK_User_Lesson_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[User_Lesson] CHECK CONSTRAINT [FK_User_Lesson_User]
GO
USE [master]
GO
ALTER DATABASE [ST_WELEARN] SET  READ_WRITE 
GO
