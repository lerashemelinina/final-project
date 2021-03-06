USE [RentCarDB]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 15/01/2019 22:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[Adress] [nvarchar](100) NOT NULL,
	[Latitude] [int] NOT NULL,
	[Lingitude] [int] NOT NULL,
	[BranchName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cars]    Script Date: 15/01/2019 22:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[CarId] [int] IDENTITY(1,1) NOT NULL,
	[CarTypeId] [int] NOT NULL,
	[Mileage] [int] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[BranchId] [int] NOT NULL,
	[IsForRent] [bit] NOT NULL,
	[CarNumber] [int] NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[CarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CarTypes]    Script Date: 15/01/2019 22:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarTypes](
	[CarTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Make] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[DelayCharge] [money] NOT NULL,
	[Year] [int] NOT NULL,
	[IsAutomatic] [bit] NOT NULL,
 CONSTRAINT [PK_CarTypes] PRIMARY KEY CLUSTERED 
(
	[CarTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 15/01/2019 22:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[StartRent] [date] NOT NULL,
	[EndRent] [date] NOT NULL,
	[ReturnDate] [date] NULL,
	[UserId] [int] NOT NULL,
	[CarId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/01/2019 22:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](40) NOT NULL,
	[IdentityNumber] [nvarchar](9) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[BirthDate] [date] NULL,
	[IsMale] [bit] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[UserRole] [nvarchar](15) NOT NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([BranchId], [Adress], [Latitude], [Lingitude], [BranchName]) VALUES (1, N'Herzel 15, Tel Aiv', 320619253, 347704043, N'Car Rental TL')
INSERT [dbo].[Branches] ([BranchId], [Adress], [Latitude], [Lingitude], [BranchName]) VALUES (2, N'Rotshild 33, Rishon Le Zion', 319639418, 348025929, N'Car Rental RZ')
INSERT [dbo].[Branches] ([BranchId], [Adress], [Latitude], [Lingitude], [BranchName]) VALUES (4, N'Rager 15, Beer Sheva', 312456148, 347961802, N'Car Rental B-7')
SET IDENTITY_INSERT [dbo].[Branches] OFF
SET IDENTITY_INSERT [dbo].[Cars] ON 

INSERT [dbo].[Cars] ([CarId], [CarTypeId], [Mileage], [Image], [BranchId], [IsForRent], [CarNumber]) VALUES (15, 11, 56, N'car5180249294.jpg', 1, 1, 6767767)
INSERT [dbo].[Cars] ([CarId], [CarTypeId], [Mileage], [Image], [BranchId], [IsForRent], [CarNumber]) VALUES (16, 10, 5672, N'car6183341041.jpg', 4, 1, 4444437)
INSERT [dbo].[Cars] ([CarId], [CarTypeId], [Mileage], [Image], [BranchId], [IsForRent], [CarNumber]) VALUES (17, 10, 43, N'car4183542568.jpg', 2, 1, 2222222)
INSERT [dbo].[Cars] ([CarId], [CarTypeId], [Mileage], [Image], [BranchId], [IsForRent], [CarNumber]) VALUES (18, 7, 33, N'car5183904386.jpg', 1, 1, 33333333)
SET IDENTITY_INSERT [dbo].[Cars] OFF
SET IDENTITY_INSERT [dbo].[CarTypes] ON 

INSERT [dbo].[CarTypes] ([CarTypeId], [Make], [Model], [Price], [DelayCharge], [Year], [IsAutomatic]) VALUES (7, N'mazda', N'mazda6', 150.0000, 150.0000, 2016, 1)
INSERT [dbo].[CarTypes] ([CarTypeId], [Make], [Model], [Price], [DelayCharge], [Year], [IsAutomatic]) VALUES (10, N'toyota', N'yaris', 100.0000, 120.0000, 2017, 1)
INSERT [dbo].[CarTypes] ([CarTypeId], [Make], [Model], [Price], [DelayCharge], [Year], [IsAutomatic]) VALUES (11, N'toyota', N'yaris', 130.0000, 140.0000, 2018, 1)
INSERT [dbo].[CarTypes] ([CarTypeId], [Make], [Model], [Price], [DelayCharge], [Year], [IsAutomatic]) VALUES (1012, N'fiat', N'punto', 120.0000, 129.0000, 2015, 1)
INSERT [dbo].[CarTypes] ([CarTypeId], [Make], [Model], [Price], [DelayCharge], [Year], [IsAutomatic]) VALUES (2012, N'SUBARU', N'IMPREZA', 130.0000, 140.0000, 2017, 1)
INSERT [dbo].[CarTypes] ([CarTypeId], [Make], [Model], [Price], [DelayCharge], [Year], [IsAutomatic]) VALUES (3012, N'toyota', N'corolla', 123.0000, 130.0000, 2015, 1)
SET IDENTITY_INSERT [dbo].[CarTypes] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [StartRent], [EndRent], [ReturnDate], [UserId], [CarId]) VALUES (2014, CAST(N'2019-01-17' AS Date), CAST(N'2019-01-18' AS Date), CAST(N'2019-01-19' AS Date), 23, 15)
INSERT [dbo].[Orders] ([OrderId], [StartRent], [EndRent], [ReturnDate], [UserId], [CarId]) VALUES (2015, CAST(N'2019-01-17' AS Date), CAST(N'2019-01-18' AS Date), CAST(N'0001-01-01' AS Date), 23, 18)
INSERT [dbo].[Orders] ([OrderId], [StartRent], [EndRent], [ReturnDate], [UserId], [CarId]) VALUES (2016, CAST(N'2019-01-18' AS Date), CAST(N'2019-01-19' AS Date), CAST(N'0001-01-01' AS Date), 23, 16)
INSERT [dbo].[Orders] ([OrderId], [StartRent], [EndRent], [ReturnDate], [UserId], [CarId]) VALUES (2017, CAST(N'2019-01-17' AS Date), CAST(N'2019-01-19' AS Date), CAST(N'0001-01-01' AS Date), 23, 17)
INSERT [dbo].[Orders] ([OrderId], [StartRent], [EndRent], [ReturnDate], [UserId], [CarId]) VALUES (2018, CAST(N'2019-02-06' AS Date), CAST(N'2019-09-08' AS Date), CAST(N'0001-01-01' AS Date), 23, 17)
INSERT [dbo].[Orders] ([OrderId], [StartRent], [EndRent], [ReturnDate], [UserId], [CarId]) VALUES (2019, CAST(N'2019-01-16' AS Date), CAST(N'2019-01-17' AS Date), CAST(N'0001-01-01' AS Date), 23, 16)
INSERT [dbo].[Orders] ([OrderId], [StartRent], [EndRent], [ReturnDate], [UserId], [CarId]) VALUES (2020, CAST(N'2019-01-30' AS Date), CAST(N'2019-02-06' AS Date), CAST(N'0001-01-01' AS Date), 23, 16)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FullName], [IdentityNumber], [UserName], [BirthDate], [IsMale], [Email], [Password], [UserRole], [Image]) VALUES (20, N'lera', N'317948636', N'leraSH', CAST(N'1985-10-04' AS Date), 0, N'lera1@gmail.com', N'12345678', N'employee', N'photo1182438563')
INSERT [dbo].[Users] ([UserId], [FullName], [IdentityNumber], [UserName], [BirthDate], [IsMale], [Email], [Password], [UserRole], [Image]) VALUES (21, N'Nasti', N'317949808', N'nastik123', CAST(N'1999-10-04' AS Date), 0, N'nastik@gmail.com', N'12345678', N'manager', NULL)
INSERT [dbo].[Users] ([UserId], [FullName], [IdentityNumber], [UserName], [BirthDate], [IsMale], [Email], [Password], [UserRole], [Image]) VALUES (23, N'ella', N'317958767', N'ella1234', CAST(N'1990-10-10' AS Date), 0, N'ela@gmail.com', N'12345678', N'customer', NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_Branches] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([BranchId])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_Branches]
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK_Cars_CarTypes] FOREIGN KEY([CarTypeId])
REFERENCES [dbo].[CarTypes] ([CarTypeId])
GO
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK_Cars_CarTypes]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Cars] FOREIGN KEY([CarId])
REFERENCES [dbo].[Cars] ([CarId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Cars]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
