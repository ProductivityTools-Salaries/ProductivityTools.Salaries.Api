ALTER TABLE [s].[Salary]
ADD Bonus INT NOT NULL
CONSTRAINT bonus_not_null DEFAULT(0)

ALTER TABLE [s].[Salary]
ADD Equity INT NOT NULL
CONSTRAINT Equity_not_null DEFAULT(0)



