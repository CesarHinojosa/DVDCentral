CREATE TABLE [dbo].[tblOrder]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [CustomerID] INT NOT NULL, 
    [OrderDate] DATETIME NOT NULL, 
    [UserID] INT NOT NULL
)
