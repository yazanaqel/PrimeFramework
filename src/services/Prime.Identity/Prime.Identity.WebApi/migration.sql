IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');

CREATE TABLE [Identity].[Permissions] (
    [Id] int NOT NULL IDENTITY,
    [PermissionName] nvarchar(max) NOT NULL,
    [NormalizedName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);

CREATE TABLE [Identity].[Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);

CREATE TABLE [Identity].[UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey])
);

CREATE TABLE [Identity].[Users] (
    [Id] uniqueidentifier NOT NULL,
    [RefreshToken] nvarchar(max) NOT NULL,
    [RefreshTokenExpiryTime] datetime2 NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Identity].[UserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);

CREATE TABLE [Identity].[RolePermissions] (
    [Id] int NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([RoleId], [Id]),
    CONSTRAINT [FK_RolePermissions_Permissions_Id] FOREIGN KEY ([Id]) REFERENCES [Identity].[Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Roles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Identity].[UserPermissions] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserPermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPermissions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Identity].[UserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_RolePermissions_Id] ON [Identity].[RolePermissions] ([Id]);

CREATE INDEX [IX_UserPermissions_UserId] ON [Identity].[UserPermissions] ([UserId]);

CREATE INDEX [IX_UserRoles_RoleId] ON [Identity].[UserRoles] ([RoleId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260625115756_Initial_Create', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
IF SCHEMA_ID(N'Business') IS NULL EXEC(N'CREATE SCHEMA [Business];');

CREATE TABLE [Business].[Categories] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [ParentCategoryId] uniqueidentifier NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Categories_Categories_ParentCategoryId] FOREIGN KEY ([ParentCategoryId]) REFERENCES [Business].[Categories] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Business].[Stores] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ImageCover] nvarchar(max) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [IsShippingAvailable] bit NOT NULL,
    [City] int NOT NULL,
    [StoreStatus] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Stores] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Stores_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Business].[Categories] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Business].[Products] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [StockQuantity] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [StoreId] uniqueidentifier NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Business].[Categories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Business].[Stores] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Business].[Orders] (
    [Id] uniqueidentifier NOT NULL,
    [OrderNumber] bigint NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [StoreId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Business].[Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Business].[Stores] ([Id])
);

CREATE TABLE [Business].[OrderItems] (
    [Id] uniqueidentifier NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [Quantity] int NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Business].[Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Business].[Products] ([Id]) ON DELETE NO ACTION
);

CREATE UNIQUE INDEX [IX_Categories_Name_ParentCategoryId] ON [Business].[Categories] ([Name], [ParentCategoryId]) WHERE [ParentCategoryId] IS NOT NULL;

CREATE INDEX [IX_Categories_ParentCategoryId] ON [Business].[Categories] ([ParentCategoryId]);

CREATE INDEX [IX_OrderItems_OrderId] ON [Business].[OrderItems] ([OrderId]);

CREATE INDEX [IX_OrderItems_ProductId] ON [Business].[OrderItems] ([ProductId]);

CREATE UNIQUE INDEX [IX_Orders_OrderNumber] ON [Business].[Orders] ([OrderNumber]);

CREATE INDEX [IX_Orders_ProductId] ON [Business].[Orders] ([ProductId]);

CREATE INDEX [IX_Orders_StoreId] ON [Business].[Orders] ([StoreId]);

CREATE INDEX [IX_Orders_UserId] ON [Business].[Orders] ([UserId]);

CREATE INDEX [IX_Products_CategoryId] ON [Business].[Products] ([CategoryId]);

CREATE INDEX [IX_Products_Name] ON [Business].[Products] ([Name]);

CREATE INDEX [IX_Products_StoreId] ON [Business].[Products] ([StoreId]);

CREATE INDEX [IX_Stores_CategoryId] ON [Business].[Stores] ([CategoryId]);

CREATE INDEX [IX_Stores_UserId] ON [Business].[Stores] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260626130022_Business', N'10.0.9');

COMMIT;
GO

