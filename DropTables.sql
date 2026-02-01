--Vider la DB 
-- 1. Supprimer toutes les Clés Étrangères (FK) pour éviter les blocages
DECLARE @sql_fk NVARCHAR(MAX) = N'';
SELECT @sql_fk += 'ALTER TABLE ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name) + ' DROP CONSTRAINT ' + QUOTENAME(f.name) + ';'
FROM sys.foreign_keys AS f
INNER JOIN sys.tables AS t ON f.parent_object_id = t.object_id
INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id;
EXEC sp_executesql @sql_fk;

-- 2. Supprimer toutes les Tables
DECLARE @sql_tables NVARCHAR(MAX) = N'';
SELECT @sql_tables += 'DROP TABLE ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name) + ';'
FROM sys.tables AS t
INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id
WHERE t.name NOT LIKE '__EFMigrationsHistory'; -- Optionnel: garde l'historique EF si tu veux

EXEC sp_executesql @sql_tables;