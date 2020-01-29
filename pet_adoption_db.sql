DROP DATABASE IF EXISTS pet_adoption_db;
CREATE DATABASE pet_adoption_db;
USE pet_adoption_db;
CREATE TABLE Pets(
				 PetId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
                 PetName VARCHAR(255) NOT NULL,
                 PetDateArrived DATE NOT NULL );
CREATE INDEX idx_PetId ON Pets (PetId);
CREATE TABLE Customers(
				 CustomerId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
                 CustName VARCHAR(255) NOT NULL,
                 CustPhone VARCHAR(255) NOT NULL,
                 CustBalance DOUBLE NOT NULL );
CREATE INDEX idx_CustId ON Customers (CustId);
CREATE TABLE CustomerPets(
				 CustPetId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
                 PetId INT NOT NULL,
                 CustomerId INT NOT NULL,
                 CONSTRAINT fk_Pet_PetId FOREIGN KEY (PetId) REFERENCES Pets (PetId)
                 ON DELETE CASCADE,
                 CONSTRAINT fk_Customer_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
                 );-- ON DELETE CASCADE);
CREATE INDEX idx_CustPetId ON CustomerPets (CustPetId);
-- INSERT INTO Customers
--             VALUES
--             (1,"Adilet","2062903315",3000),
--             (2,"Uzak","2062903315",2000),
--             (3,"Nikolay","2062903445",1000);
-- INSERT INTO Pets
--             VALUES
--             (1,"Dog","2017-10-13"),
--             (2,"Cat","2018-11-12"),
--             (3,"Sneak","2000-12-10");
-- INSERT INTO CustomerPets
-- 			VALUES 
--             (1,2,1,"2000-12-09"),
--             (2,3,3,"2000-10-10"),
--             (3,3,1,"2000-09-11");
-- Select* 
-- FROM Pets LEFT JOIN CustomerPets using(PetId)
-- 		  LEFT JOIN Customers using(CustomerId)
--                           WHERE Pets.PetName='Sneak';
                           /*LEFT JOIN CustomerPets ON Pets.PetId=CustomerPets.PedId 
                          LEFT JOIN Customers ON CustomerPets.CustomerId = Customers.CustomerId;*/
-- Select Customers.CustName, Customers.CustPhone, Pets.PetName
--  FROM Pets LEFT JOIN CustomerPets using(PetId)
--  LEFT JOIN Customers using(CustomerId);
--  
 Select Customers.CustName, Customers.CustPhone, Customers.CustBalance,CustomerPets.CustPetId,CustomerPets.CustomerId
 FROM Customers LEFT JOIN CustomerPets using(CustomerId)
 LEFT JOIN Pets using(PetId);
		

                          