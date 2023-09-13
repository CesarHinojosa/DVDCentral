BEGIN 
	INSERT INTO tblCustomer (ID, FirstName, LastName, UserID, Address, City, ZIP, Phone, ImagePath)
	VALUES
	(1, 'Cesar', 'Hinojosa', 1, '123 Main St', 'Appleton', '54789', '9205404694', 'bruh.pdf'),
	(2, 'Lee', 'Sin', 2, '789 Apple Rd', 'Greenbay', '45693', '9208459635', 'me.pdf'),
	(3, 'Lightning', 'Shen', 3, '526 Northland Dr', 'DePere', '74258','9206874895', 'shen.pdf')
END