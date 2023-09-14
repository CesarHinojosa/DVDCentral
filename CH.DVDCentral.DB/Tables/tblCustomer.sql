CREATE TABLE [dbo].[tblCustomer]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [UserID] INT NOT NULL, 
    [Address] VARCHAR(50) NOT NULL, 
    [City] VARCHAR(25) NOT NULL, 
    [State] VARCHAR(2) NOT NULL,
    [ZIP] VARCHAR(12) NOT NULL, 
    [Phone] VARCHAR(20) NOT NULL, 
    [ImagePath] VARCHAR(100) NOT NULL
)
