# What's this?

# Database setup

1. Edit the default ConnectionString in `web.config`
2. Create a database called `ssa_dropbox`
3. Open the project in Visual Studio and run `update-database` in the NuGet console. You might have to take some extra steps, not sure.
4. Execute the following scripts on the database:

```
USE [ssa_dropbox]
GO

/****** Object:  Table [dbo].[FileDownload]    Script Date: 09/12/2014 18:57:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FileDownload](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DownloadTime] [datetime] NOT NULL,
	[DownloadBy] [nvarchar](max) NOT NULL,
	[FileId] [int] NOT NULL,
 CONSTRAINT [PK_FileDownload] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



USE [ssa_dropbox]
GO

/****** Object:  Table [dbo].[FileRegistration]    Script Date: 09/12/2014 18:51:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FileRegistration](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[FileName] [nvarchar](max) NULL,
	[UploadTime] [datetime] NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_FileRegistration] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


USE [ssa_dropbox]
GO

/****** Object:  Table [dbo].[FileUser]    Script Date: 09/12/2014 18:50:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FileUser](
	[FileId] [int] NULL,
	[UserName] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
```

