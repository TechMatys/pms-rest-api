USE [PMS]
GO
/****** Object:  Table [dbo].[CompanyDocuments]    Script Date: 28-02-2022 10.33.02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyDocuments](
	[CompanyDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[TableId] [int] NULL,
	[FilesNames] [varchar](100) NULL,
	[DocumentType] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CompanyDocuments] PRIMARY KEY CLUSTERED 
(
	[CompanyDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyExpenses]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyExpenses](
	[CompanyExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Amount] [float] NULL,
	[PaymentMonth] [int] NULL,
	[PaymentYear] [int] NULL,
	[ExpenseDate] [date] NULL,
	[Notes] [varchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CompanyExpenses] PRIMARY KEY CLUSTERED 
(
	[CompanyExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyInvoices]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyInvoices](
	[CompanyInvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[GeneratedDate] [date] NULL,
	[InvoiceFileName] [varchar](500) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CompanyInvoices] PRIMARY KEY CLUSTERED 
(
	[CompanyInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePayments]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePayments](
	[EmployeePaymentId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Amount] [float] NULL,
	[PaymentMonth] [int] NULL,
	[PaymentYear] [int] NULL,
	[PaymentDate] [date] NULL,
	[Notes] [varchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeePayments] PRIMARY KEY CLUSTERED 
(
	[EmployeePaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeProjects]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeProjects](
	[EmployeeProjectId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[AssignedDate] [date] NULL,
	[Notes] [varchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeProjects] PRIMARY KEY CLUSTERED 
(
	[EmployeeProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Gender] [int] NULL,
	[DateOfBirth] [date] NULL,
	[DesignationId] [int] NULL,
	[EmailAddress] [varchar](50) NULL,
	[Mobile] [varchar](10) NULL,
	[Address] [varchar](100) NULL,
	[City] [varchar](20) NULL,
	[StateId] [int] NULL,
	[PostalCode] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalCodes]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalCodes](
	[GlobalCodeId] [int] IDENTITY(1,1) NOT NULL,
	[CodeName] [varchar](100) NULL,
	[Category] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_GlobalCodes] PRIMARY KEY CLUSTERED 
(
	[GlobalCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectPayments]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPayments](
	[ProjectPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[RecievedAmount] [float] NULL,
	[BalancedAmount] [float] NULL,
	[PaymentMonth] [int] NULL,
	[PaymentYear] [int] NULL,
	[PaymentDate] [date] NULL,
	[Notes] [varchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ProjectPayments] PRIMARY KEY CLUSTERED 
(
	[ProjectPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[OwnerName] [varchar](100) NULL,
	[Description] [varchar](200) NULL,
	[Technologies] [varchar](1000) NOT NULL,
	[DurationId] [int] NULL,
	[StatusId] [int] NOT NULL,
	[StartDate] [date] NULL,
	[CompletionDate] [date] NULL,
	[BudgetAmount] [float] NULL,
	[Notes] [varchar](200) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[States]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 28-02-2022 10.33.03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[ScreenPermissionId] [varchar](1000) NOT NULL,
	[StatusId] [int] NOT NULL,
	[Password] [varchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (3, N'Subhash', NULL, NULL, NULL, NULL, -1, NULL, NULL, NULL, NULL, NULL, 121, NULL, NULL, -1, CAST(N'2022-02-25T00:00:00.000' AS DateTime), -1, CAST(N'2022-02-26T11:25:22.160' AS DateTime), 1, -1, CAST(N'2022-02-27T03:39:52.867' AS DateTime))
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (4, N'Deepak', NULL, N'', NULL, NULL, NULL, N'deepak@techmatys.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, -1, CAST(N'2022-02-26T10:42:50.187' AS DateTime), -1, CAST(N'2022-02-26T11:22:34.380' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (5, N'Amit', NULL, NULL, NULL, NULL, NULL, N'amit@techmatys.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, -1, CAST(N'2022-02-26T10:46:37.063' AS DateTime), -1, CAST(N'2022-02-26T11:22:34.380' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (6, N'Malika', NULL, NULL, NULL, NULL, NULL, N'malika@techmatys.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, -1, CAST(N'2022-02-26T10:47:03.010' AS DateTime), -1, CAST(N'2022-02-26T11:22:34.380' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (7, N'Prakash', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -1, CAST(N'2022-02-26T10:56:09.393' AS DateTime), -1, CAST(N'2022-02-26T11:22:34.380' AS DateTime), 1, -1, CAST(N'2022-02-26T11:11:17.687' AS DateTime))
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (8, N'Subhash', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -1, CAST(N'2022-02-26T11:17:05.610' AS DateTime), -1, CAST(N'2022-02-26T11:22:34.380' AS DateTime), 1, -1, CAST(N'2022-02-26T11:17:13.463' AS DateTime))
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (9, N'Subhash', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -1, CAST(N'2022-02-26T11:18:46.930' AS DateTime), -1, CAST(N'2022-02-26T11:22:34.380' AS DateTime), 1, -1, CAST(N'2022-02-26T11:21:00.217' AS DateTime))
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [MiddleName], [LastName], [Gender], [DateOfBirth], [DesignationId], [EmailAddress], [Mobile], [Address], [City], [StateId], [PostalCode], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (10, N'Subhash', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, -1, CAST(N'2022-02-26T11:20:48.963' AS DateTime), -1, CAST(N'2022-02-26T11:22:34.380' AS DateTime), 1, -1, CAST(N'2022-02-26T11:20:53.310' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[GlobalCodes] ON 
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'Administrator', N'UserRole', 1, -1, CAST(N'2022-02-22T00:00:00.000' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'Staff', N'UserRole', 1, -1, CAST(N'2022-02-22T00:00:00.000' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (3, N'Employee', N'UserRole', 1, -1, CAST(N'2022-02-22T00:00:00.000' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (4, N'Active', N'EmployeeStatus', 1, -1, CAST(N'2022-02-22T00:00:00.000' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (5, N'InActive', N'EmployeeStatus', 1, -1, CAST(N'2022-02-22T00:00:00.000' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (6, N'Suspended', N'EmployeeStatus', 1, -1, CAST(N'2022-02-22T00:00:00.000' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (7, N'Developer', N'Designation', 1, -1, CAST(N'2022-02-26T10:20:37.573' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (8, N'Designer', N'Designation', 1, -1, CAST(N'2022-02-26T10:20:37.573' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (9, N'Business Developer', N'Designation', 1, -1, CAST(N'2022-02-26T10:20:37.573' AS DateTime), 0, NULL, NULL)
GO
INSERT [dbo].[GlobalCodes] ([GlobalCodeId], [CodeName], [Category], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (10, N'Manager', N'Designation', 1, -1, CAST(N'2022-02-26T10:20:37.573' AS DateTime), 0, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[GlobalCodes] OFF
GO
ALTER TABLE [dbo].[CompanyDocuments] ADD  CONSTRAINT [DF_CompanyDocuments_ModifiedBy]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[CompanyDocuments] ADD  CONSTRAINT [DF_CompanyDocuments_ModifiedDate]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[CompanyDocuments] ADD  CONSTRAINT [DF_CompanyDocuments_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CompanyExpenses] ADD  CONSTRAINT [DF_CompanyExpenses_ModifiedBy]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[CompanyExpenses] ADD  CONSTRAINT [DF_CompanyExpenses_ModifiedDate]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[CompanyExpenses] ADD  CONSTRAINT [DF_CompanyExpenses_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CompanyInvoices] ADD  CONSTRAINT [DF_CompanyInvoices_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[EmployeePayments] ADD  CONSTRAINT [DF_EmployeePayments_ModifiedBy]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[EmployeePayments] ADD  CONSTRAINT [DF_EmployeePayments_ModifiedDate]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[EmployeePayments] ADD  CONSTRAINT [DF_EmployeePayments_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[EmployeeProjects] ADD  CONSTRAINT [DF_EmployeeProjects_ModifiedBy]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[EmployeeProjects] ADD  CONSTRAINT [DF_EmployeeProjects_ModifiedDate]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[EmployeeProjects] ADD  CONSTRAINT [DF_EmployeeProjects_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_CreatedBy]  DEFAULT ((-1)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_CreatedBy1]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_CreatedDate1]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[GlobalCodes] ADD  CONSTRAINT [DF_GlobalCodes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[GlobalCodes] ADD  CONSTRAINT [DF_GlobalCodes_CreatedBy]  DEFAULT ((-1)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[GlobalCodes] ADD  CONSTRAINT [DF_GlobalCodes_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[GlobalCodes] ADD  CONSTRAINT [DF_GlobalCodes_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ProjectPayments] ADD  CONSTRAINT [DF_ProjectPayments_ModifiedBy]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[ProjectPayments] ADD  CONSTRAINT [DF_ProjectPayments_ModifiedDate]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[ProjectPayments] ADD  CONSTRAINT [DF_ProjectPayments_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_ModifiedBy]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_ModifiedDate]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [DF_Projects_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ModifiedBy]  DEFAULT ((-1)) FOR [ModifiedBy]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ModifiedDate]  DEFAULT (getutcdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
