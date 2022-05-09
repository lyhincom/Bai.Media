-- USE [Bai.Media.Local]
USE [lyhincom_media_qa]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/9/2022 5:21:53 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Avatars]    Script Date: 5/9/2022 5:21:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avatars](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[FileExtension] [nvarchar](10) NOT NULL,
	[Width] [int] NOT NULL,
	[CreatedDt] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[ImageBytes] [varbinary](max) NOT NULL,
	[ContentType] [nvarchar](30) NOT NULL,
	[FileSizeInBytes] [bigint] NOT NULL,
	[Height] [int] NOT NULL,
	[DatabaseUrl] [nvarchar](200) NULL,
	[FileSystemUrl] [nvarchar](200) NULL,
 CONSTRAINT [PK_Avatar_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 5/9/2022 5:21:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[Id] [uniqueidentifier] NOT NULL,
	[FileExtension] [nvarchar](10) NOT NULL,
	[Width] [int] NOT NULL,
	[CreatedDt] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[ImageBytes] [varbinary](max) NOT NULL,
	[PageId] [uniqueidentifier] NOT NULL,
	[PageType] [nvarchar](100) NOT NULL,
	[ImageGroupId] [uniqueidentifier] NULL,
	[ContentType] [nvarchar](30) NOT NULL,
	[FileSizeInBytes] [bigint] NOT NULL,
	[Height] [int] NOT NULL,
	[Priority] [int] NULL,
	[DatabaseUrl] [nvarchar](200) NULL,
	[FileSystemUrl] [nvarchar](200) NULL,
 CONSTRAINT [PK_Image_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logo]    Script Date: 5/9/2022 5:21:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logo](
	[Id] [uniqueidentifier] NOT NULL,
	[FileExtension] [nvarchar](10) NOT NULL,
	[Width] [int] NOT NULL,
	[ImageBytes] [varbinary](max) NOT NULL,
	[PageId] [uniqueidentifier] NOT NULL,
	[CreatedDt] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[ContentType] [nvarchar](30) NOT NULL,
	[FileSizeInBytes] [bigint] NOT NULL,
	[Height] [int] NOT NULL,
	[DatabaseUrl] [nvarchar](200) NULL,
	[FileSystemUrl] [nvarchar](200) NULL,
 CONSTRAINT [PK_Logo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Avatar_Key]    Script Date: 5/9/2022 5:21:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Avatar_Key] ON [dbo].[Avatars]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Avatar_QueryFields]    Script Date: 5/9/2022 5:21:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Avatar_QueryFields] ON [dbo].[Avatars]
(
	[UserId] ASC,
	[FileExtension] ASC,
	[FileSizeInBytes] ASC,
	[CreatedDt] ASC,
	[Deleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Image_ImageGroupPriority]    Script Date: 5/9/2022 5:21:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Image_ImageGroupPriority] ON [dbo].[Image]
(
	[PageId] ASC,
	[PageType] ASC,
	[ImageGroupId] ASC,
	[Priority] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Image_Key]    Script Date: 5/9/2022 5:21:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Image_Key] ON [dbo].[Image]
(
	[PageId] ASC,
	[PageType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Image_QueryFields]    Script Date: 5/9/2022 5:21:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Image_QueryFields] ON [dbo].[Image]
(
	[PageId] ASC,
	[PageType] ASC,
	[FileExtension] ASC,
	[FileSizeInBytes] ASC,
	[CreatedDt] ASC,
	[Deleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Logo_Key]    Script Date: 5/9/2022 5:21:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Logo_Key] ON [dbo].[Logo]
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Logo_QueryFields]    Script Date: 5/9/2022 5:21:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Logo_QueryFields] ON [dbo].[Logo]
(
	[PageId] ASC,
	[FileExtension] ASC,
	[FileSizeInBytes] ASC,
	[CreatedDt] ASC,
	[Deleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Avatars] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Avatars] ADD  DEFAULT (getutcdate()) FOR [CreatedDt]
GO
ALTER TABLE [dbo].[Avatars] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Avatars] ADD  DEFAULT (0x) FOR [ImageBytes]
GO
ALTER TABLE [dbo].[Avatars] ADD  DEFAULT (N'') FOR [ContentType]
GO
ALTER TABLE [dbo].[Avatars] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [FileSizeInBytes]
GO
ALTER TABLE [dbo].[Avatars] ADD  DEFAULT ((0)) FOR [Height]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT (getutcdate()) FOR [CreatedDt]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT (0x) FOR [ImageBytes]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT (N'') FOR [PageType]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT (N'') FOR [ContentType]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [FileSizeInBytes]
GO
ALTER TABLE [dbo].[Image] ADD  DEFAULT ((0)) FOR [Height]
GO
ALTER TABLE [dbo].[Logo] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Logo] ADD  DEFAULT (0x) FOR [ImageBytes]
GO
ALTER TABLE [dbo].[Logo] ADD  DEFAULT (getutcdate()) FOR [CreatedDt]
GO
ALTER TABLE [dbo].[Logo] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Deleted]
GO
ALTER TABLE [dbo].[Logo] ADD  DEFAULT (N'') FOR [ContentType]
GO
ALTER TABLE [dbo].[Logo] ADD  DEFAULT (CONVERT([bigint],(0))) FOR [FileSizeInBytes]
GO
ALTER TABLE [dbo].[Logo] ADD  DEFAULT ((0)) FOR [Height]
GO
