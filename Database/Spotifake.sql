USE [master]
GO
/****** Object:  Database [Spotifake]    Script Date: 29/06/2020 06:14:12 p. m. ******/
CREATE DATABASE [Spotifake]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Spotifake', FILENAME = N'E:\Spotifake\DATA\Spotifake.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Spotifake_log', FILENAME = N'E:\Spotifake\LOG\Spotifake_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Spotifake] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Spotifake].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Spotifake] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Spotifake] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Spotifake] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Spotifake] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Spotifake] SET ARITHABORT OFF 
GO
ALTER DATABASE [Spotifake] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Spotifake] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Spotifake] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Spotifake] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Spotifake] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Spotifake] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Spotifake] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Spotifake] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Spotifake] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Spotifake] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Spotifake] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Spotifake] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Spotifake] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Spotifake] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Spotifake] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Spotifake] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Spotifake] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Spotifake] SET RECOVERY FULL 
GO
ALTER DATABASE [Spotifake] SET  MULTI_USER 
GO
ALTER DATABASE [Spotifake] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Spotifake] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Spotifake] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Spotifake] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Spotifake] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Spotifake', N'ON'
GO
ALTER DATABASE [Spotifake] SET QUERY_STORE = OFF
GO
USE [Spotifake]
GO
/****** Object:  User [userSpotifake]    Script Date: 29/06/2020 06:14:13 p. m. ******/
CREATE USER [userSpotifake] FOR LOGIN [userSpotifake] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [userSpotifake]
GO
/****** Object:  Table [dbo].[Album]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[IdAlbum] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[type] [bit] NOT NULL,
	[releaseDate] [date] NOT NULL,
	[coverPath] [nvarchar](max) NOT NULL,
	[IdContentCreator] [int] NOT NULL,
	[IdInterpreter] [int] NOT NULL,
	[IdGenre] [int] NOT NULL,
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[IdAlbum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consumer]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consumer](
	[IdConsumer] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[lastname] [nvarchar](100) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[imageStoragePath] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Consumer] PRIMARY KEY CLUSTERED 
(
	[IdConsumer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContentCreator]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContentCreator](
	[IdContentCreator] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[lastname] [nvarchar](100) NOT NULL,
	[stageName] [nvarchar](100) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[imageStoragePath] [nvarchar](max) NOT NULL,
	[IdGenre] [int] NOT NULL,
 CONSTRAINT [PK_ContentCreator] PRIMARY KEY CLUSTERED 
(
	[IdContentCreator] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[IdGenre] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[IdGenre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interpreter]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interpreter](
	[IdInterpreter] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Interpreter] PRIMARY KEY CLUSTERED 
(
	[IdInterpreter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocalTrack]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalTrack](
	[IdLocalTrack] [int] IDENTITY(1,1) NOT NULL,
	[IdConsumer] [int] NOT NULL,
	[fileName] [nvarchar](100) NOT NULL,
	[fileType] [nvarchar](50) NOT NULL,
	[gender] [nvarchar](25) NOT NULL,
	[localPath] [nvarchar](max) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[trackNumber] [int] NOT NULL,
 CONSTRAINT [PK_LocalTrack] PRIMARY KEY CLUSTERED 
(
	[IdLocalTrack] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Playlist]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Playlist](
	[IdPlaylist] [int] IDENTITY(1,1) NOT NULL,
	[creationDate] [date] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[IdConsumer] [int] NOT NULL,
	[IdContentCreator] [int] NOT NULL,
 CONSTRAINT [PK_Playlist] PRIMARY KEY CLUSTERED 
(
	[IdPlaylist] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Track]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Track](
	[IdTrack] [int] NOT NULL,
	[durationSeconds] [float] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[trackNumber] [int] NOT NULL,
	[storagePath] [nvarchar](max) NOT NULL,
	[IdGenre] [int] NOT NULL,
	[IdInterpreter] [int] NOT NULL,
	[IdAlbum] [int] NOT NULL,
 CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED 
(
	[IdTrack] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrackPlaylist]    Script Date: 29/06/2020 06:14:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackPlaylist](
	[IdTrackPlaylist] [int] IDENTITY(1,1) NOT NULL,
	[IdTrack] [int] NOT NULL,
	[IdPlaylist] [int] NOT NULL,
 CONSTRAINT [PK_TrackPlaylist] PRIMARY KEY CLUSTERED 
(
	[IdTrackPlaylist] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_ContentCreator] FOREIGN KEY([IdContentCreator])
REFERENCES [dbo].[ContentCreator] ([IdContentCreator])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_ContentCreator]
GO
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_Genre] FOREIGN KEY([IdGenre])
REFERENCES [dbo].[Genre] ([IdGenre])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_Genre]
GO
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_Interpreter] FOREIGN KEY([IdInterpreter])
REFERENCES [dbo].[Interpreter] ([IdInterpreter])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_Interpreter]
GO
ALTER TABLE [dbo].[ContentCreator]  WITH CHECK ADD  CONSTRAINT [FK_ContentCreator_Genre] FOREIGN KEY([IdGenre])
REFERENCES [dbo].[Genre] ([IdGenre])
GO
ALTER TABLE [dbo].[ContentCreator] CHECK CONSTRAINT [FK_ContentCreator_Genre]
GO
ALTER TABLE [dbo].[LocalTrack]  WITH CHECK ADD  CONSTRAINT [FK_LocalTrack_Consumer] FOREIGN KEY([IdConsumer])
REFERENCES [dbo].[Consumer] ([IdConsumer])
GO
ALTER TABLE [dbo].[LocalTrack] CHECK CONSTRAINT [FK_LocalTrack_Consumer]
GO
ALTER TABLE [dbo].[Playlist]  WITH CHECK ADD  CONSTRAINT [FK_Playlist_Consumer] FOREIGN KEY([IdConsumer])
REFERENCES [dbo].[Consumer] ([IdConsumer])
GO
ALTER TABLE [dbo].[Playlist] CHECK CONSTRAINT [FK_Playlist_Consumer]
GO
ALTER TABLE [dbo].[Playlist]  WITH CHECK ADD  CONSTRAINT [FK_Playlist_ContentCreator] FOREIGN KEY([IdContentCreator])
REFERENCES [dbo].[ContentCreator] ([IdContentCreator])
GO
ALTER TABLE [dbo].[Playlist] CHECK CONSTRAINT [FK_Playlist_ContentCreator]
GO
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_Album] FOREIGN KEY([IdAlbum])
REFERENCES [dbo].[Album] ([IdAlbum])
GO
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_Album]
GO
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_Genre] FOREIGN KEY([IdGenre])
REFERENCES [dbo].[Genre] ([IdGenre])
GO
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_Genre]
GO
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_Interpreter] FOREIGN KEY([IdInterpreter])
REFERENCES [dbo].[Interpreter] ([IdInterpreter])
GO
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_Interpreter]
GO
ALTER TABLE [dbo].[TrackPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_TrackPlaylist_Playlist] FOREIGN KEY([IdPlaylist])
REFERENCES [dbo].[Playlist] ([IdPlaylist])
GO
ALTER TABLE [dbo].[TrackPlaylist] CHECK CONSTRAINT [FK_TrackPlaylist_Playlist]
GO
ALTER TABLE [dbo].[TrackPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_TrackPlaylist_Track] FOREIGN KEY([IdTrack])
REFERENCES [dbo].[Track] ([IdTrack])
GO
ALTER TABLE [dbo].[TrackPlaylist] CHECK CONSTRAINT [FK_TrackPlaylist_Track]
GO
USE [master]
GO
ALTER DATABASE [Spotifake] SET  READ_WRITE 
GO
