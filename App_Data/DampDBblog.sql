if (select name from sys.databases where name like'DbBlog' ) != null
  DROP database DbBlog
Create DataBase [DbBlog]
go

use DbBlog
CREATE TABLE [dbo].[Record] (
    [Id_Record]        INT IDENTITY(1,1) NOT NULL,
	[Category_Id]      int NOT NULL,
    [Title]       NVARCHAR (500) NOT NULL,
	[Text]       NVARCHAR (Max),
	[PreviewText]       NVARCHAR (Max),
[DateCreate] DateTime NOT NULL,
[DateEdit] DateTime,
[NameImageRecord] NVARCHAR (200), 
[NameVideoRecord] NVARCHAR (200),
    PRIMARY KEY CLUSTERED ([Id_Record] ASC)
);



CREATE TABLE [dbo].[Comment] (
    [Id_Comment] INT IDENTITY(1,1) NOT NULL,
[IdUser]      int,
[Text]       NVARCHAR (Max),
[DateCreate] DateTime NOT NULL,
[NameAuthorComment] NVARCHAR (200),
[IdRecord]      int,

    PRIMARY KEY CLUSTERED ([Id_Comment] ASC)
);




CREATE TABLE [dbo].[Categories] (
    [Id_Category]        INT IDENTITY(1,1) NOT NULL,
[NameCategory]       NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Category] ASC)
);

CREATE TABLE [dbo].[MailResults] (
    [Id_mailRecord]        INT IDENTITY(1,1) NOT NULL,
[NameSender]       NVARCHAR (50) NOT NULL,
[EMailSender]       NVARCHAR (50) NOT NULL,
[TextSender]       NVARCHAR (Max) NOT NULL,
[ThemeMail]       NVARCHAR (400) NOT NULL,
[TimeSending]		DateTime NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_mailRecord] ASC)
);





