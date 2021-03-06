USE [emprestimosJogos-dev]
GO
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Perfil_PerfilId]
GO
ALTER TABLE [dbo].[Token] DROP CONSTRAINT [FK_Token_Usuario_AutenticacaoId]
GO
ALTER TABLE [dbo].[Token] DROP CONSTRAINT [FK_Token_TokenType_TokenTypeId]
GO
ALTER TABLE [dbo].[Jogo] DROP CONSTRAINT [FK_Jogo_Usuario_CreatorId]
GO
ALTER TABLE [dbo].[Jogo] DROP CONSTRAINT [FK_Jogo_Amigo_AmigoId]
GO
ALTER TABLE [dbo].[Amigo] DROP CONSTRAINT [FK_Amigo_Usuario_CreatorId]
GO
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [DF__Usuario__IsDelet__412EB0B6]
GO
ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [DF__Usuario__Created__403A8C7D]
GO
ALTER TABLE [dbo].[TokenType] DROP CONSTRAINT [DF__TokenType__IsDel__3D5E1FD2]
GO
ALTER TABLE [dbo].[TokenType] DROP CONSTRAINT [DF__TokenType__Creat__3C69FB99]
GO
ALTER TABLE [dbo].[Token] DROP CONSTRAINT [DF__Token__IsDeleted__4AB81AF0]
GO
ALTER TABLE [dbo].[Token] DROP CONSTRAINT [DF__Token__CreatedDa__49C3F6B7]
GO
ALTER TABLE [dbo].[Perfil] DROP CONSTRAINT [DF__Perfil__IsDelete__398D8EEE]
GO
ALTER TABLE [dbo].[Perfil] DROP CONSTRAINT [DF__Perfil__CreatedD__38996AB5]
GO
ALTER TABLE [dbo].[Jogo] DROP CONSTRAINT [DF__Jogo__IsEmpresta__5165187F]
GO
ALTER TABLE [dbo].[Jogo] DROP CONSTRAINT [DF__Jogo__IsDeleted__5070F446]
GO
ALTER TABLE [dbo].[Jogo] DROP CONSTRAINT [DF__Jogo__CreatedDat__4F7CD00D]
GO
ALTER TABLE [dbo].[Amigo] DROP CONSTRAINT [DF__Amigo__IsDeleted__45F365D3]
GO
ALTER TABLE [dbo].[Amigo] DROP CONSTRAINT [DF__Amigo__CreatedDa__44FF419A]
GO
/****** Object:  Index [IX_Usuario_PerfilId]    Script Date: 11/01/2021 00:05:35 ******/
DROP INDEX [IX_Usuario_PerfilId] ON [dbo].[Usuario]
GO
/****** Object:  Index [IX_Usuario_Id]    Script Date: 11/01/2021 00:05:35 ******/
DROP INDEX [IX_Usuario_Id] ON [dbo].[Usuario]
GO
/****** Object:  Index [IX_Token_TokenTypeId]    Script Date: 11/01/2021 00:05:35 ******/
DROP INDEX [IX_Token_TokenTypeId] ON [dbo].[Token]
GO
/****** Object:  Index [IX_Token_AutenticacaoId]    Script Date: 11/01/2021 00:05:35 ******/
DROP INDEX [IX_Token_AutenticacaoId] ON [dbo].[Token]
GO
/****** Object:  Index [IX_Jogo_CreatorId]    Script Date: 11/01/2021 00:05:35 ******/
DROP INDEX [IX_Jogo_CreatorId] ON [dbo].[Jogo]
GO
/****** Object:  Index [IX_Jogo_AmigoId]    Script Date: 11/01/2021 00:05:35 ******/
DROP INDEX [IX_Jogo_AmigoId] ON [dbo].[Jogo]
GO
/****** Object:  Index [IX_Amigo_CreatorId]    Script Date: 11/01/2021 00:05:35 ******/
DROP INDEX [IX_Amigo_CreatorId] ON [dbo].[Amigo]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/01/2021 00:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
DROP TABLE [dbo].[Usuario]
GO
/****** Object:  Table [dbo].[TokenType]    Script Date: 11/01/2021 00:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TokenType]') AND type in (N'U'))
DROP TABLE [dbo].[TokenType]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 11/01/2021 00:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Token]') AND type in (N'U'))
DROP TABLE [dbo].[Token]
GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 11/01/2021 00:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Perfil]') AND type in (N'U'))
DROP TABLE [dbo].[Perfil]
GO
/****** Object:  Table [dbo].[Jogo]    Script Date: 11/01/2021 00:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Jogo]') AND type in (N'U'))
DROP TABLE [dbo].[Jogo]
GO
/****** Object:  Table [dbo].[Amigo]    Script Date: 11/01/2021 00:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Amigo]') AND type in (N'U'))
DROP TABLE [dbo].[Amigo]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/01/2021 00:05:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
USE [master]
GO
/****** Object:  Database [emprestimosJogos-dev]    Script Date: 11/01/2021 00:05:35 ******/
DROP DATABASE [emprestimosJogos-dev]
GO
/****** Object:  Database [emprestimosJogos-dev]    Script Date: 11/01/2021 00:05:35 ******/
CREATE DATABASE [emprestimosJogos-dev]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'emprestimosJogos-dev', FILENAME = N'/var/opt/mssql/data/emprestimosJogos-dev.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'emprestimosJogos-dev_log', FILENAME = N'/var/opt/mssql/data/emprestimosJogos-dev_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [emprestimosJogos-dev] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [emprestimosJogos-dev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [emprestimosJogos-dev] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET ARITHABORT OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [emprestimosJogos-dev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [emprestimosJogos-dev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET  ENABLE_BROKER 
GO
ALTER DATABASE [emprestimosJogos-dev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [emprestimosJogos-dev] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [emprestimosJogos-dev] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET RECOVERY FULL 
GO
ALTER DATABASE [emprestimosJogos-dev] SET  MULTI_USER 
GO
ALTER DATABASE [emprestimosJogos-dev] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [emprestimosJogos-dev] SET DB_CHAINING OFF 
GO
ALTER DATABASE [emprestimosJogos-dev] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [emprestimosJogos-dev] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [emprestimosJogos-dev] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'emprestimosJogos-dev', N'ON'
GO
ALTER DATABASE [emprestimosJogos-dev] SET QUERY_STORE = OFF
GO
USE [emprestimosJogos-dev]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/01/2021 00:05:35 ******/
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
/****** Object:  Table [dbo].[Amigo]    Script Date: 11/01/2021 00:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amigo](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[CEP] [varchar](8) NULL,
	[Logradouro] [varchar](200) NULL,
	[Complemento] [varchar](100) NULL,
	[Bairro] [varchar](60) NULL,
	[Numero] [int] NULL,
	[TelefoneCelular] [varchar](14) NOT NULL,
	[CreatorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Amigo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jogo]    Script Date: 11/01/2021 00:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jogo](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[DataEmprestimo] [datetime] NULL,
	[IsEmprestado] [bit] NOT NULL,
	[AmigoId] [uniqueidentifier] NULL,
	[CreatorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Jogo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 11/01/2021 00:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfil](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Key] [varchar](4) NULL,
	[Nome] [varchar](50) NOT NULL,
	[Descricao] [varchar](150) NULL,
	[NormalizedRoleName] [varchar](100) NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 11/01/2021 00:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Value] [text] NOT NULL,
	[TokenTypeId] [uniqueidentifier] NOT NULL,
	[AutenticacaoId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenType]    Script Date: 11/01/2021 00:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenType](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Key] [varchar](4) NULL,
	[Value] [text] NOT NULL,
 CONSTRAINT [PK_TokenType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/01/2021 00:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[PerfilId] [uniqueidentifier] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[PhoneNumber] [varchar](100) NULL,
	[ConcurrencyStamp] [varchar](100) NULL,
	[SecurityStamp] [varchar](100) NULL,
	[PasswordHash] [varchar](150) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[NormalizedEmail] [varchar](100) NULL,
	[Email] [varchar](150) NOT NULL,
	[NormalizedUserName] [varchar](100) NULL,
	[UserName] [varchar](100) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210109221811_Initial_Migration', N'3.0.1')
GO
INSERT [dbo].[Perfil] ([Id], [CreatedDate], [ModifiedDate], [IsDeleted], [Key], [Nome], [Descricao], [NormalizedRoleName]) VALUES (N'8907d860-ceb1-4345-b798-8757200e90c9', CAST(N'2021-01-09T19:18:11.590' AS DateTime), NULL, 0, N'ADM', N'Administrador', N'Perfil para usuários administradores do sistema.', N'ADMINISTRADOR')
INSERT [dbo].[Perfil] ([Id], [CreatedDate], [ModifiedDate], [IsDeleted], [Key], [Nome], [Descricao], [NormalizedRoleName]) VALUES (N'59b14405-b75a-4026-b269-d6cc24794a44', CAST(N'2021-01-10T19:40:49.050' AS DateTime), NULL, 0, N'USU', N'Usuario', N'Perfil', N'USUARIO')
GO
INSERT [dbo].[TokenType] ([Id], [CreatedDate], [ModifiedDate], [IsDeleted], [Key], [Value]) VALUES (N'652f2144-7b66-4ac4-967f-e0bae568ebb1', CAST(N'2021-01-09T19:18:11.587' AS DateTime), NULL, 0, N'RESE', N'Token para reset de senha (recuperação de senha).')
GO
/****** Object:  Index [IX_Amigo_CreatorId]    Script Date: 11/01/2021 00:05:35 ******/
CREATE NONCLUSTERED INDEX [IX_Amigo_CreatorId] ON [dbo].[Amigo]
(
	[CreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Jogo_AmigoId]    Script Date: 11/01/2021 00:05:35 ******/
CREATE NONCLUSTERED INDEX [IX_Jogo_AmigoId] ON [dbo].[Jogo]
(
	[AmigoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Jogo_CreatorId]    Script Date: 11/01/2021 00:05:35 ******/
CREATE NONCLUSTERED INDEX [IX_Jogo_CreatorId] ON [dbo].[Jogo]
(
	[CreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Token_AutenticacaoId]    Script Date: 11/01/2021 00:05:35 ******/
CREATE NONCLUSTERED INDEX [IX_Token_AutenticacaoId] ON [dbo].[Token]
(
	[AutenticacaoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Token_TokenTypeId]    Script Date: 11/01/2021 00:05:35 ******/
CREATE NONCLUSTERED INDEX [IX_Token_TokenTypeId] ON [dbo].[Token]
(
	[TokenTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Usuario_Id]    Script Date: 11/01/2021 00:05:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuario_Id] ON [dbo].[Usuario]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Usuario_PerfilId]    Script Date: 11/01/2021 00:05:35 ******/
CREATE NONCLUSTERED INDEX [IX_Usuario_PerfilId] ON [dbo].[Usuario]
(
	[PerfilId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Amigo] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Amigo] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Jogo] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Jogo] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Jogo] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsEmprestado]
GO
ALTER TABLE [dbo].[Perfil] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Perfil] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Token] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Token] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TokenType] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TokenType] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Amigo]  WITH CHECK ADD  CONSTRAINT [FK_Amigo_Usuario_CreatorId] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Amigo] CHECK CONSTRAINT [FK_Amigo_Usuario_CreatorId]
GO
ALTER TABLE [dbo].[Jogo]  WITH CHECK ADD  CONSTRAINT [FK_Jogo_Amigo_AmigoId] FOREIGN KEY([AmigoId])
REFERENCES [dbo].[Amigo] ([Id])
GO
ALTER TABLE [dbo].[Jogo] CHECK CONSTRAINT [FK_Jogo_Amigo_AmigoId]
GO
ALTER TABLE [dbo].[Jogo]  WITH CHECK ADD  CONSTRAINT [FK_Jogo_Usuario_CreatorId] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Jogo] CHECK CONSTRAINT [FK_Jogo_Usuario_CreatorId]
GO
ALTER TABLE [dbo].[Token]  WITH CHECK ADD  CONSTRAINT [FK_Token_TokenType_TokenTypeId] FOREIGN KEY([TokenTypeId])
REFERENCES [dbo].[TokenType] ([Id])
GO
ALTER TABLE [dbo].[Token] CHECK CONSTRAINT [FK_Token_TokenType_TokenTypeId]
GO
ALTER TABLE [dbo].[Token]  WITH CHECK ADD  CONSTRAINT [FK_Token_Usuario_AutenticacaoId] FOREIGN KEY([AutenticacaoId])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Token] CHECK CONSTRAINT [FK_Token_Usuario_AutenticacaoId]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Perfil_PerfilId] FOREIGN KEY([PerfilId])
REFERENCES [dbo].[Perfil] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Perfil_PerfilId]
GO
USE [master]
GO
ALTER DATABASE [emprestimosJogos-dev] SET  READ_WRITE 
GO
