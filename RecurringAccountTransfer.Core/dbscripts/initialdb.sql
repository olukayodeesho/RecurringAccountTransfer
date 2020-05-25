USE [RecurringAccountTransfers]
GO
/****** Object:  Table [dbo].[RecurringSetup]    Script Date: 05/25/2020 15:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecurringSetup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SourceAccountNumber] [varchar](10) NOT NULL,
	[SourceBankCode] [varchar](8) NOT NULL,
	[RecurringAlias] [nchar](10) NOT NULL,
	[DestinationBankCode] [varchar](8) NOT NULL,
	[DestinationAccountNumber] [varchar](10) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[RecurringFrequency] [varchar](20) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
	[Purpose] [varchar](50) NULL,
	[ProfileId] [bigint] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Narration] [varchar](50) NULL,
 CONSTRAINT [PK_RecurringSetup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecurringFrequency]    Script Date: 05/25/2020 15:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecurringFrequency](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_RecurringFrequency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequestResponseLog]    Script Date: 05/25/2020 15:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequestResponseLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestBody] [varchar](5000) NULL,
	[RequestUrl] [varchar](1000) NULL,
	[HttpMethodType] [varchar](10) NULL,
	[RequestTime] [datetime] NULL,
	[ResponseBody] [varchar](5000) NULL,
	[ResponseTime] [datetime] NULL,
	[ResponseHttpCode] [varchar](10) NULL,
 CONSTRAINT [PK_RequestResponseLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecurringSetupAttemptLog]    Script Date: 05/25/2020 15:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecurringSetupAttemptLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RecurringSetupId] [bigint] NOT NULL,
	[IsSuccessful] [bit] NOT NULL,
	[DateAttempted] [datetime] NOT NULL,
	[DebitServiceResponse] [varchar](5000) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_RecurringSetupAttemptLog_RecurringSetup]    Script Date: 05/25/2020 15:36:54 ******/
ALTER TABLE [dbo].[RecurringSetupAttemptLog]  WITH CHECK ADD  CONSTRAINT [FK_RecurringSetupAttemptLog_RecurringSetup] FOREIGN KEY([RecurringSetupId])
REFERENCES [dbo].[RecurringSetup] ([Id])
GO
ALTER TABLE [dbo].[RecurringSetupAttemptLog] CHECK CONSTRAINT [FK_RecurringSetupAttemptLog_RecurringSetup]
GO
