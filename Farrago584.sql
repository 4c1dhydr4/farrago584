USE [master]
GO
/****** Object:  Database [Farrago584]    Script Date: 30/06/2018 20:24:38 ******/
CREATE DATABASE [Farrago584]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Farrago584', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.LOCAL\MSSQL\DATA\Farrago584.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Farrago584_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.LOCAL\MSSQL\DATA\Farrago584_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Farrago584] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Farrago584].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Farrago584] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Farrago584] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Farrago584] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Farrago584] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Farrago584] SET ARITHABORT OFF 
GO
ALTER DATABASE [Farrago584] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Farrago584] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Farrago584] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Farrago584] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Farrago584] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Farrago584] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Farrago584] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Farrago584] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Farrago584] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Farrago584] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Farrago584] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Farrago584] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Farrago584] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Farrago584] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Farrago584] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Farrago584] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Farrago584] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Farrago584] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Farrago584] SET  MULTI_USER 
GO
ALTER DATABASE [Farrago584] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Farrago584] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Farrago584] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Farrago584] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Farrago584] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Farrago584]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 30/06/2018 20:24:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DNI] [char](8) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[CellphoneNumber] [varchar](9) NOT NULL,
	[isSuperuser] [char](1) NOT NULL,
	[isActive] [char](1) NOT NULL,
 CONSTRAINT [PK_Administrador] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocalRequirements]    Script Date: 30/06/2018 20:24:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalRequirements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[LocalReservation] [int] NOT NULL,
	[Requirement] [int] NOT NULL,
 CONSTRAINT [PK_LocalRequirements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalReservation]    Script Date: 30/06/2018 20:24:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LocalReservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[UserId] [int] NOT NULL,
	[PersonsNumber] [int] NOT NULL,
	[Confirmation] [char](1) NOT NULL,
	[AdminId] [int] NULL,
	[UserObservation] [varchar](50) NULL,
	[AdminObservation] [varchar](50) NULL,
	[FinalPrice] [float] NOT NULL,
	[Status] [char](1) NOT NULL,
 CONSTRAINT [PK_ReservaLocal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Requirements]    Script Date: 30/06/2018 20:24:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Requirements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReqName] [varchar](30) NOT NULL,
	[Description] [varchar](200) NULL,
	[Cost] [float] NOT NULL,
 CONSTRAINT [PK_Requerimientos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TableReservation]    Script Date: 30/06/2018 20:24:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TableReservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[PersonsNumber] [int] NOT NULL,
	[Confirmed] [char](1) NOT NULL,
	[AdminId] [int] NULL,
	[UserObservations] [varchar](300) NULL,
	[AdminObservations] [varchar](300) NULL,
	[Status] [char](1) NOT NULL,
 CONSTRAINT [PK_ReservaMesa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 30/06/2018 20:24:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Names] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DNI] [char](8) NOT NULL,
	[BornDate] [date] NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[CellphoneNumber] [char](9) NOT NULL,
	[ReservationsNumber] [smallint] NOT NULL,
	[AssistedNumber] [smallint] NOT NULL,
	[FaultsNumber] [smallint] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([id], [Username], [Password], [Name], [FirstName], [LastName], [DNI], [Email], [CellphoneNumber], [isSuperuser], [isActive]) VALUES (1, N'admin', N'admin', N'Jorge', N'Torres', N'Aguirre', N'19912025', N'emilio@fgarrago.com', N'976979383', N'Y', N'Y')
SET IDENTITY_INSERT [dbo].[Admin] OFF
SET IDENTITY_INSERT [dbo].[LocalRequirements] ON 

INSERT [dbo].[LocalRequirements] ([id], [LocalReservation], [Requirement]) VALUES (79, 32, 1)
INSERT [dbo].[LocalRequirements] ([id], [LocalReservation], [Requirement]) VALUES (80, 32, 2)
INSERT [dbo].[LocalRequirements] ([id], [LocalReservation], [Requirement]) VALUES (81, 32, 3)
INSERT [dbo].[LocalRequirements] ([id], [LocalReservation], [Requirement]) VALUES (82, 32, 4)
SET IDENTITY_INSERT [dbo].[LocalRequirements] OFF
SET IDENTITY_INSERT [dbo].[LocalReservation] ON 

INSERT [dbo].[LocalReservation] ([id], [Date], [UserId], [PersonsNumber], [Confirmation], [AdminId], [UserObservation], [AdminObservation], [FinalPrice], [Status]) VALUES (32, CAST(N'2018-10-30' AS Date), 1, 200, N'N', 1, NULL, N'No venga más', 2350, N'F')
SET IDENTITY_INSERT [dbo].[LocalReservation] OFF
SET IDENTITY_INSERT [dbo].[Requirements] ON 

