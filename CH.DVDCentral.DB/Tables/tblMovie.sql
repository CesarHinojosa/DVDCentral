CREATE TABLE [dbo].[tblMovie]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [FormatID] INT NOT NULL, 
    [DirectorID] INT NOT NULL, 
    [RatingID] INT NOT NULL, 
    [Cost] FLOAT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [ImagePath] VARCHAR(100) NOT NULL


)
