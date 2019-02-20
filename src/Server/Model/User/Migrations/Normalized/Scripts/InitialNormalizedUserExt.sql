DECLARE @CurrentMigration [nvarchar](max)
begin tran
BEGIN

	IF schema_id('users') IS NULL
			EXECUTE('CREATE SCHEMA [users]')
				IF schema_id('location') IS NULL
			EXECUTE('CREATE SCHEMA [location]')
IF schema_id('AspNet') IS NULL
			EXECUTE('CREATE SCHEMA [AspNet]')
	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[users].[ContactPhones]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[ContactPhones] (
			[PhoneId] [int] NOT NULL,
			[ContactId] [int] NOT NULL,
			CONSTRAINT [PK_users.ContactPhones] PRIMARY KEY ([PhoneId], [ContactId])
		)
		CREATE INDEX [IX_PhoneId] ON [users].[ContactPhones]([PhoneId])
		CREATE INDEX [IX_ContactId] ON [users].[ContactPhones]([ContactId])
	end



	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[users].[Contact]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[Contact] (
			[Id] [int] NOT NULL IDENTITY,
			[AddressDenormolized] [nvarchar](200),
			[PhoneNumberDenormolized] [nvarchar](10),
			[FirstName] [nvarchar](50) NOT NULL,
			[Patronymic] [nvarchar](50) NOT NULL,
			[LastName] [nvarchar](50) NOT NULL,
			[Email] [nvarchar](20),
			[SexId] [int] NOT NULL,
			[BirthdayDate] [datetime],
			[SendNews] [bit] NOT NULL,
			[Name] [nvarchar](max) NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_users.Contact] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_SexId] ON [users].[Contact]([SexId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[users].[Sexes]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[Sexes] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](50) NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_users.Sexes] PRIMARY KEY ([Id])
		)
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[users].[Phones]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[Phones] (
			[Id] [int] NOT NULL IDENTITY,
			[PhoneNumber] [nvarchar](20) NOT NULL,
			[PhoneTypeId] [int] NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_users.Phones] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_PhoneTypeId] ON [users].[Phones]([PhoneTypeId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Addresses]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Addresses] (
			[Id] [int] NOT NULL IDENTITY,
			[CountryId] [int] NOT NULL,
			[StreetId] [int] NOT NULL,
			[SubwayId] [int] NOT NULL,
			[CityId] [int] NOT NULL,
			[HouseId] [int] NOT NULL,
			[HousingId] [int] NOT NULL,
			[FlatId] [int] NOT NULL,
			[StringRepresentation] [nvarchar](max) NOT NULL,
			[Name] [nvarchar](max) NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL
			CONSTRAINT [PK_location.Addresses] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_CountryId] ON [location].[Addresses]([CountryId])
		CREATE INDEX [IX_StreetId] ON [location].[Addresses]([StreetId])
		CREATE INDEX [IX_SubwayId] ON [location].[Addresses]([SubwayId])
		CREATE INDEX [IX_CityId] ON [location].[Addresses]([CityId])
		CREATE INDEX [IX_HouseId] ON [location].[Addresses]([HouseId])
		CREATE INDEX [IX_HousingId] ON [location].[Addresses]([HousingId])
		CREATE INDEX [IX_FlatId] ON [location].[Addresses]([FlatId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Cities]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Cities] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](255) NOT NULL,
			[Region] [nvarchar](100) NOT NULL,
			[Genitive] [nvarchar](100) NOT NULL,
			[Prepositional] [nvarchar](100) NOT NULL,
			[PhoneCode] [int] NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_location.Cities] PRIMARY KEY ([Id])
		)
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Subways]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Subways] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](50) NOT NULL,
			[CityId] [int] NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_location.Subways] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_CityId] ON [location].[Subways]([CityId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Countries]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Countries] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](70) NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_location.Countries] PRIMARY KEY ([Id])
		)
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Flats]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Flats] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](10) NOT NULL,
			[HouseId] [int] NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_location.Flats] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_HouseId] ON [location].[Flats]([HouseId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Houses]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Houses] (
			[Id] [int] NOT NULL IDENTITY,
			[Number] [nvarchar](10) NOT NULL,
			[StreetId] [int] NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_location.Houses] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_StreetId] ON [location].[Houses]([StreetId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Housings]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Housings] (
			[Id] [int] NOT NULL IDENTITY,
			[Number] [nvarchar](10) NOT NULL,
			[HouseId] [int] NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_location.Housings] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_HouseId] ON [location].[Housings]([HouseId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[location].[Streets]') AND type in (N'U'))
	begin
		CREATE TABLE [location].[Streets] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](255) NOT NULL,
			[CityId] [int] NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_location.Streets] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_CityId] ON [location].[Streets]([CityId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[users].[PhoneTypes]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[PhoneTypes] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](50) NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_users.PhoneTypes] PRIMARY KEY ([Id])
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
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[users].[FeedBackComments]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[FeedBackComments] (
			[Id] [int] NOT NULL IDENTITY,
			[Rate] [decimal](18, 2) NOT NULL,
			[UserId] [int] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[Comment] [nvarchar](max) NOT NULL,
			[TargetId] [int] NOT NULL,
			[Target] [int] NOT NULL,
			CONSTRAINT [PK_users.FeedBackComments] PRIMARY KEY ([Id])
		)
		CREATE INDEX [IX_UserId] ON [users].[FeedBackComments]([UserId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[AspNet].[Users]') AND type in (N'U'))
	begin
		CREATE TABLE [AspNet].[Users] (
			[Id] [int] NOT NULL IDENTITY,
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
		CREATE UNIQUE INDEX [UserNameIndex] ON [AspNet].[Users]([UserName])
	end

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
	WHERE object_id = OBJECT_ID(N'[AspNet].[UserRoles]') AND type in (N'U'))
	begin
		CREATE TABLE [AspNet].[UserRoles] (
			[UserId] [int] NOT NULL,
			[RoleId] [int] NOT NULL,
			CONSTRAINT [PK_AspNet.UserRoles] PRIMARY KEY ([UserId], [RoleId])
		)
		CREATE INDEX [IX_UserId] ON [AspNet].[UserRoles]([UserId])
		CREATE INDEX [IX_RoleId] ON [AspNet].[UserRoles]([RoleId])
	end

	IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[users].[Hobbies]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[Hobbies] (
			[Id] [int] NOT NULL IDENTITY,
			[Name] [nvarchar](max) NOT NULL,
			[Code] [nvarchar](128) NOT NULL,
			[IsActive] [bit] NOT NULL,
			[SortOrder] [int] NOT NULL,
			[DateUpdated] [datetime] NOT NULL,
			[DateCreated] [datetime] NOT NULL,
			CONSTRAINT [PK_users.Hobbies] PRIMARY KEY ([Id])
		)
	end

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
	WHERE object_id = OBJECT_ID(N'[users].[UserHobbies]') AND type in (N'U'))
	begin
		CREATE TABLE [users].[UserHobbies] (
			[UserId] [int] NOT NULL,
			[HobbyId] [int] NOT NULL,
			CONSTRAINT [PK_users.UserHobbies] PRIMARY KEY ([UserId], [HobbyId])
		)
		CREATE INDEX [IX_UserId] ON [users].[UserHobbies]([UserId])
		CREATE INDEX [IX_HobbyId] ON [users].[UserHobbies]([HobbyId])
	end
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[users].[UserSkills]') AND type in (N'U'))
	begin
			CREATE TABLE [users].[UserSkills] (
			[SkillId] [int] NOT NULL,
			[UserId] [int] NOT NULL,
			CONSTRAINT [PK_users.UserSkills] PRIMARY KEY ([SkillId], [UserId])
		)
		CREATE INDEX [IX_SkillId] ON [users].[UserSkills]([SkillId])
		CREATE INDEX [IX_UserId] ON [users].[UserSkills]([UserId])
end
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
	begin
	CREATE TABLE [dbo].[Categories] (
		[Id] [int] NOT NULL IDENTITY,
		[Name] [nvarchar](255) NOT NULL,
		[IsApproved] [bit] NOT NULL,
		[Code] [nvarchar](128) NOT NULL,
		[IsActive] [bit] NOT NULL,
		[SortOrder] [int] NOT NULL,
		[DateUpdated] [datetime] NOT NULL,
		[DateCreated] [datetime] NOT NULL,
		CONSTRAINT [PK_dbo.Categories] PRIMARY KEY ([Id])
	)
	CREATE INDEX [ix_Categories_Name] ON [dbo].[Categories]([Name], [IsApproved])
	CREATE INDEX [ix_Categories_IsApproved] ON [dbo].[Categories]([IsApproved])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.UserSkills_dbo.Categories_SkillId')
		begin
ALTER TABLE [users].[UserSkills] ADD CONSTRAINT [FK_users.UserSkills_dbo.Categories_SkillId] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Categories] ([Id])
end
	if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.UserSkills_AspNet.Users_UserId')
		begin
