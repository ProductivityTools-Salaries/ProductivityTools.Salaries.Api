IF EXISTS (SELECT 1 FROM sys.objects o
          INNER JOIN sys.columns c ON o.object_id = c.object_id
          WHERE o.name = 'Salary' AND c.name = 'Total')
		  BEGIN
			ALTER TABLE [s].[Salary]
			DROP COLUMN Total 
		  END

IF EXISTS (SELECT 1 FROM sys.objects o
          INNER JOIN sys.columns c ON o.object_id = c.object_id
          WHERE o.name = 'Salary' AND c.name = 'Bonus')
		  BEGIN
			ALTER TABLE [s].[Salary]
			DROP COLUMN Bonus 
		  END

 IF EXISTS (SELECT 1 FROM sys.objects o
          INNER JOIN sys.columns c ON o.object_id = c.object_id
          WHERE o.name = 'Salary' AND c.name = 'Equity')
		  BEGIN
			ALTER TABLE [s].[Salary]
			DROP COLUMN Equity 
		  END







