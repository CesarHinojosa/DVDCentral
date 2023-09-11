CREATE TABLE [dbo].[tblOrderItem]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [OrderID] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [MovieID] INT NOT NULL, 
    [Cost] FLOAT NOT NULL
)