ALTER TABLE [users].[UserSkills] ADD CONSTRAINT [FK_users.UserSkills_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
end
if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.ContactPhones_users.Contact_ContactId')
		begin
			ALTER TABLE [users].[ContactPhones] ADD CONSTRAINT [FK_users.ContactPhones_users.Contact_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [users].[Contact] ([Id]) 
		end

		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.ContactPhones_users.Phones_PhoneId')
		begin
			ALTER TABLE [users].[ContactPhones] ADD CONSTRAINT [FK_users.ContactPhones_users.Phones_PhoneId] FOREIGN KEY ([PhoneId]) REFERENCES [users].[Phones] ([Id]) 
		end

		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.Contact_users.Sexes_SexId')
		begin
			ALTER TABLE [users].[Contact] ADD CONSTRAINT [FK_users.Contact_users.Sexes_SexId] FOREIGN KEY ([SexId]) REFERENCES [users].[Sexes] ([Id]) 
		end

		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.Phones_users.PhoneTypes_PhoneTypeId')
		begin
		ALTER TABLE [users].[Phones] ADD CONSTRAINT [FK_users.Phones_users.PhoneTypes_PhoneTypeId] FOREIGN KEY ([PhoneTypeId]) REFERENCES [users].[PhoneTypes] ([Id]) 
		end
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Addresses_location.Cities_CityId')
		begin
			ALTER TABLE [location].[Addresses] ADD CONSTRAINT [FK_location.Addresses_location.Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [location].[Cities] ([Id]) 
		end
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Addresses_location.Subways_SubwayId')
		begin
			ALTER TABLE [location].[Addresses] ADD CONSTRAINT [FK_location.Addresses_location.Subways_SubwayId] FOREIGN KEY ([SubwayId]) REFERENCES [location].[Subways] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Addresses_location.Countries_CountryId')
		begin
		ALTER TABLE [location].[Addresses] ADD CONSTRAINT [FK_location.Addresses_location.Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [location].[Countries] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Addresses_location.Flats_FlatId')
		begin
			ALTER TABLE [location].[Addresses] ADD CONSTRAINT [FK_location.Addresses_location.Flats_FlatId] FOREIGN KEY ([FlatId]) REFERENCES [location].[Flats] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Addresses_location.Houses_HouseId')
		begin
			ALTER TABLE [location].[Addresses] ADD CONSTRAINT [FK_location.Addresses_location.Houses_HouseId] FOREIGN KEY ([HouseId]) REFERENCES [location].[Houses] ([Id])
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Addresses_location.Housings_HousingId')
		begin
			ALTER TABLE [location].[Addresses] ADD CONSTRAINT [FK_location.Addresses_location.Housings_HousingId] FOREIGN KEY ([HousingId]) REFERENCES [location].[Housings] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Addresses_location.Streets_StreetId')
		begin
		ALTER TABLE [location].[Addresses] ADD CONSTRAINT [FK_location.Addresses_location.Streets_StreetId] FOREIGN KEY ([StreetId]) REFERENCES [location].[Streets] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Subways_location.Cities_CityId')
		begin
		ALTER TABLE [location].[Subways] ADD CONSTRAINT [FK_location.Subways_location.Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [location].[Cities] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Flats_location.Houses_HouseId')
		begin
		ALTER TABLE [location].[Flats] ADD CONSTRAINT [FK_location.Flats_location.Houses_HouseId] FOREIGN KEY ([HouseId]) REFERENCES [location].[Houses] ([Id]) 
		end 
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Houses_location.Streets_StreetId')
		begin
		ALTER TABLE [location].[Houses] ADD CONSTRAINT [FK_location.Houses_location.Streets_StreetId] FOREIGN KEY ([StreetId]) REFERENCES [location].[Streets] ([Id])
		end 
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Housings_location.Houses_HouseId')
		begin
		ALTER TABLE [location].[Housings] ADD CONSTRAINT [FK_location.Housings_location.Houses_HouseId] FOREIGN KEY ([HouseId]) REFERENCES [location].[Houses] ([Id]) 
		end
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_location.Streets_location.Cities_CityId')
		begin
			ALTER TABLE [location].[Streets] ADD CONSTRAINT [FK_location.Streets_location.Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [location].[Cities] ([Id]) 
		end 
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.FeedBackComments_AspNet.Users_UserId')
		begin
			ALTER TABLE [users].[FeedBackComments] ADD CONSTRAINT [FK_users.FeedBackComments_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id]) 
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
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.UserHobbies_users.Hobbies_HobbyId')
		begin
			ALTER TABLE [users].[UserHobbies] ADD CONSTRAINT [FK_users.UserHobbies_users.Hobbies_HobbyId] FOREIGN KEY ([HobbyId]) REFERENCES [users].[Hobbies] ([Id]) 
		end 
		
		if not exists (SELECT  name
                FROM    sys.foreign_keys
                WHERE   name = 'FK_users.UserHobbies_AspNet.Users_UserId')
		begin
			ALTER TABLE [users].[UserHobbies] ADD CONSTRAINT [FK_users.UserHobbies_AspNet.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNet].[Users] ([Id])
		end 
END
commit tran

