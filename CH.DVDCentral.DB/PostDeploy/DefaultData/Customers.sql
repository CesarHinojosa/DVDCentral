BEGIN 
	INSERT INTO tblCustomer (Id, FirstName, LastName, UserId, Address, City, State, ZIP, Phone, ImagePath)
	VALUES
	(1, 'Cesar', 'Hinojosa', 1, '123 Main St', 'Appleton', 'WI', '54789', '9205404694', 'bruh.pdf'),
	(2, 'Lee', 'Sin', 2, '789 Apple Rd', 'Los Angeles','CA', '45693',  '9208459635', 'me.pdf'),
	(3, 'Lightning', 'Shen', 3, '526 Northland Dr', 'DePere', 'WI', '74258','9206874895', 'shen.pdf')
END