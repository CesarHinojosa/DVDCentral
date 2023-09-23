BEGIN 
	INSERT INTO tblOrder (Id, CustomerId, OrderDate, ShipDate, UserId)
	VALUES
	(1, 1, '8-7-23', '8-9-23', 1),
	(2, 2, '9-5-22', '9-7-22', 2),
	(3, 3, '8-15-23', '8-17-23', 3)
END