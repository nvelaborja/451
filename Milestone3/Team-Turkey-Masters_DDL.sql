# Create Table Statements for DataBase Project MileStone 2
# Nathan VelaBorja & Jacob Krahling
# March 20, 2016
# CptS 451 Spring 2016

CREATE TABLE Demographics (
	zipcode 		varchar(10) PRIMARY KEY,
	state 			varchar(15),
	state_code 		char(2),
	city 			varchar(30),
	population 		integer,
	under18 		real 		CHECK (under18 <= 100.0),
	18to24 			real 		CHECK (18to24 <= 100.0),
	25to44 			real 		CHECK (25to44 <= 100.0),
	45to64 			real 		CHECK (45to64 <= 100.0),
	65andover 		real 		CHECK (65andover <= 100.0),
	med_age 		tinyint,
	perc_fe 		real 		CHECK (perc_fee <= 100.0),
	num_emp 		integer,
	annual_payroll 	integer,
	avg_income 		integer
);

CREATE TABLE Businesses (
	bid 		varchar(30) PRIMARY KEY,
	name 		varchar(75),
	avg_rev 	real 		CHECK (avg_rev <= 5.0),
	num_revs 	integer,
	city 		varchar(30) REFERENCES Demographics(city),
	state_code 	char(2) 	REFERENCES Demographics(state_code),
	zipcode 	varchar(10) REFERENCES Demographics(zipcode),
	open		tinyint
);

CREATE TABLE Attributes (
<<<<<<< HEAD
	bid 		varchar(30) REFERENCES Businesses(bid),
	name 		varchar(40),
	_value 		varchar(20),
	PRIMARY KEY(bid, name)
=======
	a_title		varchar(20),
    a_value		varchar(20),
    PRIMARY KEY(a_title, a_value)
>>>>>>> e9e48d452eebc4d2c4f495d5f369f13f4ccfbd4c
);

CREATE TABLE Categories (
	name 		varchar(75),
	bid 		varchar(30),
	PRIMARY KEY(name, bid)
);

CREATE TABLE Users (
	uid 		varchar(30) PRIMARY KEY,
	name 		varchar(50)
);

CREATE TABLE Reviews (
	rid 	varchar(30) PRIMARY KEY,
	uid 	varchar(30) REFERENCES Users(uid),
	bid 	varchar(30) REFERENCES Businesses(bid),
	stars 	tinyint 	CHECK(stars <= 5),
	funny 	smallint,
	useful 	smallint,
	cool 	smallint,
	date 	char(10),
	text 	text
);


# ******* NEED ********
# Trigger to update number of review and average reviews for Business
# Trigger to update votes counter in Reviews - *Triggers added to seperate file
# Order of Insertion:
#	Demographics
#	Businesses
#	Categories
#	Users
#	Reviews
# Probably need some UPDATE and DELETE handling

SELECT DISTINCT B.name FROM Businesses B, Categories C 
	WHERE B.state_code='NC' 
	AND B.city='CHARLOTTE' 
	AND B.zipcode='28207' 
	AND B.avg_rev >= 0 
	AND B.avg_rev <= 5 
	AND B.num_revs >= 0 
	AND B.num_revs <= 1000000 
	AND B.bid IN 
		(SELECT bid FROM Categories WHERE Categories.name = 'American (Traditional)' ORDER BY C.bid);

SELECT DISTINCT state_code FROM Businesses B WHERE B.state_code='NC' AND B.city='CHARLOTTE' AND B.zipcode='28207' AND B.avg_rev >= 0 AND B.avg_rev <= 5 AND B.num_revs >= 0 AND B.num_revs <= 1000000 AND B.bid IN (SELECT bid FROM Categories C WHERE C.name = 'American (Traditional)' ORDER BY C.bid);

SELECT DISTINCT name FROM Businesses B WHERE B.state_code='NC' AND B.city='CHARLOTTE' AND B.zipcode='28207' AND B.avg_rev >= 0 AND B.avg_rev <= 5 AND B.num_revs >= 0 AND B.num_revs <= 1000000 AND B.bid IN (SELECT bid FROM Categories C WHERE C.name = 'American (Traditional)' ORDER BY C.bid);

SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.state_code='NC' AND B.city='CHARLOTTE' AND B.zipcode='28207' AND B.avg_rev >= 0 AND B.avg_rev <= 5 AND B.num_revs >= 0 AND B.num_revs <= 1000000 AND B.bid IN (SELECT bid FROM Categories WHERE Categories.name = 'American (Traditional)') ORDER BY C.bid;

SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.state_code='NC' AND B.city='CHARLOTTE' AND B.zipcode='28207' AND B.avg_rev >= 0 AND B.avg_rev <= 5 AND B.num_revs >= 0 AND B.num_revs <= 1000000 AND B.bid IN (SELECT bid FROM Categories WHERE Categories.name = 'Bakeries') ORDER BY C.bid;
"SELECT DISTINCT B.name FROM Businesses B, Categories C WHERE B.state_code='NC' AND B.city='CHARLOTTE' AND B.zipcode='28207' AND B.avg_rev >= 0 AND B.avg_rev <= 5 AND B.num_revs >= 0 AND B.num_revs <= 1000000 AND B.bid IN (SELECT bid FROM Categories WHERE Categories.name = 'American (Traditional)') ORDER BY C.bid;"