BEGIN 
	INSERT INTO tblMovie (Id, Title, Description, FormatID, DirectorID, RatingID, Cost, InStkQty, ImagePath)
	VALUES
	(1, 'Star Wars', 'Lightsaber fighting across the galaxy', 1, 1, 1, 50.10, 25, 'starwars.pdf'),
	(2, 'World War Z', 'Horror zombie fighting to save planet', 2, 2, 2, 100.40, 50, 'Z.pdf'),
	(3, 'Grown Ups', 'Old guys having fun playing basketball', 3, 3, 3, 85.99, 30, 'grown.pdf')
END