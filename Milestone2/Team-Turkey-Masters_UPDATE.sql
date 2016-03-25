# Update Statements for DataBase Project MileStone 2
# Nathan VelaBorja & Jacob Krahling
# March 22, 2016
# CptS 451 Spring 2016

UPDATE Businesses
		SET num_revs = COUNT (SELECT rid FROM Reviews WHERE Reviews.bid = bid);
		
UPDATE Businesses
		SET avg_rev = 
			SUM(SELECT stars FROM Reviews WHERE Reviews.bid = bid) /
			COUNT (SELECT * FROM Reviews WHERE Reviews.bid = bid);