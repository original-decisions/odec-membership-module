
begin tran

	IF schema_id('users') IS NULL
			EXECUTE('CREATE SCHEMA [users]')
				IF schema_id('AspNet') IS NULL
			EXECUTE('CREATE SCHEMA [AspNet]')
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[users].[Sexes]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[Sexes] (
        [Id] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](50) NOT NULL,
        [Code] [nvarchar](128) NOT NULL,
        [IsActive] [bit] NOT NULL,
        [SortOrder] [int] NOT NULL,
        [DateUpdated] [datetime] NOT NULL,
        [DateCreated] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.Sexes] PRIMARY KEY ([Id])
    )
end

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[EmailUrlRelations]') AND type in (N'U'))
begin
    CREATE TABLE [dbo].[EmailUrlRelations] (
        [Postfix] [nvarchar](40) NOT NULL,
        [EmailUrl] [nvarchar](255) NOT NULL,
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.EmailUrlRelations] PRIMARY KEY ([Postfix])
    )
End

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[AspNet].[Roles]') AND type in (N'U'))
begin
    CREATE TABLE [AspNet].[Roles] (
        [Id] [int] NOT NULL IDENTITY,
        [InRoleId] [int],
        [Scope] [nvarchar](max),
        [Name] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_AspNet.Roles] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_InRoleId] ON [AspNet].[Roles]([InRoleId])
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNet].[Roles]([Name])
	end

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[AspNet].[UserRoles]') AND type in (N'U'))
begin
    CREATE TABLE [AspNet].[UserRoles] (
        [UserId] [int] NOT NULL,
        [RoleId] [int] NOT NULL,
        CONSTRAINT [PK_AspNet.UserRoles] PRIMARY KEY ([UserId], [RoleId])
    )
    CREATE INDEX [IX_UserId] ON [AspNet].[UserRoles]([UserId])
    CREATE INDEX [IX_RoleId] ON [AspNet].[UserRoles]([RoleId])
End

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[AspNet].[UserClaims]') AND type in (N'U'))
begin
    CREATE TABLE [AspNet].[UserClaims] (
        [Id] [int] NOT NULL IDENTITY,
        [UserId] [int] NOT NULL,
        [ClaimType] [nvarchar](max),
        [ClaimValue] [nvarchar](max),
        CONSTRAINT [PK_AspNet.UserClaims] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_UserId] ON [AspNet].[UserClaims]([UserId])
end

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[AspNet].[UserLogins]') AND type in (N'U'))
begin
    CREATE TABLE [AspNet].[UserLogins] (
        [LoginProvider] [nvarchar](128) NOT NULL,
        [ProviderKey] [nvarchar](128) NOT NULL,
        [UserId] [int] NOT NULL,
        CONSTRAINT [PK_AspNet.UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
    )
    CREATE INDEX [IX_UserId] ON [AspNet].[UserLogins]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[AspNet].[Users]') AND type in (N'U'))
begin
    CREATE TABLE [AspNet].[Users] (
        [Id] [int] NOT NULL IDENTITY,
        [SexId] [int],
        [AddressLine1] [nvarchar](max) NOT NULL,
        [AddressLine2] [nvarchar](max) NOT NULL,
        [MainPhone] [nvarchar](max) NOT NULL,
        [AdditionalPhone] [nvarchar](max) NOT NULL,
        [ProfilePicturePath] [nvarchar](max),
        [FirstName] [nvarchar](max),
        [LastName] [nvarchar](max),
        [Patronymic] [nvarchar](max),
        [DateUpdated] [datetime],
        [LastActivityDate] [datetime],
        [LastLogin] [datetime],
        [RemindInDays] [int] NOT NULL,
        [DateRegistration] [datetime] NOT NULL,
        [Email] [nvarchar](256),
        [EmailConfirmed] [bit] NOT NULL,
        [PasswordHash] [nvarchar](max),
        [SecurityStamp] [nvarchar](max),
        [PhoneNumber] [nvarchar](max),
        [PhoneNumberConfirmed] [bit] NOT NULL,
        [TwoFactorEnabled] [bit] NOT NULL,
        [LockoutEndDateUtc] [datetime],
        [LockoutEnabled] [bit] NOT NULL,
        [AccessFailedCount] [int] NOT NULL,
        [UserName] [nvarchar](256) NOT NULL,
		[Description] [text] null,
		[Rating] decimal(18,2) not null default (0)
        CONSTRAINT [PK_AspNet.Users] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_SexId] ON [AspNet].[Users]([SexId])
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNet].[Users]([UserName])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserClaims_AspNet.Users_UserId')
		begin
			ALTER TABLE [AspNet].[UserClaims] ADD CONSTRAINT [FK_AspNet.UserClaims_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserLogins_AspNet.Users_UserId')
		begin
			ALTER TABLE [AspNet].[UserLogins] ADD CONSTRAINT [FK_AspNet.UserLogins_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
		end 
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Users_UserId')
		begin
			ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id]) 
		end 
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.UserRoles_AspNet.Roles_RoleId')
		begin
			ALTER TABLE [AspNet].[UserRoles] ADD CONSTRAINT [FK_AspNet.UserRoles_AspNet.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNet].[Roles] ([Id]) 
		end 
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_AspNet.Roles_AspNet.Roles_InRoleId')
		begin
			ALTER TABLE [AspNet].[Roles] ADD CONSTRAINT [FK_AspNet.Roles_AspNet.Roles_InRoleId] FOREIGN KEY ([InRoleId]) REFERENCES [AspNet].[Roles] ([Id])
		end 
commit tran