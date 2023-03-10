USE [master]
GO
/****** Object:  Database [tickets]    Script Date: 16/02/2023 1:15:12 p. m. ******/
CREATE DATABASE [tickets]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tickets', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\tickets.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tickets_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\tickets_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [tickets] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tickets].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tickets] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tickets] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tickets] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tickets] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tickets] SET ARITHABORT OFF 
GO
ALTER DATABASE [tickets] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [tickets] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tickets] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tickets] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tickets] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tickets] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tickets] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tickets] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tickets] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tickets] SET  DISABLE_BROKER 
GO
ALTER DATABASE [tickets] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tickets] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tickets] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tickets] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tickets] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tickets] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tickets] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tickets] SET RECOVERY FULL 
GO
ALTER DATABASE [tickets] SET  MULTI_USER 
GO
ALTER DATABASE [tickets] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tickets] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tickets] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tickets] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tickets] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tickets] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'tickets', N'ON'
GO
ALTER DATABASE [tickets] SET QUERY_STORE = ON
GO
ALTER DATABASE [tickets] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [tickets]
GO
/****** Object:  Table [dbo].[ticket]    Script Date: 16/02/2023 1:15:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticket](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nchar](50) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[status] [nchar](10) NULL,
 CONSTRAINT [PK_ticket] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ticket] ON 

INSERT [dbo].[ticket] ([id], [user_name], [created_at], [updated_at], [status]) VALUES (1, NULL, NULL, NULL, N'cerrado   ')
INSERT [dbo].[ticket] ([id], [user_name], [created_at], [updated_at], [status]) VALUES (2, N'Marian', CAST(N'2023-02-16T18:09:37.253' AS DateTime), CAST(N'2023-02-16T18:09:37.253' AS DateTime), N'cerrado   ')
SET IDENTITY_INSERT [dbo].[ticket] OFF
GO
USE [master]
GO
ALTER DATABASE [tickets] SET  READ_WRITE 
GO