INSERT [dbo].[Requirements] ([id], [ReqName], [Description], [Cost]) VALUES (1, N'Luces', N'Luces estrobostópicas para que tu fiesta sea la más envidiada de la ciudad.', 200)
INSERT [dbo].[Requirements] ([id], [ReqName], [Description], [Cost]) VALUES (2, N'Mozos', N'Excepcional equipo de mozos.', 250)
INSERT [dbo].[Requirements] ([id], [ReqName], [Description], [Cost]) VALUES (3, N'Vagilla', N'Fina vagilla de porcelana, adecuada para los eventos más exclusivos', 100)
INSERT [dbo].[Requirements] ([id], [ReqName], [Description], [Cost]) VALUES (4, N'Cena', N'Escoge el platillo adecuado para tus invitados.', 1800)
SET IDENTITY_INSERT [dbo].[Requirements] OFF
SET IDENTITY_INSERT [dbo].[TableReservation] ON 

INSERT [dbo].[TableReservation] ([id], [DateTime], [UserId], [PersonsNumber], [Confirmed], [AdminId], [UserObservations], [AdminObservations], [Status]) VALUES (23, CAST(N'2018-06-27 13:50:00.000' AS DateTime), 1, 2, N'N', 1, N'Silla para discapacitados', N'Gracias por venir', N'A')
SET IDENTITY_INSERT [dbo].[TableReservation] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [Username], [Password], [Names], [FirstName], [LastName], [DNI], [BornDate], [Email], [CellphoneNumber], [ReservationsNumber], [AssistedNumber], [FaultsNumber]) VALUES (1, N'luisquiroz', N'admin', N'Luis', N'Quiorz', N'Burga', N'70197949', CAST(N'1993-04-04' AS Date), N'quirozburga@gmail.com', N'980655030', 2, 1, 1)
INSERT [dbo].[User] ([id], [Username], [Password], [Names], [FirstName], [LastName], [DNI], [BornDate], [Email], [CellphoneNumber], [ReservationsNumber], [AssistedNumber], [FaultsNumber]) VALUES (2, N'eduardo', N'admin', N'Eduardo', N'Collantes', N'Bazan', N'11111111', CAST(N'1994-10-10' AS Date), N'colla@gmail.com', N'999999999', 0, 0, 0)
INSERT [dbo].[User] ([id], [Username], [Password], [Names], [FirstName], [LastName], [DNI], [BornDate], [Email], [CellphoneNumber], [ReservationsNumber], [AssistedNumber], [FaultsNumber]) VALUES (3, N'augustoquiroz', N'admin', N'Augusto', N'Quiroz', N'Burga', N'12345678', CAST(N'2018-06-27' AS Date), N'augusto@gmail.com', N'976880089', 0, 0, 0)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[LocalRequirements]  WITH CHECK ADD  CONSTRAINT [FK_LocalRequirements_LocalReservation] FOREIGN KEY([LocalReservation])
REFERENCES [dbo].[LocalReservation] ([id])
GO
ALTER TABLE [dbo].[LocalRequirements] CHECK CONSTRAINT [FK_LocalRequirements_LocalReservation]
GO
ALTER TABLE [dbo].[LocalRequirements]  WITH CHECK ADD  CONSTRAINT [FK_LocalRequirements_Requirements] FOREIGN KEY([Requirement])
REFERENCES [dbo].[Requirements] ([id])
GO
ALTER TABLE [dbo].[LocalRequirements] CHECK CONSTRAINT [FK_LocalRequirements_Requirements]
GO
ALTER TABLE [dbo].[LocalReservation]  WITH CHECK ADD  CONSTRAINT [FK_LocalReservation_Admin] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[LocalReservation] CHECK CONSTRAINT [FK_LocalReservation_Admin]
GO
ALTER TABLE [dbo].[LocalReservation]  WITH CHECK ADD  CONSTRAINT [FK_LocalReservation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[LocalReservation] CHECK CONSTRAINT [FK_LocalReservation_User]
GO
ALTER TABLE [dbo].[TableReservation]  WITH CHECK ADD  CONSTRAINT [FK_TableReservation_Admin] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[TableReservation] CHECK CONSTRAINT [FK_TableReservation_Admin]
GO
ALTER TABLE [dbo].[TableReservation]  WITH CHECK ADD  CONSTRAINT [FK_TableReservation_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[TableReservation] CHECK CONSTRAINT [FK_TableReservation_User]
GO
USE [master]
GO
ALTER DATABASE [Farrago584] SET  READ_WRITE 
GO
