﻿CREATE TABLE [dbo].[tblUser]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [UserName] VARCHAR(25) NOT NULL, 
    [Password] VARCHAR(28) NOT NULL
)
