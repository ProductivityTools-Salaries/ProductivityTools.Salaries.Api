DECLARE @name varchar(100)

SELECT @name =  c.name
FROM sys.default_constraints c
where name like '%Bonus%'

IF @name IS NOT NULL
BEGIN
EXEC('ALTER TABLE  [s].[Salary] DROP CONSTRAINT ' + @name)
END

SELECT @name =  c.name
FROM sys.default_constraints c
where name like '%Equity%'

IF @name is not null
begin
EXEC('ALTER TABLE  [s].[Salary] DROP CONSTRAINT ' + @name)
end
