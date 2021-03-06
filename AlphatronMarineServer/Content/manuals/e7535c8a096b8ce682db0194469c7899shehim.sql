CREATE DATABASE [SheHim]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SheHim', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SheHim.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SheHim_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SheHim_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SheHim] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SheHim].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SheHim] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SheHim] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SheHim] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SheHim] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SheHim] SET ARITHABORT OFF 
GO
ALTER DATABASE [SheHim] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SheHim] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SheHim] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SheHim] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SheHim] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SheHim] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SheHim] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SheHim] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SheHim] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SheHim] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SheHim] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SheHim] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SheHim] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SheHim] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SheHim] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SheHim] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SheHim] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SheHim] SET RECOVERY FULL 
GO
ALTER DATABASE [SheHim] SET  MULTI_USER 
GO
ALTER DATABASE [SheHim] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SheHim] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SheHim] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SheHim] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SheHim] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SheHim', N'ON'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Appointment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[WorkerID] [int] NULL,
	[ClientID] [int] NULL,
	[Date] [date] NULL,
	[Time] [time](4) NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Auth](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Token] [varchar](max) NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Client](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[SecondName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [varchar](max) NULL,
	[Age] [int] NULL,
	[Gender] [int] NULL,
	[CardID] [int] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ClientCard](
	[CardNumber] [int] IDENTITY(10000000,1) NOT NULL,
	[DiscountAmount] [int] NULL,
	[GivenDate] [date] NULL,
 CONSTRAINT [PK_ClientCard] PRIMARY KEY CLUSTERED 
(
	[CardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Event](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Picture] [nvarchar](max) NULL,
	[Text] [text] NULL,
	[Date] [date] NULL,
	[Time] [time](4) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Service](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Price] [int] NULL,
	[Part] [int] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](max) NULL,
	[Password] [varchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[SecondName] [nvarchar](max) NULL,
	[Phone] [varchar](max) NULL,
	[Salary] [int] NULL,
	[SalaryDate] [date] NULL,
	[Role] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Client] FOREIGN KEY([ClientID])
REFERENCES [Client] ([ID])
ON UPDATE SET NULL
ON DELETE SET NULL
GO
ALTER TABLE [Appointment] CHECK CONSTRAINT [FK_Appointment_Client]
GO
ALTER TABLE [Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Service] FOREIGN KEY([ServiceID])
REFERENCES [Service] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Appointment] CHECK CONSTRAINT [FK_Appointment_Service]
GO
ALTER TABLE [Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_User] FOREIGN KEY([WorkerID])
REFERENCES [User] ([ID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [Appointment] CHECK CONSTRAINT [FK_Appointment_User]
GO
ALTER TABLE [Auth]  WITH CHECK ADD  CONSTRAINT [FK_Auth_User] FOREIGN KEY([UserID])
REFERENCES [User] ([ID])
GO
ALTER TABLE [Auth] CHECK CONSTRAINT [FK_Auth_User]
GO
ALTER TABLE [Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_ClientCard] FOREIGN KEY([CardID])
REFERENCES [ClientCard] ([CardNumber])
GO
ALTER TABLE [Client] CHECK CONSTRAINT [FK_Client_ClientCard]
GO
ALTER DATABASE [SheHim] SET  READ_WRITE 
GO
