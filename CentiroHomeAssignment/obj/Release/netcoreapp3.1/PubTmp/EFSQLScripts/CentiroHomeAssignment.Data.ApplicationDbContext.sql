IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312161516_addOrderToDatabase')
BEGIN
    CREATE TABLE [Order] (
        [Id] int NOT NULL IDENTITY,
        [OrderNumber] int NOT NULL,
        [OrderLineNumber] int NOT NULL,
        [ProductNumber] nvarchar(max) NULL,
        [Quantity] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Price] float NOT NULL,
        [ProductGroup] nvarchar(max) NULL,
        [OrderDate] nvarchar(max) NULL,
        [CustomerName] nvarchar(max) NULL,
        [CustomerNumber] int NOT NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220312161516_addOrderToDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220312161516_addOrderToDatabase', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313015520_updateDateType')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'ProductNumber');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Order] ALTER COLUMN [ProductNumber] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313015520_updateDateType')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'ProductGroup');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Order] ALTER COLUMN [ProductGroup] nvarchar(max) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313015520_updateDateType')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'OrderDate');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Order] ALTER COLUMN [OrderDate] datetime2 NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313015520_updateDateType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220313015520_updateDateType', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220313163837_addRanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220313163837_addRanges', N'3.1.3');
END;

GO

