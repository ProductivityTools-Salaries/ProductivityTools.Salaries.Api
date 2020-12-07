UPDATE [s].[Salary] SET	B2b=0 where B2b is null
ALTER TABLE [s].[Salary] alter column B2b BIT not null
