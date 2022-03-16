ALTER TABLE [s].[Salary]
DROP COLUMN Bonus 

ALTER TABLE [s].[Salary]
DROP COLUMN Equity 

ALTER TABLE [s].[Salary]
ADD Bonus INT DEFAULT(0)

ALTER TABLE [s].[Salary]
ADD Equity INT DEFAULT(0)

ALTER TABLE [s].[Salary]
ADD Total as Value+Equity+Bonus