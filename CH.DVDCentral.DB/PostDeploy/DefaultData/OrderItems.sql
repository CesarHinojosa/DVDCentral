BEGIN 
	INSERT INTO tblOrderItem (ID, OrderID, Quantity, MovieID, Cost)
	VALUES 
	(1, 1, 5, 1, 10.50),
	(2, 2, 4, 2, 12.10),
	(3, 3, 2, 3, 15)
END