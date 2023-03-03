INSERT INTO Customer (lastName, firstName, emailAddress, phoneNumber, adminUser, dateSignedUp, dateLastOrdered)
                 VALUES('Jones', 'Ricky', 'rickyjones@gmail.com', '6035869577', 'N', '2022-12-02', '2022-12-05');
INSERT INTO Customer (lastName, firstName, emailAddress, phoneNumber, adminUser, dateSignedUp, dateLastOrdered)
                 VALUES('Sullivan', 'Gemma', 'gemmasullivan@gmail.com', '6048850125', 'N', '2022-11-15', '2022-12-04');

INSERT INTO Category (categoryID) VALUES('Cheese');
INSERT INTO Category (categoryID) VALUES('Breads');
INSERT INTO Category (categoryID) VALUES('Protein');
INSERT INTO Category (categoryID) VALUES('Sauces');
INSERT INTO Category (categoryID) VALUES('Seasonings');
INSERT INTO Category (categoryID) VALUES('Vegetables');

INSERT INTO Product (productID, description, basePrice, image)
                        VALUES(1, 'Large Sandwich', 3.00, 'sandwich-large.jpg');
INSERT INTO Product (productID, description, basePrice, image)
                        VALUES(2, 'Medium Sandwich', 2.00, 'sandwich-medium.jpg');
INSERT INTO Product (productID, description, basePrice, image)
                        VALUES(3, 'Small Sandwich', 1.00, 'sandwich-small.jpg');

INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Bacon', 2.00, 'Protein', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Tomato', 0.50, 'Vegetables', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Ranch Dressing', 0.50, 'Sauces', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Mayonnaise', 0.25, 'Sauces', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('BBQ Sauce', 0.50, 'Sauces', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Beef', 2.00, 'Protein', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Lamb', 2.50, 'Protein', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Cucumber', 0.25, 'Vegetables', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Red Onion', 0.25, 'Vegetables', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Ciabatta Bun', 0.75, 'Breads', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Rye Bread', 0.75, 'Breads', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Multigrain Bread', 0.75, 'Breads', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Cheddar Cheese', 0.50, 'Cheese', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Camembert Cheese', 0.75, 'Cheese', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Cream Cheese', 0.25, 'Cheese', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Lettuce', 0.25, 'Vegetables', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Chicken', 2.00, 'Protein', 'Y');
INSERT INTO Ingredient (description, price, categoryID, inStock)
                        VALUES('Ciabatta Bun', 0.50, 'Breads', 'Y');

INSERT INTO OrderHeader (customerID, orderDate, orderValue, pickupTime, orderStatus, currency, paymentType, paymentID, paymentDate)
                         VALUES(2, '2022-11-15', 12.50, '2022-11-15 05:30:00 PM', 'C', 'CAD', 'Paypal', 'MPNU4QQ9N277425VE022403S', '2022-11-15 04:13:00 PM');
INSERT INTO OrderHeader (customerID, orderDate, orderValue, pickupTime, orderStatus, currency, paymentType, paymentID, paymentDate)
                         VALUES(1, '2022-12-02', 12.00, '2022-12-02 02:45:00 PM', 'O', 'CAD', 'Paypal', 'DSPU4YD8N357407VE036207P', '2022-12-02 01:09:00 PM');
INSERT INTO OrderHeader (customerID, orderDate, orderValue, pickupTime, orderStatus, currency, paymentType, paymentID, paymentDate)
                         VALUES(2, '2022-12-04', 16.25, '2022-12-04 07:06:00 PM', 'O', 'CAD', 'Paypal', 'EJIO4FL9N248485FR092409W', '2022-12-04 06:32:00 PM');
INSERT INTO OrderHeader (customerID, orderDate, orderValue, pickupTime, orderStatus, currency, paymentType, paymentID, paymentDate)
                         VALUES(1, '2022-12-05', 10.00, '2022-12-05 10:15:00 PM', 'O', 'CAD', 'Paypal', 'SKYN7QRAJ290725QD65495T', '2022-12-05 08:09:00 PM');

INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(1, 3, 1, 'C', 3.00);
INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(1, 2, 1, 'C', 2.00);

INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(2, 3, 1, 'O', 1.00);
INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(2, 1, 1, 'O', 3.00);

INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(3, 2, 1, 'O', 2.00);
INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(3, 1, 1, 'O', 3.00);

INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(4, 3, 1, 'O', 1.00);
INSERT INTO OrderLine (orderID, productID, quantity, lineStatus, price)
                       VALUES(4, 3, 1, 'O', 1.00);


INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 1, 12, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 1, 1, 1, 2.00);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 1, 2, 1, 0.50);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 1, 16, 1, 0.25);

INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 2, 11, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 2, 14, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 2, 9, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 2, 8, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(1, 2, 17, 1, 2.00);

INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 3, 11, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 3, 2, 1, 0.50);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 3, 6, 1, 2.00);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 3, 5, 1, 0.50);

INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 4, 10, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 4, 17, 1, 2.00);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 4, 13, 1, 0.50);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 4, 16, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 4, 2, 1, 0.50);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(2, 4, 4, 1, 0.25);

INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 5, 12, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 5, 9, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 5, 1, 2, 2.00);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 5, 8, 1, 0.25);

INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 6, 11, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 6, 9, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 6, 1, 2, 2.00);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 6, 8, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(3, 6, 14, 1, 0.75);

INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 7, 12, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 7, 7, 1, 2.50);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 7, 2, 1, 0.50);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 7, 8, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 7, 3, 1, 0.50);

INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 8, 10, 1, 0.75);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 8, 8, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 8, 6, 1, 2.00);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 8, 15, 1, 0.25);
INSERT INTO LineIngredient (orderID, lineID, ingredientID, quantity, price)
                            VALUES(4, 8, 4, 1, 0.25);


							