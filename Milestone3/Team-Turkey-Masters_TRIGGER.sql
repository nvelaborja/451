# Triggers for DataBase Project MileStone 2
# Nathan VelaBorja & Jacob Krahling
# March 22, 2016
# CptS 451 Spring 2016

CREATE TRIGGER ReviewCountInc
	AFTER INSERT ON Reviews
	FOR EACH ROW 
		SET num_revs = num_revs + 1;

CREATE TRIGGER ReviewCountDec
	AFTER DELETE ON Reviews
	FOR EACH ROW
		SET num_revs = num_revs - 1;

CREATE TRIGGER AvgReviewSet
	AFTER INSERT ON Reviews
		SET avg_rev = 
			SUM(SELECT stars FROM Reviews WHERE Reviews.bid = bid) /
			COUNT (SELECT * FROM Reviews WHERE Reviews.bid = bid);

CREATE TRIGGER OpenReviewOnly
	BEFORE INSERT ON Reviews
		IF NEW.bid = bid AND open != 1
		THEN NEW.bid = NULL;